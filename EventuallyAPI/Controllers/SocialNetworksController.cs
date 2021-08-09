using EventuallyAPI.Core.DTOs;
using EventuallyAPI.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventuallyAPI.Controllers
{
    [ApiController]
    [Route("api/socialnetworks")]
    public class SocialNetworksController:
        ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public SocialNetworksController(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SocialNetworkDTO>>> GetAllSocialNetworks()
        {
            var socialNetworks = await _applicationDBContext.SocialNetworks.ToListAsync();

            return socialNetworks.Select(socialNetwork => new SocialNetworkDTO
            {
                Name=socialNetwork.Name,
                Id=socialNetwork.Id
            }).ToList();

        }
    }
}
