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

        public static MainWindow Instance { get; private set; } // тут будет форма

        Albums albums = new Albums();
       
        /// <summary>
        /// Объект класса Music для доступа к методам
        /// </summary>
        private Music play;

        /// <summary>
        /// Указывает время для слайдера
        /// </summary>
        public TimeSpan TotalTime;

        /// <summary>
        /// Список найденной музыки
        /// </summary>
        List<Music> stackMusic = new List<Music>();


        List<Albums> stackAlbum = new List<Albums>();

        List<Music> LastMusic = new List<Music>();
        /// <summary>
        /// Таймер для слайдера
        /// </summary>
        DispatcherTimer timerVideoTime = new DispatcherTimer();
       


        //Поток для файлов      
        System.Threading.Thread myThread;

        #region Window
        public MainWindow(){

            InitializeComponent();
            Instance = this;

           DG_TabLastMusic.ItemsSource =  LM.Load();
            myThread = new Thread(new ThreadStart(loadAlb));
            myThread.Start();
         

            SliderPlay.AddHandler(MouseLeftButtonUpEvent,
                      new MouseButtonEventHandler(timeSlider_MouseLeftButtonUp),
                      true);

            SliderPlay.AddHandler(MouseLeftButtonDownEvent,
                      new MouseButtonEventHandler(timeSlider_MouseLeftButtonDown),
                      true);

        }
        void loadAlb()
        {
            stackAlbum = albums.Loading();
            Dispatcher.Invoke(() => DG_TabPlayLists.ItemsSource = stackAlbum);
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
        bool Max_Min = false;
        /// <summary>
        /// Развернуть свернуть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Collapse_Click(object sender, RoutedEventArgs e)
        {
            
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

            // Переход на вкладку
            TabMyMusic.SelectedIndex = 0;
            TabMyMusic.SelectedItem = TabMusic;
            TabMyMusic.SelectedItem = TabMyMusic.Items[1];
            myThread = new Thread(new ThreadStart(Open));
            myThread.Start();
        } // Button_OpenFile_Click

        private void Open()
        {
            /* Dispatcher.Invoke(() =>*/
            
           
            play = new Music();
            List<Music> musics = new List<Music>();
            musics = play.OpenPathToMusic();
            if(musics.Count != 0)
            {
                Dispatcher.Invoke(() => DG_TabMusic.ItemsSource = null);
                Dispatcher.Invoke(() => DG_TabMusic.ItemsSource = musics);
                stackMusic = musics;
            }
              
         
        }
        /// <summary>
        /// Запуск музыки по 2 нажатию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void DG_TabMusic_MouseDoubleClick(object sender, MouseButtonEventArgs e){
           ItemCollection MusicItem = null;
          
           play = (Music)(((System.Windows.Controls.DataGrid)sender).SelectedItem);

           if(TabMyMusic.SelectedItem == PlayListTB)
            {
                MusicItem = DG_TabPlayList.Items;
            }
               
          
           
           if (TabMyMusic.SelectedItem == TabMusic)
            {
                MusicItem = DG_TabMusic.Items;
            }
                

            if (play != null)
            {
                FileInfo fileInf = new FileInfo(play.full_name);
                var audio = TagLib.File.Create(Convert.ToString(fileInf));               
                TB_MUsic.Text = fileInf.Name;
                TB_album.Text = audio.Tag.Album;

                play.PlayMus(play.full_name, MusicItem,play);
            } // if
                            

        } // DG_TabMusic_MouseDoubleClick

        private void Button_OpenPath_Click(object sender, RoutedEventArgs e)
        {
            play.OpenFile();
        } //Button_OpenPath_Click


        #endregion

        #region MediaElem
        /// <summary>
        /// Открытие (проигрывание) Медиа Элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        List<string> FileMusic = new List<string>();
        Last_Music LM = new Last_Music();
        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
         
            FileMusic.Add(Media.Source.ToString());
            if (FileMusic.Count >= 9)
            {
                FileMusic.Insert(0, Media.Source.ToString());
                FileMusic.RemoveAt(FileMusic.Count-1);
                FileMusic.RemoveAt(FileMusic.Count - 1);

            }
            LM.Save(FileMusic);

            TotalTime = Media.NaturalDuration.TimeSpan;
                SliderPlay.Maximum = TotalTime.TotalSeconds;

                timerVideoTime.Interval = TimeSpan.FromSeconds(1);
                timerVideoTime.Tick += new EventHandler(play.timer_Tick);
                timerVideoTime.Start();
     
        } // Media_MediaOpened
        
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
            DG_TabPlayLists.ItemsSource = null;
            stackAlbum = albums.Loading();
            DG_TabPlayLists.ItemsSource = stackAlbum;
        } // Button_NewPLayList_Click

       

        private void DG_TabPlayLists_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            albums = (Albums)(((System.Windows.Controls.DataGrid)sender).SelectedItem);
            if (albums != null)
            {
                DG_TabPlayList.ItemsSource = null;
                DG_TabPlayList.ItemsSource = albums.MyMusicInPlayList;
                LB_Album.Content = albums.name;
            }
        }
        //ItemsControl.ItemsSource
        List<Albums> a1;
        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {
            albums = new Albums();
           a1 = new List<Albums>();
            var selcted = DG_TabPlayLists.SelectedItems;
            if (selcted != null)
            {
                foreach (var collect in selcted)
                {
                    a1.Add(albums = (Albums)collect);            
                }
                foreach (var collect in selcted)
                {
                    albums = (Albums)collect;
                    foreach(var stack in stackAlbum)
                    {
                        if (albums.FullName == stack.FullName)
                            stackAlbum.Remove(stack);
                        break;
                    }
                }
                
                DG_TabPlayLists.ItemsSource = null;
               
                DG_TabPlayLists.ItemsSource = stackAlbum;
                // создаем новый поток
                Thread myThread = new Thread(new ThreadStart(delete));
                myThread.Start(); // запускаем поток
               

            }
        }

        private void delete()
        {
            EditAlbum editAlbu = new EditAlbum();
            if (editAlbu.Delete(a1))
            {
                Dispatcher.Invoke(() => DG_TabPlayLists.ItemsSource = null);
                Dispatcher.Invoke(() => DG_TabPlayLists.ItemsSource = albums.Loading());
            }
        }

        

        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            play.Next();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            play.Back();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemCollection MusicItem = null;

            if (TabMyMusic.SelectedItem == PlayListTB)          
                MusicItem = DG_TabPlayList.Items;



            if (TabMyMusic.SelectedItem == TabMusic) 
                MusicItem = DG_TabMusic.Items;

            if (TabMyMusic.SelectedItem == PlayListTB)
                DG_TabPlayList.ItemsSource = play.Mix(MusicItem);

            if (TabMyMusic.SelectedItem == TabMusic)
                DG_TabMusic.ItemsSource =  play.Mix(MusicItem);
        }
       public bool Replay = false;
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if(Replay == false)
            {
                Replay = true;
                
            }
            else
            {
                Replay = false;
            }
        }

        private void DG_TabMusic_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            play = (Music)(((System.Windows.Controls.DataGrid)sender).SelectedItem);
            if(play != null)
            {
                RedactWin redact = new RedactWin(play.name, play.smalName, play.music_album_playlist, play.full_name);
                redact.ShowDialog();
            }
           
        } // DG_TabMusic_MouseRightButtonDown

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            SearchClass search = new SearchClass(TB_TextSearch.Text);
            DG_TabMusic.ItemsSource =  search.Search(DG_TabMusic.Items);
        }
    }
}
