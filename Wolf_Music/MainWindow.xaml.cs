using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Wolf_Music.Classes;


namespace Wolf_Music
{
   
   
    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
       
    }

    public partial class MainWindow : Window
    {
        bool Max_Min = false;
        Music play;
        private TimeSpan TotalTime;
        Music playSearchMusic = new Music();
        List<Music> stackMusic = new List<Music>();
        DispatcherTimer timerVideoTime = new DispatcherTimer();
        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
        Thread myThread;
        public MainWindow(){

            InitializeComponent();
           

            SliderPlay.AddHandler(MouseLeftButtonUpEvent,
                      new MouseButtonEventHandler(timeSlider_MouseLeftButtonUp),
                      true);

            SliderPlay.AddHandler(MouseLeftButtonDownEvent,
                      new MouseButtonEventHandler(timeSlider_MouseLeftButtonDown),
                      true);

        }

        private void timeSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timerVideoTime.Stop();
           
        }

        private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Media.Position = TimeSpan.FromSeconds(SliderPlay.Value);
            timerVideoTime.Start();           
        }
    

        private void MainWindow_WolfMusic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            this.DragMove();
        } // MainWindow_WolfMusic_MouseLeftButtonDown

        private void Button_Close_Click(object sender, RoutedEventArgs e){
            this.Close();
        } // Button_Close_Click

        private void Button_Minim_Click(object sender, RoutedEventArgs e){
            WindowState = WindowState.Minimized;
        } // Button_Minim_Click

        private void Button_Collapse_Click(object sender, RoutedEventArgs e){
            if (Max_Min == false)
            {
                WindowState = WindowState.Maximized;
                Max_Min = true;
            } // if
            else if (Max_Min == true)
            {
                WindowState = WindowState.Normal;
                Max_Min = false;
            }
        } // Button_Collapse_Click

        private void I_LastAlbum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Переходим на вкладку альбома
            TabMyMusic.SelectedIndex = 1;
           
            TabMyMusic.SelectedItem = TabMyMusic.Items[2];
        }


        /// <summary>
        /// Открыть папку с музыкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OpenFile_Click(object sender, RoutedEventArgs e){
            // создаем новый поток\
            // Открываем диалоговое окно выбора папки
            string rootFolder ="";
           
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                // Получаем Выбранную папку
                rootFolder = dialog.SelectedPath;
                 
            myThread = new Thread(new ParameterizedThreadStart(OpenPath));
            myThread.Start(rootFolder); // запускаем поток
                              
        } // Button_OpenFile_Click
       
        /// <summary>
        /// Запуск музыки по 2 нажатию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_TabMusic_MouseDoubleClick(object sender, MouseButtonEventArgs e){
            play = (Music)(((System.Windows.Controls.DataGrid)sender).SelectedItem);
            if (play != null)
            {
               
                        Media.Source = new Uri(play.full_name);
                        Media.Play();
                        TB_MUsic.Text = play.name;
                        TB_album.Text = play.music_album_playlist;                      

            }
        } // DG_TabMusic_MouseDoubleClick

       
        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = Media.NaturalDuration.TimeSpan;
            SliderPlay.Maximum = TotalTime.TotalSeconds;
         
            // Create a timer that will update the counters and the time slider
          
            timerVideoTime.Interval = TimeSpan.FromSeconds(1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            timerVideoTime.Start();
          
        } // Media_MediaOpened

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                // Check if the movie finished calculate it's total time
                if (Media.NaturalDuration.TimeSpan.TotalSeconds > 0)
                {
                    if (TotalTime.TotalSeconds > 0)
                    {
                        // Updating time slider
                        SliderPlay.Value = Media.Position.TotalSeconds;
                        timemus.Text = Media.Position.ToString("mm\\:ss");
                    } // if

                } //if
                if (Media.Position == TotalTime)
                {
                    for (int i = 0; i < stackMusic.Count; i++)
                    {
                        if(stackMusic[i].full_name == play.full_name)
                        {
                           
                            i++;
                            play.full_name = stackMusic[i].full_name;
                            Media.Source = new Uri(stackMusic[i].full_name);
                            Media.Play();
                            TB_MUsic.Text = stackMusic[i].name;
                            TB_album.Text = stackMusic[i].music_album_playlist;
                            
                        }
                       
                    }
                  
                }
            }
            catch
            {

            }
        } //timer_Tick

        private void Button_pause_Click(object sender, RoutedEventArgs e){
            Media.Pause();
        } // Button_pause_Click

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            Media.Play();           
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                Media.Volume = (double)sliderVolume.Value;
            }
            catch
            {

            }
        }

        private void Button_Close2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_OpenPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
            
        } //Button_OpenPath_Click

        private void OpenFile()
        {
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                FileInfo fileInf = new FileInfo(filename);
                Media.Source = new Uri(fileInf.FullName);
                TB_MUsic.Text = fileInf.Name;
                TB_album.Text = "";
                Media.Play();

            } // if    
        } // OpenFile

        private  void OpenPath(object rootFolder)
        {
           
                List<string> OpenMusic = new List<string>();
               
                string folder = Convert.ToString(rootFolder);
            // Проходим по подпапкам и находим mp3 файлы
            if (folder != "")
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories))
                {
                    // Расширение файла 
                    string extens = System.IO.Path.GetExtension(file);

                    // находим mp3 файлы
                    if (extens == ".mp3")
                    {
                        // Добавляем в список найденную музыку 
                        OpenMusic.Add(file);
                    }

                }
                // Переходим на вкладку музыки 
                Dispatcher.Invoke(() => TabMyMusic.SelectedIndex = 0);
                Dispatcher.Invoke(() => TabMyMusic.SelectedItem = TabMusic);
                Dispatcher.Invoke(() => TabMyMusic.SelectedItem = TabMyMusic.Items[1]);
                Dispatcher.Invoke(() => DG_TabMusic.ItemsSource = null);
                Dispatcher.Invoke(() => DG_TabMusic.ItemsSource = playSearchMusic.SortSeachMusic(OpenMusic));
                Dispatcher.Invoke(() => stackMusic = playSearchMusic.SortSeachMusic(OpenMusic));
                myThread.Abort();//прерываем поток
                myThread.Join(500);//таймаут на завершение
            } // if

        }

        private void Button_OpenMusic_Click(object sender, RoutedEventArgs e){
            OpenFile();
        } // Button_OpenMusic_Click

        private void Button_OpenMusic_Path_Click(object sender, RoutedEventArgs e){
            // создаем новый поток\
            // Открываем диалоговое окно выбора папки
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            // Получаем Выбранную папку
            string rootFolder = dialog.SelectedPath;
            myThread = new Thread(new ParameterizedThreadStart(OpenPath));
            myThread.Start(rootFolder); // запускаем поток
        } // Button_OpenMusic_Path_Click

        private void Button_NewPLayList_Click(object sender, RoutedEventArgs e)
        {
            New_Album new_Album = new New_Album();
            new_Album.ShowDialog();
        }
    }
}
