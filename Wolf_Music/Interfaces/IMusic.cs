using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    
    /// <summary>
    ///  Возвращает путь выбранной папки из которой будут проигрываться песни
    /// </summary>
    /// <returns></returns>
    string GetFullPath(); 

    /// <summary>
    /// Возвращает путь выбранных файлов 
    /// </summary>
    /// <returns></returns>
    List<string> GetFullNameFile();
    
    

    } // IMusic
}
