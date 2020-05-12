using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreBackpack.Time;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportComponents;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace DashReportViewer.Shared.RealTimeCompiler
{
    public class Compile
    {
        public async static Task<ReportEntity> Execute(Guid id, string cSharpCode, IReportService reportService, DashReportAppSettings appSettings, ReportType ContentType = ReportType.View)
        {
            var output = new List<string>();

            // define source code, then parse it (to the type used for compilation)
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(cSharpCode);


            var coreDir = Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location);
            
            var files = Directory.GetFiles(coreDir, "*.dll");

            List<MetadataReference> references = new List<MetadataReference>();
            foreach (var item in files)
            {
                var fi = new FileInfo(item);
                if (fi.Name.Contains("System."))
                {
                    references.Add(MetadataReference.CreateFromFile(item));
                }
            }

            references.Add(MetadataReference.CreateFromFile(typeof(TimeZoneExtention).Assembly.Location));

            // define other necessary objects for compilation
            string assemblyName = Path.GetRandomFileName();

            // analyse and generate IL code from syntax tree
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                // write IL code into memory
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    // handle exceptions
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        output.Add(diagnostic.Id + ": " + diagnostic.GetMessage());
                        //Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    // load this 'virtual' DLL so that we can use
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    // just need to define these two....
                    Dictionary<string, object> parameterValues = new Dictionary<string, object>(); // empty right now


                    // create instance of the desired class and call the desired function
                    Type type = assembly.GetType("DashReportViewer.Reports.Report");
                    ReportEntity obj = (ReportEntity)Activator.CreateInstance(type, parameterValues, reportService);

                    await obj.Run();







                    var components = new List<BaseReportReportComponent>();
                    foreach (Widget widget in obj.RawData)
                    {
                        if (widget.Content.GetType() == typeof(TableContent))
                        {
                            components.Add(new TableReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(AreaChartContent))
                        {
                            components.Add(new AreaChartReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(BubbleChartContent))
                        {
                            components.Add(new BubbleChartReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(CalendarChartContent))
                        {
                            components.Add(new CalendarChartReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(PieChartContent))
                        {
                            components.Add(new PieChartReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(HistogramsContent))
                        {
                            components.Add(new HistogramsReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(ScatterChartContent))
                        {
                            components.Add(new ScatterChartReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(TextContent))
                        {
                            components.Add(new TextReportComponent(widget));
                        }
                        else if (widget.Content.GetType() == typeof(AnnotationChartContent))
                        {
                            components.Add(new AnnotationChartReportComponent(widget));
                        }
                    }

                    if (obj != null)
                    {
                        return obj;
                    }
                    throw new Exception("Report is null");
                }
            }

            return null;
        }
    }
}
