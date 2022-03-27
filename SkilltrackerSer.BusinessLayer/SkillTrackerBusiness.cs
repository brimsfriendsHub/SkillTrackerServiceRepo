using System;
using System.Collections.Generic;
using System.Text;
using SkilltrackerSer.BusinessLayer;
using SkillTrackerSer.Repositories;
using SkillTrackerSer.Model;
using SkillTrackerSer.DataModel;
using System.Threading.Tasks;
using System.Linq;

namespace SkilltrackerSer.BusinessLayer
{
    public class SkillTrackerBusiness:ISkillTrackerBusiness
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
                TechnicalSkill = x.Technicalskills.Select(y => new SkillTrackerSer.Model.Technicalskill
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

            AddSkillModel AKS = new AddSkillModel();
            List<SkillTrackerSer.Model.Technicalskill> ts = new List<SkillTrackerSer.Model.Technicalskill>() {
                new SkillTrackerSer.Model.Technicalskill {
                Id="HTML",
                Value="HTML",
                SkillType= "Tech",
                SelectedValue = 12
            },
            new SkillTrackerSer.Model.Technicalskill
            {
                Id = "Spoken",
                Value = "Spoken",
                SkillType = "NTech",
                SelectedValue = 12
            }
            };

            AKS.AssociateId = "cts1";
            AKS.Id = AKS.AssociateId;
            AKS.Name = "RK";
            AKS.EmailId = "a@gmail.com";
            AKS.MobileNo = "9894123456";
            AKS.TechnicalSkill = ts;

            lstPersons.Add(AKS);

            if (searchSkill!=null)
                return lstPersons;

            var persons = await _istr.GetTaskAsync();
            var filtered = persons.Where(x =>
            (x.Id == searchSkill.AssociateId || string.IsNullOrEmpty(searchSkill.AssociateId)) &&
            (x.Name == searchSkill.Name || string.IsNullOrEmpty(searchSkill.Name)) &&
            (x.Technicalskills.Any(y => y.Name == searchSkill.SelectedSkill) || string.IsNullOrEmpty(searchSkill.SelectedSkill))
            ).ToList();

            lstPersons=filtered.Select(x => new AddSkillModel { Id = x.Id,
            AssociateId = x.Id,
            Name = x.Name,
            EmailId = x.EmailId,
            MobileNo = x.MobileNo,
            TechnicalSkill =x.Technicalskills.Select(y=> new SkillTrackerSer.Model.Technicalskill {
                Id = y.Id,
                Value = y.Name,
                SkillType = y.SkillType,
                SelectedValue = y.Level}).ToList()
            }).ToList();           

            lstPersons.Add(AKS);

            return lstPersons;
        }
        public async Task<AddSkillModel> GetPersonById(string id)
        {
            AddSkillModel person = new AddSkillModel();
            var persons = await _istr.FindByIdAsync(id);

            if (persons != null)
            {
                person.AssociateId = persons.Id;
                person.Name = persons.Name;
                person.EmailId = persons.EmailId;
                person.MobileNo = persons.MobileNo;

                person.TechnicalSkill = persons.Technicalskills.
                                Select(x => new SkillTrackerSer.Model.Technicalskill
                                {
                                    Id = x.Id,
                                    Value = x.Name,
                                    SkillType = x.SkillType,
                                    SelectedValue = x.Level
                                }).ToList();

            }

            return person;
        }

        public async Task<AddSkillModel> UpdatePerson(AddSkillModel person)
        {
            Person _person = new Person();

            _person.Id = person.Id;
            _person.Technicalskills = person.TechnicalSkill.Select(x => new SkillTrackerSer.DataModel.Technicalskill
            {
                Id = x.Id,
                Name = x.Value,
                SkillType = x.SkillType,
                Level = x.SelectedValue
            }).ToList();

            await _istr.UpdateSkill(_person);

            return person;

        }
        public async Task<AddSkillModel> AddPerson(AddSkillModel person)
        {
            Person _person = new Person();

            _person.Id = string.IsNullOrEmpty(person.Id)? FormatId(person.AssociateId): FormatId(person.Id);
            _person.Name = person.Name;
            _person.EmailId = person.EmailId;
            _person.MobileNo = person.MobileNo;
            _person.Technicalskills = person.TechnicalSkill.Select(x => new SkillTrackerSer.DataModel.Technicalskill
            {
                Id = x.Id,
                Name = x.Value,
                SkillType = x.SkillType,
                Level = x.SelectedValue
            }).ToList();

            await _istr.SaveAsync(_person);

            return person;

        }

        private string FormatId(string id)
        {
            var uid = id.ToUpperInvariant();
            return uid.StartsWith("CTS") ? id : string.Format("CTS{0}", uid);
        }

    }
}
