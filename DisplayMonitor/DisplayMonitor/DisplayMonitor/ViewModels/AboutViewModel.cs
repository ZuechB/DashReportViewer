using System;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DisplayMonitor.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        //private Timer _timer;
        public AboutViewModel()
        {
            Title = "About";
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));

            //_timer = new Timer();
            //_timer.Interval = 1000; // 1 milliseconds  
            //_timer.Elapsed += _timer_Elapsed;
            //_timer.Start();

            WebUrl = "https://www.google.com";
        }

        //private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //}

        private string _WebUrl;
        public string WebUrl
        {
            get { return _WebUrl; }
            set {
                _WebUrl = value;
                OnPropertyChanged(); 
            }
        }
    }
}