using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf_Music.Interfaces
{
    interface IPlayMusic{

        /// <summary>
        /// Название музыки
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// Расположение музыки
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        string album { get; set; }

        /// <summary>
        /// Картинка 
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Проигрывание музыки
        /// </summary>
        /// <param name="name"></param>
        /// <param name="FullName"></param>
        /// <param name="album"></param>
        /// <param name="Image"></param>
        /// <returns></returns>
        bool PlayMusic(string name, string FullName, string album, string Image);

    }
}
