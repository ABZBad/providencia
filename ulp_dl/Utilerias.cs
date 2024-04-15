using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ulp_dl
{
    public class CustomColumnNames
    {
        private string _columnName;
        private string _aliasColumnName;

        public CustomColumnNames(string ColumName, string Alias)
        {
            _columnName = ColumName;
            _aliasColumnName = Alias;
        }

        public string ColumnName
        {
            get { return _columnName; }
        }
        public string Alias
        {
            get { return _aliasColumnName; }
        }
    }
    public class CustomTable
    {
        public static DataTable GetCustomDataTable(DataTable OriginalDataTbl, List<CustomColumnNames> CustomColumnNames)
        {
            DataTable _dataResult = new DataTable();
            DataTable OriginalDatatTable = OriginalDataTbl.Copy();

            string _columnsToReturn = ":";
            List<string> _columnsToRemove = new List<string>();

            foreach (CustomColumnNames _columnNames in CustomColumnNames)
            {
                _columnsToReturn += _columnNames.ColumnName + ":";
            }
            //_columnsToReturn = _columnsToReturn.Trim();

            foreach (DataColumn _col in OriginalDatatTable.Columns)
            {
                if (_columnsToReturn.Contains(":" + _col.ColumnName + ":") == false)
                {
                    _columnsToRemove.Add(_col.ColumnName);
                }
            }
            foreach (string _columnNameToRemove in _columnsToRemove)
            {
                OriginalDatatTable.Columns.Remove(_columnNameToRemove);
            }
            string _alias = "'";
            for (int i = 0; i < OriginalDatatTable.Columns.Count; i++)
            {
                int _j = 0;
                foreach (CustomColumnNames _columnName in CustomColumnNames)
                {
                    if (OriginalDatatTable.Columns[i].ColumnName == _columnName.ColumnName)
                    {
                        try
                        {

                            OriginalDatatTable.Columns[i].ColumnName = _columnName.Alias;
                        }
                        catch (DuplicateNameException Ex)
                        {
                            //OriginalDatatTable.Columns[i].ColumnName = _alias + _columnName.Alias; // i.ToString(); //+ i.ToString(); //;
                            System.Diagnostics.Debug.Print(_j.ToString("000"));
                            OriginalDatatTable.Columns[i].ColumnName = _columnName.Alias + _j.ToString("0000");
                        }
                        break;
                    }
                    _j++;
                }
            }

            return OriginalDatatTable;
        }
        public static DataTable GetCustomDataTable(DataTable OriginalDatatTable, List<string> ColumnValues)
        {
            List<CustomColumnNames> Columns = new List<CustomColumnNames>();

            foreach (string _columnValue in ColumnValues)
            {


                string[] _line = _columnValue.Replace("[", "").Replace("]", "").Split(',');

                CustomColumnNames _cv = new CustomColumnNames(_line[0], _line[1]);

                Columns.Add(_cv);
            }

            return GetCustomDataTable(OriginalDatatTable, Columns);
        }
        public static DataTable RemoveDataTableItem(DataTable OriginalDataTable, string ColumnName, int UID)
        {

            DataTable _resultTable = new DataTable();


            _resultTable = OriginalDataTable.Copy();


            foreach (DataRow _row in _resultTable.Rows)
            {
                if ((int)_row[ColumnName] == UID)
                {
                    _resultTable.Rows.Remove(_row);
                    break;
                }
            }
            return _resultTable;
        }
    } 
    public static class Linq2DataTable
    {
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }

    }
    public class ObjectShredder<T>
    {
        private System.Reflection.FieldInfo[] _fi;
        private System.Reflection.PropertyInfo[] _pi;
        private System.Collections.Generic.Dictionary<string, int> _ordinalMap;
        private System.Type _type;

        // ObjectShredder constructor.
        public ObjectShredder()
        {
            _type = typeof(T);
            _fi = _type.GetFields();
            _pi = _type.GetProperties();
            _ordinalMap = new Dictionary<string, int>();
        }

        /// <summary>
        /// Loads a DataTable from a sequence of objects.
        /// </summary>
        /// <param name="source">The sequence of objects to load into the DataTable.</param>
        /// <param name="table">The input table. The schema of the table must match that 
        /// the type T.  If the table is null, a new table is created with a schema 
        /// created from the public properties and fields of the type T.</param>
        /// <param name="options">Specifies how values from the source sequence will be applied to 
        /// existing rows in the table.</param>
        /// <returns>A DataTable created from the source sequence.</returns>
        public DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Load the table from the scalar sequence if T is a primitive type.
            if (typeof(T).IsPrimitive)
            {
                return ShredPrimitive(source, table, options);
            }

            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            // Initialize the ordinal map and extend the table schema based on type T.
            table = ExtendTable(table, typeof(T));

            // Enumerate the source sequence and load the object values into rows.
            table.BeginLoadData();
            try
            {
                using (IEnumerator<T> e = source.GetEnumerator())
                {
                    while (e.MoveNext())
                    {
                        if (options != null)
                        {
                            table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                        }
                        else
                        {
                            table.LoadDataRow(ShredObject(table, e.Current), true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            if (!table.Columns.Contains("Value"))
            {
                table.Columns.Add("Value", typeof(T));
            }

            // Enumerate the source sequence and load the scalar values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                Object[] values = new object[table.Columns.Count];
                while (e.MoveNext())
                {
                    values[table.Columns["Value"].Ordinal] = e.Current;

                    if (options != null)
                    {
                        table.LoadDataRow(values, (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(values, true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public object[] ShredObject(DataTable table, T instance)
        {

            FieldInfo[] fi = _fi;
            PropertyInfo[] pi = _pi;

            if (instance.GetType() != typeof(T))
            {
                // If the instance is derived from T, extend the table schema
                // and get the properties and fields.
                ExtendTable(table, instance.GetType());
                fi = instance.GetType().GetFields();
                pi = instance.GetType().GetProperties();
            }

            // Add the property and field values of the instance to an array.
            Object[] values = new object[table.Columns.Count];
            foreach (FieldInfo f in fi)
            {
                values[_ordinalMap[f.Name]] = f.GetValue(instance);
            }

            foreach (PropertyInfo p in pi)
            {
                values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
            }

            // Return the property and field values of the instance.
            return values;
        }

        public DataTable ExtendTable(DataTable table, Type type)
        {
            // Extend the table schema if the input table was null or if the value 
            // in the sequence is derived from type T.            
            foreach (FieldInfo f in type.GetFields())
            {
                if (!_ordinalMap.ContainsKey(f.Name))
                {
                    // Add the field as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                        : table.Columns.Add(f.Name, f.FieldType);

                    // Add the field to the ordinal map.
                    _ordinalMap.Add(f.Name, dc.Ordinal);
                }
            }
            foreach (PropertyInfo p in type.GetProperties())
            {
                if (!_ordinalMap.ContainsKey(p.Name))
                {
                    // Add the property as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                        : table.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);

                    // Add the property to the ordinal map.
                    _ordinalMap.Add(p.Name, dc.Ordinal);
                }
            }

            // Return the table.
            return table;
        }
    }

}
