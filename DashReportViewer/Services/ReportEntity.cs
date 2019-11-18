using DashReportViewer.Attributes;
using DashReportViewer.Models;
using DashReportViewer.Models.CoreBackPack.Time;
using DashReportViewer.Models.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DashReportViewer.Services
{
    public class ReportEntity
    {
        private string name;
        private string description;

        private IList<string> excludedProperties = new List<string>();
        private IDictionary<string, string> mappedProperties = new Dictionary<string, string>();

        protected readonly IReportService reportService;
        protected readonly Dictionary<string, object> parameterValues;
        protected IList<ReportParams> parameters;
        protected IList<ReportType> viewOptions;

        public IEnumerable<object> RawData { get; private set; }

        private List<string> _columns;
        private List<List<object>> _data;

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

        public string Filename { get; set; }

        public Func<object, List<(string filename, List<object> data)>> MultipleFileExport { get; set; }

        public ReportEntity(Dictionary<string, object> parameterValues, IReportService reportService)
        {
            this.parameterValues = parameterValues;
            this.reportService = reportService;

            SetPropertyValuesFromParams();
        }

        public async Task Run()
        {
            RawData = await Main();
        }

        public IList<ReportParams> Parameters
        {
            get
            {
                return parameters;
            }
        }

        private void SetPropertyValuesFromParams()
        {
            parameters = new List<ReportParams>();

            foreach (var param in GetAttributes())
            {
                if (param.GetType() == typeof(HideHeaderAttribute))
                {
                    HideHeader = true;
                }
                else if (param.GetType() == typeof(ReportViewAttribute))
                {
                    var paramVal = (ReportViewAttribute)param;

                    if (viewOptions == null)
                    {
                        viewOptions = new List<ReportType>();
                    }

                    viewOptions.Add(paramVal.Option);
                }
                else if (param.GetType() == typeof(ReportParams))
                {
                    var paramVal = (ReportParams)param;

                    var pitem = parameterValues.Where(p => p.Key.Replace(" ", string.Empty).ToLower() == paramVal.Id.ToLower());

                    if (pitem.Any())
                    {
                        paramVal.DefaultValue = pitem.First().Value;
                    }

                    if (paramVal != null && paramVal.DefaultValue != null)
                    {
                        //if (paramVal.DefaultValue.GetType() == typeof(Widget))
                        //{
                        //    var widget = ((Widget)paramVal.DefaultValue);
                        //    var test = widget;
                        //}
                        if (paramVal.DefaultValue.GetType() == typeof(string) && paramVal.InputType == ReportInputType.DateRange && !String.IsNullOrWhiteSpace(paramVal.DefaultValue.ToString()))
                        {
                            DateTime start;
                            DateTime end;

                            var dates = ((string)paramVal.Value).Trim().Split("-");
                            start = TimeFrame.StartOfDay(DateTime.Parse(dates[0]));
                            end = TimeFrame.EndOfDay(DateTime.Parse(dates[1]));

                            paramVal.DefaultValue = new DateRange() { Start = start, End = end };



                            //var dt = DateTime.Now;
                            //var ParamConditions = paramVal.DefaultValue.ToString().Split('_');
                            //if (ParamConditions.Any())
                            //{
                            //    foreach (var parmItem in ParamConditions)
                            //    {
                            //        if (parmItem.Contains("year:"))
                            //        {
                            //            var YearNum = Convert.ToInt32(parmItem.Replace("year:", ""));
                            //            dt = DateTime.Now.AddYears(YearNum);
                            //        }
                            //        else if (parmItem.Contains("month:"))
                            //        {
                            //            var MonthNum = Convert.ToInt32(parmItem.Replace("month:", ""));
                            //            dt = DateTime.Now.AddMonths(MonthNum);
                            //        }
                            //        else if (parmItem.Contains("day:"))
                            //        {
                            //            var dayNum = Convert.ToInt32(parmItem.Replace("day:", ""));
                            //            dt = DateTime.Now.AddDays(dayNum);
                            //        }
                            //        else if (parmItem.Contains("starttime"))
                            //        {
                            //            dt = TimeFrame.StartOfDay(dt);
                            //        }
                            //        else if (parmItem.Contains("endtime"))
                            //        {
                            //            dt = TimeFrame.EndOfDay(dt);
                            //        }
                            //        else if (parmItem == paramVal.DefaultValue.ToString())
                            //        {
                            //            dt = Convert.ToDateTime(parmItem);
                            //        }
                            //    }
                            //}
                            //paramVal.DefaultValue = dt.ToString();
                        }
                    }

                    parameters.Add(paramVal);
                }
            }
        }

        public Guid Id
        {
            get
            {
                return Guid.Parse(GetType().GetCustomAttribute<ReportNameAttribute>().ReportId);
            }
        }

        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(name))
                    return name;
                else
                    return GetType().GetCustomAttribute<ReportNameAttribute>().ReportName;
            }
            set
            {
                this.name = value;
            }
        }

        public string Description
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(description))
                {
                    return description;
                }
                else
                {
                    var nameAtt = GetType().GetCustomAttribute<ReportNameAttribute>();
                    if (nameAtt != null)
                    {
                        return nameAtt.Description;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            set
            {
                this.description = value;
            }
        }

        public bool HideHeader { get; private set; }

        public IList<ReportType> ViewOptions
        {
            get
            {
                if (viewOptions == null)
                    viewOptions = Enum.GetValues(typeof(ReportType)).Cast<ReportType>().ToList();
                return viewOptions;
            }
            private set
            {
                viewOptions = value;
            }
        }

        private Attribute[] GetAttributes()
        {
            var attributes = GetType().GetCustomAttributes();

            foreach (var attribute in attributes)
            {
                if (attribute.GetType() == typeof(ReportParams))
                {
                    var attri = (ReportParams)attribute;

                    if (attri.InputType == ReportInputType.CustomOption)
                    {
                        var methodName = attri.Name.Replace(" ", string.Empty);
                        var attriMethod = this.GetType().GetMethod(methodName);
                        if (attriMethod == null)
                        {
                            throw new NullReferenceException("No method with name " + methodName + "in report or base class. You must create a method to populate this custom parameter");
                        }
                        var custAttr = (ReportParams)attriMethod.GetCustomAttribute(typeof(ReportParams));

                        if (attri.Option == null)
                        {
                            attri.Option = new List<OptionObject>();
                        }

                        var result = (List<KeyValuePair<string, string>>)attriMethod.Invoke(this, new object[] { });

                        attri.Option.Add(new OptionObject()
                        {
                            Name = custAttr.Name,
                            Options = result
                        });
                    }

                }
            }
            return attributes.ToArray();
        }

        protected T GetParameterDefaultValue<T>(string name)
        {
            try
            {
                var attribute = parameters.SingleOrDefault(p => p.Id == name);
                if (attribute == null)
                {
                    attribute = GetType().GetCustomAttributes()
                                             .Where(a => a.GetType() == typeof(ReportParams) && ((ReportParams)a).Name.Replace(" ", "") == name)
                                             .FirstOrDefault() as ReportParams;
                }
                //if (conversion != null && attribute.DefaultValue.GetType() != typeof(T))
                //{
                //    return conversion(attribute.DefaultValue);
                //}

                return (T)attribute.DefaultValue;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public T GetParameterValueAsEnum<T>(string name) where T : struct
        {
            if (parameterValues != null)
            {
                foreach (var item in parameterValues)
                {
                    if (item.Key.ToLower() == name.ToLower())
                    {
                        if (!String.IsNullOrWhiteSpace(item.Value.ToString()))
                        {
                            return (T)Enum.Parse(typeof(T), item.Value.ToString());
                        }
                        break;
                    }
                }
            }

            return default(T);
        }

        //public string GetParameterValue(string name)
        //{
        //    if (parameterValues != null)
        //    {
        //        foreach (var item in parameterValues)
        //        {
        //            if (item.Key.ToString().ToLower() == name.ToLower())
        //                return item.Value.ToString();
        //        }
        //    }
        //    return GetParameterDefaultValue<string>(name);
        //}

        public T GetParameterValue<T>(string name) where T : class
        {
            //if (parameterValues != null)
            //{
            //    foreach (var item in parameterValues)
            //    {
            //        if (item.Key.ToLower() == name.ToLower())
            //        {
            //            if (!string.IsNullOrWhiteSpace(item.Value.ToString()))
            //            {
            //                return conversion(item.Value);
            //            }
            //            break;
            //        }
            //    }
            //}

            return GetParameterDefaultValue<T>(name);

        }

        //protected Location GetLocation(long locationId)
        //{
        //    return dataContext.Locations.Where(l => l.Id == locationId).FirstOrDefault();
        //}

        //protected Company GetCompany(long companyId)
        //{
        //    return dataContext.Companies.Where(c => c.Id == companyId).FirstOrDefault();
        //}

        //[ReportParams("Select Location")]
        //public List<KeyValuePair<string, string>> SelectLocation()
        //{
        //    var locations = (from l in dataContext.Locations
        //                     where PartnerId < 0 || l.PartnerId == PartnerId
        //                     orderby l.Name
        //                     select new { l.Id, l.Name })
        //                     .AsEnumerable()
        //                     .Select(l => new KeyValuePair<string, string>(l.Id.ToString(), l.Name))
        //                     .ToList();

        //    var listOption = GetParameterDefaultValue<ReportParams.ListOptions>("SelectLocation");

        //    if (listOption == ReportParams.ListOptions.ALL)
        //        locations.Insert(0, new KeyValuePair<string, string>(((int)listOption).ToString(), "All Locations"));

        //    return locations;
        //}


        protected List<KeyValuePair<string, string>> EnumValueToParamList(Type type)
        {
            //We have to do this type shit because for some reason not all of our enum types are ints. 
            var underlyingType = Enum.GetUnderlyingType(type);

            var values = Enum.GetValues(type);
            var toReturn = new List<KeyValuePair<string, string>>();
            foreach (var value in values)
            {
                toReturn.Add(new KeyValuePair<string, string>(Convert.ChangeType(value, underlyingType).ToString(), Enum.GetName(type, value).Replace("_", " ")));
            }

            return toReturn;

        }

        [ReportParams("Date Grouping")]
        public List<KeyValuePair<string, string>> DateGrouping()
        {
            var list = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Weeks", "Weeks"),
                new KeyValuePair<string, string>("Months", "Months")

            };

            return list;
        }

        protected List<KeyValuePair<string, string>> EnumToParamList(Type type)
        {
            return Enum.GetNames(type).AsEnumerable()
                                      .Select(item => new KeyValuePair<string, string>(item, item.Replace("_", " ")))
                                      .ToList();
        }

        public Guid ToGuid(object guid)
        {
            return Guid.Parse(guid.ToString());

        }

        protected virtual Task<IEnumerable<object>> Main()
        {
            throw new Exception("Report must override Main method to populate data.");
        }


        public (List<string> columns, List<List<object>> data) ProcessData(IEnumerable<object> reportData)
        {
            if (reportData != null && reportData.Any())
            {
                var columns = new List<string>();
                var data = new List<List<object>>();

                if (reportData.First().GetType() == typeof(Dictionary<string, object>))
                {
                    columns = ((Dictionary<string, object>)reportData.First()).Keys.ToList();
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
                else if (reportData.First().GetType() == typeof(List<object>))
                {
                    columns = ((List<object>)reportData.First()).Select(c => c.ToString()).ToList();

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
                    var propInfo = reportData.First().GetType().GetProperties();

                    foreach (var column in propInfo)
                    {
                        // get the attribute of the column name
                        var newName = column.GetCustomAttribute<ColumnNameAttribute>();
                        if (newName != null)
                        {
                            columns.Add(newName.Name);
                        }
                        else
                        {
                            if (excludedProperties.Contains((string)column.Name) == false)
                            {
                                var columnName = mappedProperties.ContainsKey((string)column.Name) ? mappedProperties[(string)column.Name] : (string)column.Name;
                                columns.Add(columnName);
                            }
                        }
                    }

                    foreach (var propertyItem in reportData)
                    {
                        var RowData = new List<object>();
                        foreach (var column in propInfo)
                        {
                            if (excludedProperties.Contains((string)column.Name) == false)
                            {
                                //if (propertyItem.GetType() == typeof(Widget))
                                //{
                                //    //var val = (propertyItem as Widget).Content;
                                //    RowData.Add(propertyItem);
                                //}
                                //else
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
                        }
                        data.Add(RowData);
                    }
                }
                return (columns, data);
            }
            return (null, null);
        }

        //protected void Exclude<T>(Expression<Func<T, object>> property)
        //{
        //    excludedProperties.Add(Reflection.FindProperty(property).Name);
        //}

        //protected void MapColumnName<T>(Expression<Func<T, object>> property, string columnName)
        //{
        //    mappedProperties.Add(Reflection.FindProperty(property).Name, columnName);
        //}

        //public IList<IWidget> Widgets
        //{
        //    get
        //    {
        //        if (widgets == null)
        //            widgets = new List<IWidget>();

        //        return widgets;
        //    }
        //}

        public virtual void RowClick(long Id)
        {

        }

        public virtual void RowClick(Guid Id)
        {

        }

        public virtual void RowClick(int Id)
        {

        }

        public virtual void RowClick(string Id)
        {

        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
