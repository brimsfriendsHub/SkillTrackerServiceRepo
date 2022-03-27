using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillTrackerSer.Model;
using SkilltrackerSer.BusinessLayer;

namespace SkillTrackerSer.Controllers
{
    [Route("api/admin")]
    public class AdminController : Controller
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


        [HttpGet]
        public string Get()
        {
            return "Admin  Get";
        }

        [HttpGet]
        [Route("GetBySearch")]
        public async Task<List<AddSkillModel>> GetBySearch(SearchSkillModel searchskill)
        {           

            return  await _istb.GetFilteredPersons(searchskill);
        }

        [HttpGet("GetByName")]
        public AddSkillModel GetByName(string col)
        {
            //return "GetByName You can use me";
            return Profiles.Where(x => x.AssociateId == col).FirstOrDefault();
        }
        [HttpGet("GetBySkill")]
        public string GetBySkill(string col)
        {
            //var res = Profiles.Select(x.EmployeeController).Where(x => x. =col);
            return "GetBySkill You can use me";
        }
    }
}