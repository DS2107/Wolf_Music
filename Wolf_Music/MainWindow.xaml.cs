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

    public partial class MainWindow : Window
    {
        /// <summary>
        /// Правая кнопка мышки по музыке 
        /// </summary>
        Music_Editing Music_Editing;
        /// <summary>
        /// Объект класса Music для доступа к методам
        /// </summary>
        private Music play;

        /// <summary>
        /// Указывает время для слайдера
        /// </summary>
        private TimeSpan TotalTime;

        /// <summary>
        /// Список найденной музыки
        /// </summary>
        List<Music> stackMusic = new List<Music>();

        /// <summary>
        /// Таймер для слайдера
        /// </summary>
        DispatcherTimer timerVideoTime = new DispatcherTimer();

        //Поток для файлов      
        Thread myThread;

        #region Window
        public MainWindow(){

            InitializeComponent();
            SliderPlay.AddHandler(MouseLeftButtonUpEvent,
                      new MouseButtonEventHandler(timeSlider_MouseLeftButtonUp),
                      true);

            SliderPlay.AddHandler(MouseLeftButtonDownEvent,
                      new MouseButtonEventHandler(timeSlider_MouseLeftButtonDown),
                      true);

        }

        /// <summary>
        /// Закртыь окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Двигать окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_WolfMusic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        } // MainWindow_WolfMusic_MouseLeftButtonDown

        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } // Button_Close_Click

        /// <summary>
        /// Свернуть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Minim_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        } // Button_Minim_Click

        /// <summary>
        /// Развернуть свернуть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Collapse_Click(object sender, RoutedEventArgs e)
        {
            bool Max_Min = false;
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

        #endregion Window

        #region Slider
        /// <summary>
        /// Переключение музыки на слайдере
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timerVideoTime.Stop();
        } // timeSlider_MouseLeftButtonDown

        /// <summary>
        /// Переключение музыки на слайдере
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Media.Position = TimeSpan.FromSeconds(SliderPlay.Value);
            timerVideoTime.Start();
        } // timeSlider_MouseLeftButtonUp
        #endregion

        #region PLayMusic
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

        private void Button_OpenMusic_Path_Click(object sender, RoutedEventArgs e)
        {
            // создаем новый поток\
            // Открываем диалоговое окно выбора папки
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            // Получаем Выбранную папку
            string rootFolder = dialog.SelectedPath;
            myThread = new Thread(new ParameterizedThreadStart(OpenPath));
            myThread.Start(rootFolder); // запускаем поток
        } // Button_OpenMusic_Path_Click


        private void Button_OpenPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();

        } //Button_OpenPath_Click

        private void Button_OpenMusic_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        } // Button_OpenMusic_Click
        #endregion

        #region MediaElem
        /// <summary>
        /// Открытие (проигрывание) Медиа Элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = Media.NaturalDuration.TimeSpan;
            SliderPlay.Maximum = TotalTime.TotalSeconds;
         
          
          
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

        #endregion

        #region OPEN
        /// <summary>
        /// Открыть файл
        /// </summary>
        private void OpenFile()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
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

        /// <summary>
        /// Открыть папку
        /// </summary>
        /// <param name="rootFolder">имя папки</param>
        private void OpenPath(object rootFolder)
        {
            play = new Music();
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
                Dispatcher.Invoke(() => DG_TabMusic.ItemsSource = play.SortSeachMusic(OpenMusic));
                Dispatcher.Invoke(() => stackMusic = play.SortSeachMusic(OpenMusic));
                myThread.Abort();   //прерываем поток
                myThread.Join(500); //таймаут на завершение
            } // if

        } // OpenPath

        #endregion

        #region RightButton
        /// <summary>
        /// Редактировать 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        } // MenuItem_Click EDIT

        /// <summary>
        /// Удалить песню навсегда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            play = (Music)(((System.Windows.Controls.DataGrid)sender).SelectedItem);
            if (play != null)
                Music_Editing.DeletMusic(play.full_name);
        } // MenuItem_Click_1 DELETE

        /// <summary>
        /// Перейти к папке с песней
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        } // MenuItem_Click_2 GO TO FOLDER
        #endregion

        /// <summary>
        /// 
        /// Перейти к созданию нового плейлиста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_NewPLayList_Click(object sender, RoutedEventArgs e)
        {
            New_Album new_Album = new New_Album();
            new_Album.ShowDialog();
        } // Button_NewPLayList_Click

       
    }
}
