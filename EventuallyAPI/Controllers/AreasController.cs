using EventuallyAPI.Core.DTOs;
using EventuallyAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventuallyAPI.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreasController:ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public AreasController(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<AreaDTO>>> GetAll()
        {
            var areas =await _applicationDBContext.Areas.ToListAsync();

            return areas.Select(area => new AreaDTO
            {
                Id = area.Id,
                Name = area.Name
            }).ToList();
        }
    }
}
