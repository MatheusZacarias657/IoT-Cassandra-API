using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ETL.Application.Utils
{
    internal class QueryHelper
    {
        internal static string GetInsertQuery(JsonObject obj, string table)
        {
            string values = string.Empty;
            string fields = string.Empty;

            foreach (var property in obj)
            {
                fields += $"{property.Key},";

                if (double.TryParse($"{property.Value}", out double _) || 
                    bool.TryParse($"{property.Value}", out bool _))
                {
                    values += $"{property.Value},";
                }
                else
                {
                    values += $"'{property.Value}',";
                }
            }

            return $"INSERT INTO {table} ({fields[..^1]}) VALUES ({values[..^1]})";
        }
    }
}
