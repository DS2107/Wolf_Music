using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Wolf_Music.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Wolf_Music.Classes
{
    class Albums : IAlbums
    {
        string __Image;
        string __name;
        string __FullName;
        List<Music> __MyMusicInPlayList;
        public string name {
            get { return __name; }
            set { __name = value; }
        }

        public List<Music> MyMusicInPlayList
        {
            get { return __MyMusicInPlayList; }
            set { __MyMusicInPlayList = value; }
        }

        public string Image {
            get { return __Image; }
            set { __Image = value; }
        }

        public string FullName
        {
            get { return __FullName; }
            set { __FullName = value; }
        }
        public double time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Список найденной музыки
        /// </summary>
        List<string> music;
        string line;
        public void Create(string image, string name,string imageName)
        {
            if (File.Exists("PlayList.txt"))
            {
                using (StreamReader sr = new StreamReader("PlayList.txt", System.Text.Encoding.Default))
                {
                    
                    line = sr.ReadLine();
                    sr.Close();
                    if (line == null)
                    {
                        MessageBox.Show("Specify the folder in which you will store playlists!!!");

                        var dialog = new System.Windows.Forms.FolderBrowserDialog();
                        dialog.ShowDialog();
                        using (StreamWriter sw = new StreamWriter("PlayList.txt", false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(dialog.SelectedPath);
                        }
                    }
                    else
                    {
                        string nameParth = line + @"\" + name+".wm3";
                        DirectoryInfo dirInfo = new DirectoryInfo(nameParth);
                        dirInfo.Create();
                        string fullName = nameParth + @"\" + name + ".txt";
                        FileStream fs = File.Create(fullName, 1024);
                        
                        fs.Close();

                        FileStream fs4 = File.Create(nameParth + @"\" + "Foto" +".txt", 1024);

                        fs4.Close();
                        string localPath = new Uri(image).LocalPath;
                     //   File.Copy(localPath, nameParth+@"\"+imageName,true);
                        using (StreamWriter sw = new StreamWriter(fullName, false, System.Text.Encoding.Default))
                        {
                            foreach(var v in music)
                            {
                                sw.WriteLine(v);
                            }     
                        }
                        using (StreamWriter sw = new StreamWriter(nameParth + @"\" + "Foto" + ".txt", false, System.Text.Encoding.Default))
                        {
                           
                                sw.WriteLine(image);
                            
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Specify the folder in which you will store playlists!!!");
                FileStream fs = File.Create("PlayList.txt", 1024);
                fs.Close();
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.ShowDialog();
                using (StreamWriter sw = new StreamWriter("PlayList.txt", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(dialog.SelectedPath);
                }
                if(music.Count != 0)
                {
                    using (StreamReader sr = new StreamReader("PlayList.txt", System.Text.Encoding.Default))
                    {

                        line = sr.ReadLine();
                        sr.Close();
                    }
                        string nameParth = line + @"\" + name;
                    DirectoryInfo dirInfo = new DirectoryInfo(nameParth);
                    dirInfo.Create();
                    string fullName = nameParth + @"\" + name + ".txt";
                    FileStream fs2 = File.Create(fullName, 1024);
                    fs2.Close();
                    FileStream fs3 = File.Create(nameParth + @"\" + "Foto" + ".txt", 1024);

                    fs3.Close();
                    // File.Copy(image, fullName);
                    using (StreamWriter sw = new StreamWriter(fullName, false, System.Text.Encoding.Default))
                    {
                        foreach (var v in music)
                        {
                            sw.WriteLine(v);
                        }
                    }
                    using (StreamWriter sw = new StreamWriter(nameParth + @"\" + "Foto" + ".txt", false, System.Text.Encoding.Default))
                    {

                        sw.WriteLine(image);

                    }
                }
            }
        }

        public bool searchMusic()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Music Files(*.MP3)|*.MP3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                music = new List<string>();
                foreach (String file in openFileDialog.FileNames)
                {
                    music.Add(file);
                }
             
            } // if  
            if (music != null)
                return true;
            else
                return true;
        }

        public List<Albums> Loading()
        {
            List<Albums> firstAlbum = new List<Albums>();
            using (StreamReader sr = new StreamReader("PlayList.txt", System.Text.Encoding.Default))
            {

                line = sr.ReadLine();
                sr.Close();
            }
            List<Music> musics = null;
            string[] dirs = Directory.GetDirectories(line);
            List<string> pathPlayList = new List<string>();
            foreach (string s in dirs)
            {
                if(s.Substring(s.Length - 4) == ".wm3")
                {
                    pathPlayList.Add(s);
                }
               
            }
            Music MusicInPlayList = new Music();
            foreach(var path in pathPlayList)
            {
              
                string[] filePLayList = Directory.GetFiles(path);
                FullName = path;
                foreach(var file in filePLayList)
                {
                    FileInfo m1 = new FileInfo(file);
                 
                    if (m1.Name == "Foto.txt")
                    {
                        using (StreamReader sr = new StreamReader(file, System.Text.Encoding.Default))
                        {
                           string line2 = sr.ReadLine();
                            Image = line2;
                            sr.Close();
                        }
                       
                    }
                    else if (m1.Name != "Foto.txt")
                    {
                        musics = new List<Music>();
                        name = Path.GetFileName(file);
                        name = name.Substring(0, name.Length - 4);
                        using (StreamReader sr = new StreamReader(file, System.Text.Encoding.Default))
                        {
                            string pathmusic;

                            while ((pathmusic = sr.ReadLine()) != null)
                            {
                                FileInfo fileInf = new FileInfo(pathmusic);
                                var audio = TagLib.File.Create(Convert.ToString(fileInf));


                                Music music = new Music
                                {
                                    smalName = audio.Tag.Title,
                                    name = fileInf.Name,
                                    full_name = fileInf.FullName,
                                    music_album_playlist = audio.Tag.Album,
                                    time = audio.Properties.Duration.ToString("mm\\:ss")
                                };
                                musics.Add(music);
                            }
                        }
                    }
                }
               
                Albums albums = new Albums
                {
                    Image = Image,
                    name = name,
                    MyMusicInPlayList = musics,
                    FullName = FullName
                };
               
                firstAlbum.Add(albums);
            }
            return firstAlbum;
        }

        public bool Delete(List<Albums> name)
        {
            System.Threading.Thread.Sleep(5000);
            foreach (var collect in name)
            {
               // FileStream fs = new FileStream(collect.Image, FileMode.Open);
               // fs.Close();
              
                Directory.Delete(collect.FullName,true);
                // albums.FullName
            }
            return true;
        }

        public bool Edit(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> MyMusic()
        {
            throw new NotImplementedException();
        }
    } // Albums
}
