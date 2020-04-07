using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wolf_Music.Classes;

namespace Wolf_Music.Interfaces
{
    interface IMusic{

    /// <summary>
    ///  Короткое название музыки
    /// </summary>
    string name { get; set; } 
    
    /// <summary>
    /// Полный путь
    /// </summary>
    string full_name { get; set; } 
        
    /// <summary>
    /// альбом или плейлист в котором песня находится
    /// </summary>
    string music_album_playlist { get; set; } 

    /// <summary>
    /// Время песни
    /// </summary>
    string time { set; get; }

    List<Music> OpenPathToMusic();

    void  PlayMus(string nameMusic, ItemCollection collection, Music play);

    void OpenFile();

    } // IMusic
}
