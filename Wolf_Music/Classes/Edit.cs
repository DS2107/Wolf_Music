using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using Wolf_Music.Interfaces;

namespace Wolf_Music.Classes
{
    class Edit: EditInterface
    {
        string filepath_;

        public string filepath
        { get => filepath_;
          set => filepath_ = value;
        }


        public Edit(string path_file)
        {
            filepath = path_file;
        } // Edit

        /// <summary>
        /// редактирование
        /// </summary>
        public void FileEditTag(string NewFileName, string NewName, string NewAlbum)
        {
            var audio = TagLib.File.Create(Convert.ToString(filepath));
            audio.Tag.Album = NewAlbum;
            audio.Tag.Title = NewName;

            FileInfo fileInf = new FileInfo(filepath);
            string NewFileName1 = Path.Combine(fileInf.DirectoryName, NewFileName);
            System.IO.File.Move(fileInf.FullName, NewFileName1);
            audio.Save();

        } // FileEditTag


    } 
}
