using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FG4._0
{
    /// <summary>
    /// Interaction logic for VideoController.xaml
    /// </summary>
    public partial class VideoController : UserControl
    {
        VideoControllerViewModel mv;
        public VideoController()
        {
            mv = (Application.Current as App).VCVM;
            DataContext = mv;
            InitializeComponent();
        }
        
        private void Open_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                mv.UpdateFile(openFileDialog.FileName);
                isFileUploaded = true;
            }
        }

        private void backwards_Button_Click(object sender, RoutedEventArgs e)
        { 
            mv.ChangeTime(-50);//5 sec
        }

        private void forward_Button_Click(object sender, RoutedEventArgs e)
        {
            mv.ChangeTime(50);//5 sec
        }

        bool isPlay = false;
        bool isFileUploaded = false;
        bool isFirst = true;

        private void PlayPause_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isFileUploaded)
            {
                if (!isPlay)
                {
                    PlayPauseBtn.Source = new BitmapImage(new Uri(@"pause.png", UriKind.Relative));
                    isPlay = true;
                    if (isFirst)
                    {
                        mv.Start();
                        isFirst = false;
                    }
                    else
                    {
                        mv.Play();
                    }
                }
                else
                {
                    PlayPauseBtn.Source = new BitmapImage(new Uri(@"play.png", UriKind.Relative));
                    isPlay = false;
                    mv.Pause();
                }
            }
        }
        private void Play() 
        {

        }
        private void Pause()
        {

        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            mv.ChangeSpeed(SpeedOfVideo.SelectedIndex * 0.25 + 0.25);
        }

        bool isplayed;
        private void VideoSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mv.ChangeTimeSlider((int)VideoSlider.Value);
            if (isplayed)
            {
                mv.Play();
            }
        }

        private void VideoSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isplayed = isPlay;
            mv.Pause();
        }
    }
}
