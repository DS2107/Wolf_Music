﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Interfaces;
using TagLib;
using System.Media;
using System.Threading;
using System.Windows.Controls;
using System.Windows;
using Wolf_Music.Classes;

namespace Wolf_Music.Classes
{
    class Music : SearchClass, IMusic
    {
        string __name;
        string __time;
        string __full_name;
        string __album;
        string __smallName;

        
        public string name
        {
            get { return __name; }
            set { __name = value; }
        }

        public string smalName
        {
            get { return __smallName; }
            set { __smallName = value; }
        }


        public string full_name
        {
            get { return __full_name; }
            set { __full_name = value; }
        }
        public string music_album_playlist
        {
            get { return __album; }
            set { __album = value; }
        }
        public string time
        {
            get { return __time; }
            set { __time = value; }
        }

        

        public List<Music> OpenPathToMusic()
        {
            string rootFolder = "";

            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Threading.Thread thread = new System.Threading.Thread(() => dialog.ShowDialog());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
           

            // Получаем Выбранную папку
            rootFolder = dialog.SelectedPath;
            
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
            } // if

           return SortMusic(OpenMusic);

        } // OpenPathToMusic

        private List<Music> SortMusic(List<string> path)
        {
            List<Music> musics = new List<Music>();
            foreach (var fileMusic in path)
            {

                FileInfo fileInf = new FileInfo(fileMusic);
                var audio = TagLib.File.Create(Convert.ToString(fileInf));
                if (audio.Tag.Album == null)
                    music_album_playlist = "NO album";
                else
                    music_album_playlist = audio.Tag.Album;

                Music music = new Music
                {
                    smalName = audio.Tag.Title,
                    name = fileInf.Name,
                    full_name = fileInf.FullName,
                    music_album_playlist = music_album_playlist,
                    time = audio.Properties.Duration.ToString("mm\\:ss")
                };
                musics.Add(music);
            }

            return musics;
        } // SortMusic


        ItemCollection collectionMyMusic;
        // Запуск музыки
        Music obj;

        List<Music> LastMusic;
        public void PlayMus(string nameMusic, ItemCollection collection, Music play)
        {
            LastMusic = new List<Music>();
            MainWindow.Instance.Media.Source = new Uri(nameMusic);
            obj = play;
            MainWindow.Instance.Media.Play();
         
            collectionMyMusic = collection;

        } // PlayMus

        internal void timer_Tick(object sender, EventArgs e)
        {
            ItemCollection MusicItem;
            if (MainWindow.Instance.TabMyMusic.SelectedItem == MainWindow.Instance.PlayListTB)
            {
               MusicItem = MainWindow.Instance.DG_TabPlayList.Items;
                collectionMyMusic = MusicItem;
            }

            if (MainWindow.Instance.TabMyMusic.SelectedItem == MainWindow.Instance.TabMusic)
            {
                MusicItem = MainWindow.Instance.DG_TabMusic.Items;
                collectionMyMusic = MusicItem;
            }
            try
            {
                // Check if the movie finished calculate it's total time
                if (MainWindow.Instance.Media.NaturalDuration.TimeSpan.TotalSeconds > 0)
                {
                    if (MainWindow.Instance.TotalTime.TotalSeconds > 0)
                    {
                        // Updating time slider
                        MainWindow.Instance.SliderPlay.Value = MainWindow.Instance.Media.Position.TotalSeconds;
                        MainWindow.Instance.timemus.Text = MainWindow.Instance.Media.Position.ToString("mm\\:ss");
                    } // if

                } //if
                if (MainWindow.Instance.TotalTime == MainWindow.Instance.Media.Position)
                {
                    if (collectionMyMusic != null)
                        NextMusic(collectionMyMusic);
                } // if
            }
            catch
            {

            }
        }

        Last_Music LM;
        List<string> FileMusic = new List<string>();
        private void NextMusic(ItemCollection items)
        {
            LM = new Last_Music();
            for (int i = 0; i < items.Count; i++)
            {
                var item = (Music)items[i];
                if (new Uri(item.full_name) == MainWindow.Instance.Media.Source)
                {
                    i++;
                    var itemNext = (Music)items[i];
                    MainWindow.Instance.Media.Source = new Uri(itemNext.full_name);
                    FileInfo fileInf = new FileInfo(itemNext.full_name);
                    var audio = TagLib.File.Create(Convert.ToString(fileInf));
                    MainWindow.Instance.TB_MUsic.Text = itemNext.name;
                    MainWindow.Instance.TB_album.Text = audio.Tag.Album;
                    if (LastMusic.Count == 5)
                    {
                        LastMusic.Insert(0, item);
                        LastMusic.RemoveAt(LastMusic.Count - 1);
                        LastMusic.Add(item);

                        FileMusic.Insert(0, item.full_name);
                        FileMusic.RemoveAt(FileMusic.Count - 1);
                        FileMusic.Add(item.full_name);
                        LM.Save(FileMusic);

                    }
                    else
                    {
                        LastMusic.Add(item);
                        FileMusic.Add(item.full_name);
                        LM.Save(FileMusic);
                    }
                }
            }
        } // NextMusic

        public void OpenFile()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {

                string filename = openFileDialog.FileName;
                FileInfo fileInf = new FileInfo(filename);
                var audio = TagLib.File.Create(Convert.ToString(fileInf));
                MainWindow.Instance.Media.Source = new Uri(fileInf.FullName);
                MainWindow.Instance.TB_MUsic.Text = fileInf.Name;
                MainWindow.Instance.TB_album.Text = audio.Tag.Album;

                MainWindow.Instance.Media.Play();

            } // if  
        }
    } // Music
}
