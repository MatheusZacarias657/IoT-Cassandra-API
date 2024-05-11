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
        public List<ExecutionAction> Executions { get; set; }

        public ExecutionReference(string greenhouse, List<ExecutionAction> executions) 
        {
            GreenHouse = greenhouse;
            Executions = executions;
        }
    }

    public class ExecutionAction
    {
        public List<string> RelatedTables { get; set; }
        public DateTime LastExecution { get; set; }
        public string Id { get; set; }

        public ExecutionAction(string lastExecution, string relatedTables, string id)
        {
            LastExecution = DateTime.Parse(lastExecution);
            RelatedTables = relatedTables.Split(',').ToList();
            Id = id;
        }
    }
}
