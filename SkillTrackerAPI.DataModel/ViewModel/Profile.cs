using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SkillTrackerAPI.DataModel.ViewModel
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
        [Range(1111111111,9999999999)]
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

    public class Technicalskill
    {
        public string Id { get; set; }

        public string Value { get; set; }

        [Required]
        public string SkillType { get; set; }

        [Required]
        [Range(0, 20)]
        public int SelectedValue { get; set; }
    }
}
