using Cassandra;
using Cassandra.Data.Linq;
using Domain.DTO.ETL;
using ETL.Application.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Repository
{
    internal class ETLRepository
    {
        private ISession _session;

        public ETLRepository()
        {
            _session = CassandraSessionFactory.GetSession();
        }

        public List<string> GetGreenhouses()
        {
            string query = @"SELECT id
                              FROM greenhouse_list";

            PreparedStatement statement = _session.Prepare(query);
            RowSet rows = _session.Execute(statement.Bind());

            return rows.ToArray().Select(row => row["id"].ToString()).ToList();
        }

        public List<ExecutionAction> GetExecutionActions(string greenhouse)
        {
            string query = @$"SELECT last_execution, related_tables, id
                             FROM etl_execution
                             WHERE greenhouse = '{greenhouse}'
                             ALLOW FILTERING;";

            PreparedStatement statement = _session.Prepare(query);
            RowSet rows = _session.Execute(statement.Bind());

            List<ExecutionAction> actions = rows.ToArray().Select(row =>
                                            new ExecutionAction(row["last_execution"].ToString(), row["related_tables"].ToString(), row["id"].ToString()))
                                            .ToList();

            return actions;
        }

        public void UpdateExecution(string id, DateTime lastExecution)
        {
            string lastExecutionText = lastExecution.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = @$"UPDATE etl_execution
                              SET last_execution = '{lastExecutionText}'
                              WHERE id = '{id}';";

            PreparedStatement statement = _session.Prepare(query);
            _session.Execute(statement.Bind());
        }
    }
}
