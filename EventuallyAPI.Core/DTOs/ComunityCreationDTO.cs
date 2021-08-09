

using Microsoft.AspNetCore.Http;

using System.Collections.Generic;

namespace EventuallyAPI.Core.DTOs
{
    public class ComunityCreationDTO
    {
        public string Name { get; set; }
        public int AreaId { get; set; }
        public string OtherArea { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Github { get; set; }
        public string Linkedin { get; set; }
        public string Instagram { get; set; }
        public string Discord { get; set; }
        public string Twitter { get; set; }
        public IFormFile Logo { get; set; }
        public IFormFile Banner { get; set; }
    }
}
