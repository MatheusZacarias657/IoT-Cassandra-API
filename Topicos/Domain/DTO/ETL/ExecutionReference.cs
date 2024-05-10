using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.ETL
{
    public class ExecutionReference
    {
        public string GreenHouse { get; set; }
        public List<ExecutionAction> Execution { get; set; }
    }

    public class ExecutionAction
    {
        public string RelatedTables { get; set; }
        public DateTime LastExecution { get; set; }
    }
}
