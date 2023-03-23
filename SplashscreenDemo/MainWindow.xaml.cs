using Squirrel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace SplashscreenDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string labelText = "Main Windows2";

        public string LabelText
        {
            get => labelText; set
            {
                labelText = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public MainWindow()
        {
            InitializeComponent();
            info.currentMainWindow = this;
            Task.Run(UpdateMyApp);
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            LabelText = "Update Available";
        }
        public void NoUpdate()
        {
            LabelText = "Up to date";
        }
        public void WaitUpdate()
        {
            LabelText = "Wait..";
        }
        private async Task UpdateMyApp()
        {
            info.currentMainWindow.WaitUpdate();
            using var mgr = new UpdateManager(@"C:\Users\RemcoFischerHetronic\source\repos\SplashscreenDemo\SplashscreenDemo\bin\Release\net6.0-windows\Releases");
            var newVersion = await mgr.UpdateApp();

            // optionally restart the app automatically, or ask the user if/when they want to restart
            if (newVersion != null)
            {
                info.currentMainWindow.Update();
                //UpdateManager.RestartApp();
            }
            else
                info.currentMainWindow.NoUpdate();
        }
    }
}
