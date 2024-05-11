using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Domain.DTO.ETL
{
    public class CombinedData
    {
        public DateTime LasExecution { get; set; }
        public List<JsonObject> Registers { get; set; }

        public CombinedData(DateTime lasExecution, List<JsonObject> registers)
        {
            LasExecution = lasExecution;
            Registers = registers;
        }
    }
}
