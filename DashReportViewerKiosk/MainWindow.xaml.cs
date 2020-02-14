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
