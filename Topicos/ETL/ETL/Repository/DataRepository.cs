using Cassandra;
using Cassandra.Data.Linq;
using Domain.DTO.ETL;
using ETL.Application.Factory;
using ETL.Application.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Text.Json.Nodes;

namespace ETL.Repository
{
    internal class DataRepository
    {
        private ISession _session;

        public DataRepository()
        {
            _session = CassandraSessionFactory.GetSession();
        }

        internal List<DataReference> Extract(string greenhouse, string tablename, string lastExecution)
        {

            string query = @$"SELECT register_date, value
                              FROM {tablename}
                              WHERE greenhouse = '{greenhouse}'
                              AND register_date > '{lastExecution}' 
                              ALLOW FILTERING;";

            PreparedStatement statement = _session.Prepare(query);
            RowSet rows = _session.Execute(statement.Bind());

            List<DataReference> datas = rows.ToArray().Select(row =>
                                            new DataReference(row["value"].ToString(), row["register_date"].ToString(), tablename))
                                            .ToList();

            return datas;
        }

        internal void Load(List<JsonObject> registers, string greenhouse, string tablename)
        {
            Console.WriteLine("Inicialize the data load");

            foreach (JsonObject register in registers)
            {
                register["greenhouse"] = greenhouse;
                register["id"] = Guid.NewGuid().ToString();

                string query = QueryHelper.GetInsertQuery(register, tablename);
                PreparedStatement statement = _session.Prepare(query);
                _session.Execute(statement.Bind());
            }

            Console.WriteLine("All data was loaded");
        }
    }
}
