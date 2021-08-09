using EventuallyAPI.Core.DTOs;
using EventuallyAPI.Core.Entities;
using EventuallyAPI.Data;
using EventuallyAPI.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventuallyAPI.Controllers
{
    [ApiController]
    [Route("api/comunities")]
    public class ComunitiesController : ControllerBase

    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IFileStorage _fileStorage;

        public ComunitiesController(ApplicationDBContext applicationDBContext,
            IFileStorage fileStorage)
        {
            _applicationDBContext = applicationDBContext;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comunity>>> GetAll()
        {
            return await _applicationDBContext.Comunities.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comunity>> GetById(int id)
        {
            return await _applicationDBContext.Comunities.FirstOrDefaultAsync(comunity => comunity.Id == id);

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ComunityCreationDTO comunityCreationDTO)
        {
            var comunity = new Comunity
            {
                Name = comunityCreationDTO.Name,
                OtherArea = comunityCreationDTO.OtherArea,
                Description = comunityCreationDTO.Description,
                AreaId = comunityCreationDTO.AreaId,
                Email = comunityCreationDTO.Email
            };
            var transaction = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                using MemoryStream logoStream = new();
                await comunityCreationDTO.Logo.CopyToAsync(logoStream);

                comunity.Logo = await _fileStorage.SaveFile(logoStream.ToArray(), Path.GetExtension(comunityCreationDTO.Logo.FileName), "comunities", comunityCreationDTO.Logo.ContentType, Guid
                    .NewGuid().ToString());

                using MemoryStream bannerStream = new();
                await comunityCreationDTO.Banner.CopyToAsync(bannerStream);

                comunity.Banner = await _fileStorage.SaveFile(bannerStream.ToArray(), Path.GetExtension(comunityCreationDTO.Banner.FileName), "comunities", comunityCreationDTO.Banner.ContentType, Guid
                    .NewGuid().ToString());

                _applicationDBContext.Add(comunity);
                await _applicationDBContext.SaveChangesAsync();
                var socialNetworks = new List<ComunitySocialNetwork>();
                if (!string.IsNullOrEmpty(comunityCreationDTO.Facebook))
                {
                    socialNetworks.Add(new ComunitySocialNetwork
                    {
                        ComunityId=comunity.Id,
                        SocialNetworkId=SocialNetworkIdConstants.Facebook,
                        Value = comunityCreationDTO.Facebook
                    });
                }
                if (!string.IsNullOrEmpty(comunityCreationDTO.Instagram))
                {
                    socialNetworks.Add(new ComunitySocialNetwork
                    {
                        ComunityId = comunity.Id,
                        SocialNetworkId = SocialNetworkIdConstants.Instagram,
                        Value = comunityCreationDTO.Instagram
                    });
                }
                if (!string.IsNullOrEmpty(comunityCreationDTO.Twitter))
                {
                    socialNetworks.Add(new ComunitySocialNetwork
                    {
                        ComunityId = comunity.Id,
                        SocialNetworkId = SocialNetworkIdConstants.Twitter,
                        Value = comunityCreationDTO.Twitter
                    });
                }
                if (!string.IsNullOrEmpty(comunityCreationDTO.Linkedin))
                {
                    socialNetworks.Add(new ComunitySocialNetwork
                    {
                        ComunityId = comunity.Id,
                        SocialNetworkId = SocialNetworkIdConstants.Linkedin,
                        Value = comunityCreationDTO.Linkedin
                    });
                }
                if (!string.IsNullOrEmpty(comunityCreationDTO.Discord))
                {
                    socialNetworks.Add(new ComunitySocialNetwork
                    {
                        ComunityId = comunity.Id,
                        SocialNetworkId = SocialNetworkIdConstants.Discord,
                        Value = comunityCreationDTO.Discord
                    });
                }
                if (!string.IsNullOrEmpty(comunityCreationDTO.Github))
                {
                    socialNetworks.Add(new ComunitySocialNetwork
                    {
                        ComunityId = comunity.Id,
                        SocialNetworkId = SocialNetworkIdConstants.Github,
                        Value = comunityCreationDTO.Github
                    });
                }

                _applicationDBContext.AddRange(socialNetworks);

                await _applicationDBContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                transaction.Rollback();
            }

            return NoContent();
        }

        private int GetSocialNetworkId(string name)
        {
            return name == "facebook" ? SocialNetworkIdConstants.Facebook : name == "Linkedin" ? SocialNetworkIdConstants.Linkedin : name == "Instagram" ? SocialNetworkIdConstants.Instagram : name == "Github" ? SocialNetworkIdConstants.Github :
                name == "Twitter" ? SocialNetworkIdConstants.Twitter : SocialNetworkIdConstants.Discord;
        }

    }
}
