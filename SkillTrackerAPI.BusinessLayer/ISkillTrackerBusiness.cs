using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SkillTrackerAPI.DataModel.ViewModel;
namespace SkillTrackerAPI.BusinessLayer
{
    public interface ISkillTrackerBusiness
    {
        Task<List<AddSkillModel>> GetPersons();
        Task<List<AddSkillModel>> GetFilteredPersons(SearchSkillModel searchSkill);
        Task<AddSkillModel> GetPersonById(string id);
        Task<AddSkillModel> UpdatePerson(AddSkillModel person);
        Task<AddSkillModel> AddPerson(AddSkillModel person);
    }
}
