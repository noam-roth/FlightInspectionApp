using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FG4._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    ///

    public partial class App : Application
    {
        public VideoControllerViewModel VCVM { get; internal set; }
        public NavigatorsViewModel NVM { get; internal set; }
        public GraphsAndCorrelationViewModel GACVM { get; internal set; }
        FGModel FGM;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            File.Copy(e.Args[0]+".dll", "Detector.dll", true);
            
            FGM = new FGModel(e.Args[0]);
            VCVM = new VideoControllerViewModel(new VideoControllerModel(FGM));
            NVM = new NavigatorsViewModel(new NavigatorsModel(FGM));
            GACVM = new GraphsAndCorrelationViewModel(new GraphsAndCorrelationModel(FGM));
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FGM.t.Abort();//
        }
    }
}
