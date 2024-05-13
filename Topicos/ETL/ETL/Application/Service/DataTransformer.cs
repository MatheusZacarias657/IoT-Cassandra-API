using Domain.DTO.ETL;
using ETL.Application.Factory;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ETL.Application.Service
{
    internal class DataTransformer
    {
        internal static CombinedData Combine(List<DataReference> references)
        {
            double tolerance = ConfigurationFactory.GetInterval("Transform:CombineTolerance");
            List <IGrouping<DateTime,DataReference>> combinedData = references.GroupBy(o => o.RegisterDate).ToList();
            int lenght = combinedData.Count; 
            List<JsonObject> results = new ();

            for(int i = 0; i < lenght; i++)
            {
                if (i >= combinedData.Count)
                    break;

                IGrouping<DateTime, DataReference> item = combinedData[i];
                DateTime dateTime = item.Key;
                IGrouping<DateTime, DataReference> foundMore = combinedData.FirstOrDefault(g => g.Key == dateTime.AddSeconds(tolerance));

                if (foundMore != null)
                {
                    List<DataReference> newData = new (item);
                    newData = newData.Union(foundMore.Select(i => i)).ToList();
                    combinedData.Add(newData.GroupBy(a => dateTime).First());
                    combinedData.Remove(item);
                    combinedData.Remove(foundMore);
                }
            }

            foreach(IGrouping<DateTime, DataReference> groupReference in combinedData)
            {
                List<DataReference> datas = new(groupReference);
                JsonObject register = new()
                {
                    ["register_date"] = groupReference.Key.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                foreach (DataReference data in datas)
                {
                    string value = (data.Value.Contains(',')) ? data.Value.Replace(',', '.') : data.Value;
                    register[$"{data.Table}_value"] = value;
                }

                results.Add(register);
            }

            DateTime mostRecentelly = combinedData.OrderBy(a => a.Key).Last().Key;

            return new CombinedData(mostRecentelly, results);
        }
    }
}
