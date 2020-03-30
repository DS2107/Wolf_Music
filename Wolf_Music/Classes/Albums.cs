using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wolf_Music.Interfaces;
namespace Wolf_Music.Classes
{
    class Albums : SearchClass, IAlbums
    {
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      
        public string Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      

        public void Create(string image, string name)
        {
            if (File.Exists("PlayList.txt"))
            {
                using (StreamReader sr = new StreamReader("PlayList.txt", System.Text.Encoding.Default))
                {
                    string line;
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
                        DirectoryInfo dirInfo = new DirectoryInfo(line + @"\" + name);
                        dirInfo.Create();
                    }

                }
            }
            else
            {
                FileStream fs = File.Create("PlayList.txt", 1024);

            }
        }

        public bool Delete(string name)
        {
            throw new NotImplementedException();
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
