using System;
using System.Collections.Generic;
using System.Text;
using SkillTrackerAPI.Repositories;
using SkillTrackerAPI.DataModel.ViewModel;
using SkillTrackerAPI.DataModel;
using System.Threading.Tasks;
using System.Linq;
using SkillTrackerAPI.Repository;
using Technicalskill = SkillTrackerAPI.DataModel.Technicalskill;
using TechnicalskillViewModel = SkillTrackerAPI.DataModel.ViewModel.Technicalskill;

namespace SkillTrackerAPI.BusinessLayer
{
    public class SkillTrackerBusiness : ISkillTrackerBusiness
    {

        private ISkillTrackerRepository _istr;

        public SkillTrackerBusiness(ISkillTrackerRepository istr)
        {
            _istr = istr;
        }

        public async Task<List<AddSkillModel>> GetPersons()
        {
            List<AddSkillModel> lstPersons = new List<AddSkillModel>();
            var persons = await _istr.GetTaskAsync();

            lstPersons = persons.Select(x => new AddSkillModel
            {
                Id = x.Id,
                AssociateId = x.Id,
                Name = x.Name,
                EmailId = x.EmailId,
                MobileNo = x.MobileNo,
                TechnicalSkill = x.Technicalskills.Select(y => new TechnicalskillViewModel
                {
                    Id = y.Id,
                    Value = y.Name,
                    SkillType = y.SkillType,
                    SelectedValue = y.Level
                }).ToList()
            }).ToList();

            return lstPersons;
        }
        public async Task<List<AddSkillModel>> GetFilteredPersons(SearchSkillModel searchSkill)
        {
            List<AddSkillModel> lstPersons = new List<AddSkillModel>();
            try
            {
                var persons = await _istr.GetTaskAsync();
                var filtered = persons.Where(x =>
                (x.Id == searchSkill.AssociateId || string.IsNullOrEmpty(searchSkill.AssociateId)) &&
                (x.Name == searchSkill.Name || string.IsNullOrEmpty(searchSkill.Name)) &&
                (x.Technicalskills.Any(y => y.Name == searchSkill.SelectedSkill) || string.IsNullOrEmpty(searchSkill.SelectedSkill))
                ).ToList();

                lstPersons = filtered.Select(x => new AddSkillModel
                {
                    Id = x.Id,
                    AssociateId = x.Id,
                    Name = x.Name,
                    EmailId = x.EmailId,
                    MobileNo = x.MobileNo,
                    TechnicalSkill = x.Technicalskills.OrderByDescending(z=>z.Level).Select(y => new TechnicalskillViewModel
                    {
                        Id = y.Id,
                        Value = y.Name,
                        SkillType = y.SkillType,
                        SelectedValue = y.Level
                    }).ToList()
                }).ToList();

                //lstPersons.Add(AKS);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstPersons;
        }
        public async Task<AddSkillModel> GetPersonById(string id)
        {
            AddSkillModel person = new AddSkillModel();
            try
            {
                var persons = await _istr.FindByIdAsync(id);

                if (persons != null)
                {
                    person.AssociateId = persons.Id;
                    person.Name = persons.Name;
                    person.EmailId = persons.EmailId;
                    person.MobileNo = persons.MobileNo;

                    person.TechnicalSkill = persons.Technicalskills.
                                    Select(x => new TechnicalskillViewModel
                                    {
                                        Id = x.Id,
                                        Value = x.Name,
                                        SkillType = x.SkillType,
                                        SelectedValue = x.Level
                                    }).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;
        }

        public async Task<AddSkillModel> UpdatePerson(AddSkillModel person)
        {
            try
            {
                Person _person = new Person();

                _person.Id = person.Id;
                _person.Technicalskills = person.TechnicalSkill.Select(x => new Technicalskill
                {
                    Id = x.Id,
                    Name = x.Value,
                    SkillType = x.SkillType,
                    Level = x.SelectedValue
                }).ToList();

                await _istr.UpdateSkill(_person);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;

        }
        public async Task<AddSkillModel> AddPerson(AddSkillModel person)
        {
            try
            {
                Person _person = new Person();

                _person.Id = string.IsNullOrEmpty(person.Id) ? FormatId(person.AssociateId) : FormatId(person.Id);
                _person.Name = person.Name;
                _person.EmailId = person.EmailId;
                _person.MobileNo = person.MobileNo;
                _person.Technicalskills = person.TechnicalSkill.Select(x => new Technicalskill
                {
                    Id = x.Id,
                    Name = x.Value,
                    SkillType = x.SkillType,
                    Level = x.SelectedValue
                }).ToList();

                await _istr.SaveAsync(_person);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;

        }

        private string FormatId(string id)
        {
            var uid = id.ToUpperInvariant();
            return uid.StartsWith("CTS") ? id : string.Format("CTS{0}", uid);
        }
    }
}
