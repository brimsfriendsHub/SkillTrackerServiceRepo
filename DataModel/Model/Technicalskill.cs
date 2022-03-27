using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillTrackerSer.Model
{
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
