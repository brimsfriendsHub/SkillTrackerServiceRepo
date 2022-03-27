using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using SkillTrackerser.Repositories;
using SkillTrackerSer.DataModel;

namespace SkillTrackerSer.Repositories
{
    public interface ISkillTrackerRepository : IRepository<Person>
    {
        Task UpdateSkill(Person person);
    }
    public class SkillTrackerRepository : Repository<Person>, ISkillTrackerRepository
    {      
        readonly IDynamoDBContext _context;
        const int AllowUpdateAfter = 10;

        public SkillTrackerRepository(IDynamoDBContext context):base(context)
        {
            _context = context;
        }

        public async Task UpdateSkill(Person person)
        {
            var existingEntity = await FindByIdAsync(person.Id);

            if (existingEntity == null)
                throw new Exception($"Person with Id '{person.Id}' does not exist");

            DateTime dt = existingEntity.UpdatedDate.HasValue ? existingEntity.UpdatedDate.Value : existingEntity.CreatedDate;

            if (!IskillUpdateAllow(person))
                throw new Exception($"Minimum days '{AllowUpdateAfter}' required to update your profile");

            existingEntity.UpdatedDate = DateTime.Today;
            existingEntity.Technicalskills = person.Technicalskills;

            await _context.SaveAsync(existingEntity);
        }

        private bool IskillUpdateAllow(Person person)
        {
            if(person.UpdatedDate.HasValue && IsModifiedWithinDay(person.UpdatedDate.GetValueOrDefault()))
                return false;

            return IsModifiedWithinDay(person.CreatedDate);
        }

        bool IsModifiedWithinDay(DateTime dt)
        {
            return dt.AddDays(AllowUpdateAfter) > DateTime.Today;
        }
    }
}
