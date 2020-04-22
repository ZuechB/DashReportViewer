using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DashReportViewer.Shared.Models.CoreBackPack.Time;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace DashReportViewer.Shared.RealTimeCompiler
{
    public class Compile
    {
        public async static Task<List<string>> Execute(Guid id, string cSharpCode)
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
            //MetadataReference[] references = new MetadataReference[]
            //{
            //    //MetadataReference.CreateAssemblyReference("mscorlib"),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "mscorlib.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Runtime.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Linq.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Data.Linq.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Data.DataSetExtensions.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Xml.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Xml.Linq.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.dll")),
            //    MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Core.dll")),
            //    MetadataReference.CreateFromFile(typeof(TimeZoneExtention).Assembly.Location)
            //};

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

                    try
                    {
                        // just need to define these two....
                        Dictionary<string, object> parameterValues = new Dictionary<string, object>();
                        ReportService reportService



                        // create instance of the desired class and call the desired function
                        Type type = assembly.GetType("DashReportViewer.Reports.Report");
                        object obj = Activator.CreateInstance(type, parameterValues, reportService);
                        var returnObj = type.InvokeMember("Main",
                            BindingFlags.Default | BindingFlags.InvokeMethod,
                            null,
                            obj,
                            null); //{ "Hello World" });

                        var enumeration = returnObj;
                    }
                    catch(Exception exp)
                    {

                    }

                    







                    //output.Add(returnObj);

                    return output;
                }
            }

            return output;
        }
    }
}
