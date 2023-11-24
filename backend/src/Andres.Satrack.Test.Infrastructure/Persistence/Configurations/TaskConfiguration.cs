using Andres.Satrack.Test.Domain.Aggregates.TaskAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Andres.Satrack.Test.Domain.Aggregates.TaskAggregate.Task;

namespace Andres.Satrack.Test.Infrastructure.Persistence.Configurations
{
    internal class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name);
            builder.Property(t => t.Category);
            builder.Property(t => t.LimitDate);
            builder.Property(t => t.Completed);
        }
    }
}
