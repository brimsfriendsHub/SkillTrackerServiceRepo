using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillTrackerAPI.BusinessLayer;
using SkillTrackerAPI.DataModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillTrackerAPI.Controllers
{
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        public static List<AddSkillModel> Profiles { get; set; }
        private ISkillTrackerBusiness _istb;

        public AdminController(ISkillTrackerBusiness istb, ILogger<AdminController> logger)
        {
            _istb = istb;
            _logger = logger;

            Profiles = new List<AddSkillModel>();
            Profiles.Add(new AddSkillModel() { Name = "Raj", AssociateId = "CTS1", MobileNo = "9923412345", EmailId = "eamil@gmail.com" });
            Profiles.Add(new AddSkillModel() { Name = "Rajesh", AssociateId = "CTS2", MobileNo = "9923412345", EmailId = "fmamil@gmail.com" });
        }

        [HttpPost]        
        public async Task<List<AddSkillModel>> GetBySearch(SearchSkillModel searchskill)
        {
            return await _istb.GetFilteredPersons(searchskill);
        }
    }
}
