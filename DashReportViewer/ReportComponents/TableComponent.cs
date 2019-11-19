using DashReportViewer.Attributes;
using DashReportViewer.Models.Widgets;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DashReportViewer.ReportComponents
{
    public class TableComponent : BaseReportComponent
    {
        private List<string> _columns;
        private List<List<object>> _data;

        public TableComponent(Widget widget) : base(widget) { }

        public List<string> Columns
        {
            get
            {
                if (_columns == null)
                {
                    (_columns, _data) = ProcessData(RawData);
                }
                return _columns;
            }
        }

        public List<List<object>> Data
        {
            get
            {
                if (_data == null)
                {
                    (_columns, _data) = ProcessData(RawData);
                }
                return _data;
            }
        }

        public (List<string> columns, List<List<object>> data) ProcessData(IEnumerable<object> reportData)
        {
            if (reportData != null && reportData.Any())
            {
                var columns = new List<string>();
                var data = new List<List<object>>();

                var firstDataType = reportData.First();

                if (firstDataType.GetType() == typeof(Dictionary<string, object>))
                {
                    columns = ((Dictionary<string, object>)firstDataType).Keys.ToList();
                    var ReportLayout = (List<Dictionary<string, object>>)reportData;

                    foreach (var dataItem in ReportLayout)
                    {
                        var RowData = new List<object>();
                        foreach (var item in dataItem)
                        {
                            RowData.Add(item.Value);
                        }
                        data.Add(RowData);
                    }
                }
                else if (firstDataType.GetType() == typeof(List<object>))
                {
                    columns = ((List<object>)firstDataType).Select(c => c.ToString()).ToList();

                    var ReportLayout = (List<List<object>>)reportData;

                    foreach (var dataItem in ReportLayout.Skip(1))
                    {
                        var RowData = new List<object>();
                        foreach (var item in dataItem)
                        {
                            RowData.Add(item);
                        }
                        data.Add(RowData);
                    }
                }
                else
                {
                    var propInfo = firstDataType.GetType().GetProperties();

                    foreach (var column in propInfo)
                    {
                        // get the attribute of the column name
                        var newName = column.GetCustomAttribute<ColumnNameAttribute>();
                        if (newName != null)
                        {
                            columns.Add(newName.Name);
                        }
                        //else
                        //{
                        //    if (excludedProperties.Contains((string)column.Name) == false)
                        //    {
                        //        var columnName = mappedProperties.ContainsKey((string)column.Name) ? mappedProperties[(string)column.Name] : (string)column.Name;
                        //        columns.Add(columnName);
                        //    }
                        //}
                    }

                    foreach (var propertyItem in reportData)
                    {
                        var RowData = new List<object>();
                        foreach (var column in propInfo)
                        {
                            //if (excludedProperties.Contains((string)column.Name) == false)
                            //{
                                var propValue = GetPropValue(propertyItem, column.Name);
                                if (propValue != null)
                                {
                                    RowData.Add(propValue);

                                }
                                else
                                {
                                    RowData.Add("");
                                }
                            //}
                        }
                        data.Add(RowData);
                    }
                }
                return (columns, data);
            }
            return (null, null);
        }
    }
}
