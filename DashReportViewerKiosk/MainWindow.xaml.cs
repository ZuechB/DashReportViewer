using Authsome;
using DashReportViewerKiosk.Extentions;
using DashReportViewerKiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashReportViewerKiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly IAuthsomeService authsomeService;
        private Timer timer;
        private WebBrowser browser;

        private const string BaseUrl = "https://localhost:44302";

        //private string[] Pages = {
        //    "https://hellorayereport.azurewebsites.net/report/reports?reportType=724db366-5a96-43f5-a2fc-20dc108597e8&ContentType=5",
        //    "https://hellorayereport.azurewebsites.net/report/reports?reportType=6f61b059-24bb-4ce9-b83e-5c3d55991dbb&ContentType=5",
        //    "https://hellorayereport.azurewebsites.net/report/reports?reportType=7b01799f-36a6-4ed0-b29d-930cadb6138e&ContentType=5",
        //    "https://hellorayereport.azurewebsites.net/report/reports?reportType=b15848fd-6141-4693-9426-b8920fc2bb48&ContentType=5",
        //    "https://hellorayereport.azurewebsites.net/report/reports?reportType=bf634431-1172-4639-b3ca-218ba484c17b&ContentType=5",
        //    "https://hellorayereport.azurewebsites.net/report/reports?reportType=62caa9b0-a995-46f0-8530-31cfe0cee12f&ContentType=5",
        //    "https://hellorayereport.azurewebsites.net/report/reports?reportType=ab817d86-9ffd-43aa-9ce9-9059aa749d67&ContentType=5"
        //};

        private List<string> Pages { get; set; }

        private int index = 0;

        public MainWindow()
        {
            authsomeService = new AuthsomeService();
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var api = BaseUrl + "/api/reportapi";
            var response = await authsomeService.GetAsync<List<Report>>(api);
            Pages = new List<string>();

            foreach (var report in response.Content)
            {
                Pages.Add(BaseUrl + "/report/reports?reportType=" + report.id);
            }

            browser = new WebBrowser();
            clsWebbrowser_Errors.SuppressscriptErrors(browser, true);
            masterGrid.Children.Add(browser);

            timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Timer_Elapsed(null, null);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // another website here
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                browser.Source = new Uri(Pages[index]);
            }));
            index++;

            if (index >= Pages.Count())
            {
                index = 0;
            }
        }
    }
}
