using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace SkillTrackerSer.DataModel
{
    [DynamoDBTable("Person")]
    public class Person
    {
        public Person() {
            CreatedDate = DateTime.Today;
        }

        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<Technicalskill> Technicalskills { get; set; }

    }

    public class Technicalskill
    {
        public Technicalskill()
        {
            Level = 0;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string SkillType { get; set; }
    }
}
