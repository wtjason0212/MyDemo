using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCore.Data;

namespace UserCore.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/form")]
    [ApiController]
    public class OldWhiteController : ControllerBase
    {
        private readonly SchoolContext _context;

        public OldWhiteController(SchoolContext schoolContext)
        {
            _context = schoolContext;
        }

        /// <summary>
        /// OldWhite111
        /// </summary>
        /// <returns>OldWhite111</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var white = from s in _context.oldwhites
                        select s;

            var result = white.AsNoTracking().ToList();

            return Content("");
        }

    }
}
