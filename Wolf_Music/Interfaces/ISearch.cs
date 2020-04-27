using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wolf_Music.Classes;

namespace Wolf_Music.Interfaces
{
    interface ISearch
    {
        /// <summary>
        /// То что ищем
        /// </summary>
        string searchName { get; set; }


        //List<Music> Search(ItemCollection musics);
        List<Music> Search(ItemCollection musics);
    }
}
