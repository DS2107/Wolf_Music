using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf_Music.Classes
{
    class EditAlbum
    {
        public EditAlbum()
        {

        }

        public bool Delete(List<Albums> name)
        {
            System.Threading.Thread.Sleep(5000);
            foreach (var collect in name)
            {
                // FileStream fs = new FileStream(collect.Image, FileMode.Open);
                // fs.Close();

                Directory.Delete(collect.FullName, true);
                // albums.FullName
            }
            return true;
        }

    }
}
