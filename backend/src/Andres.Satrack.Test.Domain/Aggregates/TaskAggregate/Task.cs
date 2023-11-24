using SharedKernel.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andres.Satrack.Test.Domain.Aggregates.TaskAggregate
{
    public class Task : AggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime LimitDate { get; set; }

        private Task(string name, string category, DateTime limitDate)
        {
            Name = name;
            Category = category;
            LimitDate = limitDate;
        }

        public static Task Create(string name, string category, DateTime limitDate)
        {
            var task = new Task(name, category, limitDate);

            return task;
        }

        public void Edit(string name, string category, DateTime limitDate)
        {
            Name = name;
            Category = category;
            LimitDate = limitDate;
        }
    }
}
