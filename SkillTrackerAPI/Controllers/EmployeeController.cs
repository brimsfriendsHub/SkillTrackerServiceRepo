using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillTrackerAPI.DataModel.ViewModel;
using SkillTrackerAPI.BusinessLayer;
using Microsoft.Extensions.Logging;

namespace SkillTrackerAPI.Controllers
{
    [Route("api/v1/engineer")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public static List<AddSkillModel> Profiles { get; set; }
        private ISkillTrackerBusiness _istb;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ISkillTrackerBusiness istb, ILogger<EmployeeController> logger)
        {
            _istb = istb;
            _logger = logger;
            Profiles = new List<AddSkillModel>();
            Profiles.Add(new AddSkillModel() { Name = "Raj", AssociateId = "CTS1", MobileNo = "9923412345", EmailId = "eamil@gmail.com" });
            Profiles.Add(new AddSkillModel() { Name = "Rajesh", AssociateId = "CTS2", MobileNo = "9923412345", EmailId = "fmamil@gmail.com" });
        }


        [HttpGet("getProfiles")]
        public async Task<List<AddSkillModel>> GetProfiles()
        {
            return await _istb.GetPersons();
        }

        [HttpGet("getProfile/{id}")]
        public async Task<AddSkillModel> GetProfile(string id)
        {
            return await _istb.GetPersonById(id);
        }

        [HttpPost]
        [Route("add-profile")]
        public async Task<AddSkillModel> AddProfile([FromBody] AddSkillModel profile)
        {
            return await _istb.AddPerson(profile);
        }

        [HttpPost]
        [Route("update-profile")]
        public async Task<AddSkillModel> UpdateProfile([FromBody] AddSkillModel profile)
        {
            return await _istb.UpdatePerson(profile);
        }
    }
}
