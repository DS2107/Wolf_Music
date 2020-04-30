using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Classes;


namespace Wolf_Music.Classes
{
    class Last_Music : Music
    {
        public Last_Music()
        {

        }
        public void Save(List<string> LastMusic)
        {
            if (System.IO.File.Exists("LastMusic.txt"))
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("LastMusic.txt", false, System.Text.Encoding.Default))
                {
                    foreach (var music in LastMusic)
                    {
                        sw.WriteLine(music);
                    }

                }
            }
            else
            {
                System.IO.FileStream fs = System.IO.File.Create("LastMusic.txt", 1024);
                fs.Close();
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("LastMusic.txt", false, System.Text.Encoding.Default))
                {
                    foreach (var music in LastMusic)
                    {
                        sw.WriteLine(music);
                    }

                }
            }
           
        }


        public List<Music> Load()
        {
            string pathmusic;
            List < Music > LastMusic = new List<Music>();
            if (File.Exists("LastMusic.txt"))
            {


                using (System.IO.StreamReader sr = new System.IO.StreamReader("LastMusic.txt", System.Text.Encoding.Default))
                {

                    while ((pathmusic = sr.ReadLine()) != null)
                    {
                        pathmusic = pathmusic.Remove(0, 8);
                        System.IO.FileInfo fileInf = new System.IO.FileInfo(pathmusic);
                        var audio = TagLib.File.Create(Convert.ToString(fileInf));


                        Music music = new Music
                        {
                            smalName = audio.Tag.Title,
                            name = fileInf.Name,
                            full_name = fileInf.FullName,
                            music_album_playlist = audio.Tag.Album,
                            time = audio.Properties.Duration.ToString("mm\\:ss")
                        };
                        LastMusic.Add(music);
                    }
                }
                return LastMusic;
            }
            else
            {
                return null;
            }
                
        }
    } // Last_Music
}
