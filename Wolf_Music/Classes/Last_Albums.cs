using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Interfaces;

namespace Wolf_Music.Classes
{
    class Last_Albums : Albums
    {
       public Last_Albums()
        {

        }

        public void save(string name)
        {
            if (File.Exists("PlayListSave.txt"))
            {
                using (StreamWriter sw = new StreamWriter("PlayListSave.txt", false, System.Text.Encoding.Default))
                {

                    sw.WriteLine(name);

                }

            }
            else
            {
                FileStream fs = File.Create("PlayListSave.txt", 1024);
                fs.Close();
                using (StreamWriter sw = new StreamWriter("PlayListSave.txt", false, System.Text.Encoding.Default))
                {

                    sw.WriteLine(name);

                }


            }

        } //  save

        public void load()
        {
            
        } // 
    } // Last_Albums
}
