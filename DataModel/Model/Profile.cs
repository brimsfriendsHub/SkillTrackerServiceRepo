using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SkillTrackerSer.Model;

namespace SkillTrackerSer.Model
{
    public class AddSkillModel
    {     
        public string Id { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        //[RegularExpression("^CTS[0-9]*", ErrorMessage = "Must start with CTS..")]
        public string AssociateId { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        public List<Technicalskill> TechnicalSkill { get; set; }

        public AddSkillModel()
        {
            TechnicalSkill = new List<Technicalskill>();
         
        }
    }
}
