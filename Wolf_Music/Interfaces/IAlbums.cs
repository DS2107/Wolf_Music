using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        ///  Музыка в альбоме
        /// </summary>
        /// <returns></returns>
        List<string> MyMusic();

        /// <summary>
        /// Создать альбом
        /// </summary>
        /// <param name="image">Картинка альбома</param>
        /// <param name="name">Название альбома</param>
        /// <returns></returns>
        void Create(string image, string name,string imageName); 

        /// <summary>
        /// Удаление альбома, каждому альбому будет присваеваться уникальное имя и удаление по нему же
        /// </summary>
        /// <param name="name">Имя альбома</param>
        /// <returns></returns>
        bool Delete(string name);

        /// <summary>
        ///  Реактирование альбома
        /// </summary>      
        /// <param name="name">Имя альбома</param>
        /// <returns></returns>
        bool Edit(string name); 
    } // IAlbums
}
