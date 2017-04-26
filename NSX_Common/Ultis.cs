using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NSX_Common
{
    public class Ultis
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            
                            if (propertyInfo.PropertyType.IsEnum)
                                propertyInfo.SetValue(obj, Enum.Parse(propertyInfo.PropertyType, row[prop.Name].ToString(), true), null);
                            else
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (!DBNull.Value.Equals(dr[column.ColumnName]))
                        {
                            if (pro.PropertyType.IsEnum)
                                pro.SetValue(obj, Enum.Parse(pro.PropertyType, dr[column.ColumnName].ToString(), true), null);
                            else
                                pro.SetValue(obj, dr[column.ColumnName], null);
                        }

                    }

                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
