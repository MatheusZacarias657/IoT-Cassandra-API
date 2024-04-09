using System.Globalization;
using System.Reflection;

namespace IoTCassandraAPI.Application.Util
{
    public class QueryHelper
    {
        public static string GetInsertQuery<T>(T obj, string table)
        {
            try
            {
                PropertyInfo[] objProperties = obj.GetType().GetProperties();
                string values = string.Empty;
                string fields = string.Empty;

                foreach (PropertyInfo prop in objProperties)
                {
                    string propName = string.Empty;
                    object propValue = null;

                    try
                    {
                        propName = prop.CustomAttributes.FirstOrDefault().ConstructorArguments.FirstOrDefault().Value.ToString();
                    }
                    catch (Exception)
                    {
                        propName = prop.Name;
                    }

                    try
                    {
                        propValue = prop.GetValue(obj);
                    }
                    catch(Exception ex) { }

                    fields += $"{propName},";

                    if (prop.PropertyType == typeof(string))
                    {
                        values += $"'{propValue}',";
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        DateTime dateTime = DateTime.Parse(propValue.ToString());
                        string finalDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        values += $"'{finalDate}',";
                    }
                    else
                    {
                        values += $"{propValue},";
                    }                    
                }

                return $"INSERT INTO {table} ({fields[..^1]}) VALUES ({values[..^1]})";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
