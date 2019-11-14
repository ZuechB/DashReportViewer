﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DashReportViewer.Models.Reporting
{
    public class ReportViewModel
    {
        private IEnumerable<ReportParams> parameters;

        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public Guid UniqueID { get; set; }
        public IEnumerable<ReportParams> Parameters
        {
            get
            {
                return parameters.OrderBy(p => p.OrderId);
            }
            set
            {
                this.parameters = value;
            }
        }
        public bool HasViewTypes
        {
            get
            {
                return ViewTypes.Where(t => t != ReportType.View).Count() > 0;
            }
        }

        public IList<string> Columns { get; set; }
        public IList<List<string>> Data { get; set; }
        public IList<ReportType> ViewTypes { get; set; }
        public bool HasData { get { return Data.Count > 0; } }
    }
}