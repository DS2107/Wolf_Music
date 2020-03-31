using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Interfaces;
using TagLib;
using System.Media;
using System.Threading;
using System.Windows.Controls;

namespace Wolf_Music.Classes
{
    class Music : SearchClass, IMusic
    {
        string __name;
        string __time;
        string __full_name;
        string __album;
        string _smallName;

        
        public string name
        {
            get { return __name; }
            set { __name = value; }
        }

        public string smalName
        {
            get { return _smallName; }
            set { _smallName = value; }
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

        public static explicit operator Music(ItemCollection v)
        {
            throw new NotImplementedException();
        }
    } // Music
}
