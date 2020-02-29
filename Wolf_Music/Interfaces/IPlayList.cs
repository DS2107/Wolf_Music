using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf_Music.Interfaces
{
    interface IPlayList
    {
        /// <summary>
        ///  Название плейлиста 
        /// </summary>
        string name { get; set; }


        /// <summary>
        ///  Картинка плейлиста
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Общее время плейлиста
        /// </summary>
        double time { get; set; }

        /// <summary>
        /// Музыка в плейлиста
        /// </summary>
        /// <returns></returns>
        List<string> MyMusic();

        /// <summary>
        /// Создать плейлиста
        /// </summary>
        /// <param name="image">Картинка плейлиста</param>
        /// <param name="name">Имя плейлиста</param>
        /// <returns></returns>
        bool Create(string image, string name);

        /// <summary>
        /// Удаление плейлиста
        /// </summary>
        /// <param name="name">Имя плейлиста</param>
        /// <returns></returns>
        bool Delete(string name);

        /// <summary>
        /// Реактирование плейлиста
        /// </summary>
        /// <param name="name">Имя плелиста</param>
        /// <returns></returns>
        bool Edit(string name); 
    }
}
