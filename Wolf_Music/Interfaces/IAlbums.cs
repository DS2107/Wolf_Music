using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Classes;

namespace Wolf_Music.Interfaces
{
    interface IAlbums{
        /// <summary>
        /// Название альбома 
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// Картинка альбома
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Общее время альбома
        /// </summary>
        double time { get; set; }     

        /// <summary>
        /// Создать альбом
        /// </summary>
        /// <param name="image">Картинка альбома</param>
        /// <param name="name">Название альбома</param>
        /// <returns></returns>
        void Create(string image, string name,string imageName); 

       

       
    } // IAlbums
}
