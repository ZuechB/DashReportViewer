using System;
using System.Collections.Generic;
using System.Text;

namespace DashReportViewer.GA.Models
{
    public class DimensionResultFlexible
    {
        public string FirstColumn { get; set; }
        public string? SecondColumn { get; set; }
        public string? ThirdColumn { get; set; }
        public string? FourthColumn { get; set; }
        public string? FifthColumn { get; set; }
        public string ValueOne { get; set; }
        public string? ValueTwo {get; set; }
        public string? ValueThree { get; set; }
        public string? ValueFour { get; set; }
        public string? ValueFive { get; set; }
        public DateTime? Date { get; set; }
    }
}
