using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Interfaces;
using TagLib;
using System.Media;

namespace Wolf_Music.Classes
{
    class Music : SearchClass,IMusic
    {
         string __name;
        string __time;
        string __full_name;
        string __album;   
        public string name
        {
              get { return __name; }
              set { __name = value;  }  
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

        public List<string> GetFullNameFile()
        {
            throw new NotImplementedException();
        }

        public string GetFullPath()
        {
            throw new NotImplementedException();
        }

        public List<Music> SortSeachMusic(List<string> path)
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
                    name = fileInf.Name,
                    full_name = fileInf.FullName,
                    music_album_playlist = music_album_playlist,
                    time = audio.Properties.Duration.ToString("mm\\:ss")
                };
                musics.Add(music);
            }
           
            
           
            return musics;
        }

       
    } // Music
}
