using Microsoft.EntityFrameworkCore;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andres.Satrack.Test.Infrastructure.Persistence
{
    public class TaskContext : DbContextBase
    {
        public TaskContext(DbContextOptions options, IValidatableObjectService validatableObjectService) :
            base(options, "Andres.Satrack.Test", typeof(TaskContext).Assembly, validatableObjectService, null)
        {
        }
    }
}
