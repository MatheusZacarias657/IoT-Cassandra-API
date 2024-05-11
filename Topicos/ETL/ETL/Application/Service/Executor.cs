using Cassandra;
using Domain.DTO.ETL;
using ETL.Application.Service;
using ETL.Repository;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Application.Processing
{
    internal class Executor : IDisposable
    {
        private ETLRepository _etlRepository;
        private DataRepository _dataRepository;

        public Executor()
        {
            _etlRepository = new ETLRepository();
            _dataRepository = new DataRepository();
        }

        internal void ExecuteExtraction()
        {
            List<ExecutionReference> executions = GetExecutions();

            foreach (ExecutionReference execution in executions)
            {
                foreach (ExecutionAction action in execution.Executions)
                {
                    DateTime? executionDate = ProcessData(action, execution.GreenHouse);

                    if(executionDate != null)
                    {
                        _etlRepository.UpdateExecution(action.Id, executionDate.Value);
                    }
                }
            }

            Console.WriteLine("ExecuteExtraction");
        }

        private List<ExecutionReference> GetExecutions()
        {
            List<string> greenhouses = _etlRepository.GetGreenhouses();
            List<ExecutionReference> references = new ();

            foreach (string greenhouse in greenhouses)
            {
                List<ExecutionAction> actions = _etlRepository.GetExecutionActions(greenhouse);
                ExecutionReference reference = new(greenhouse, actions);
                references.Add(reference);
            }
            return references;
        }

        private DateTime? ProcessData(ExecutionAction action, string greenhouse)
        {
            string lastExecution = action.LastExecution.ToString("yyyy-MM-dd HH:mm:ss.fff");
            List<DataReference> refereces = new ();
            string tableName = "";

            foreach (string table in action.RelatedTables)
            {
                List<DataReference> data = _dataRepository.Extract(greenhouse, table, lastExecution);

                if (data.Count == 0)
                    return null;

                refereces = refereces.Union(data).ToList();
                tableName += $"{table}_";
            }

            tableName = tableName[..^1];

            CombinedData registers = DataTransformer.Combine(refereces);
            _dataRepository.Load(registers.Registers, greenhouse, tableName);

            return registers.LasExecution;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
