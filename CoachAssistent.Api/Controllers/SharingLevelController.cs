using CoachAssistent.Common.Enums;
using CoachAssistent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharingLevelController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<SelectViewModel> GetSharingLevels()
        {
            return Enum.GetValues<SharingLevel>().Select(sl => new SelectViewModel((int)sl, sl.ToString()));
        }
    }
}
