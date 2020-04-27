using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wolf_Music.Interfaces;

namespace Wolf_Music.Classes
{
    class SearchClass : ISearch
    {
        string searchName_;

        public string searchName { get => searchName_;
            set => searchName_=value; }
        

        /// <summary>
        /// Класс для поиска музыки в списке
        /// </summary>
        /// <param name="name">Имя которое ввели</param>
        public SearchClass(string name)
        {
            searchName = name;
        } // SearchClass


        /// <summary>
        /// Метод, который ищет музыку
        /// </summary>
        /// <param name="searchName">Имя фвйла</param>
        /// <param name="musics">Список музыки в котором искать</param>
        /// <returns></returns>
        public List<Music> Search(ItemCollection musics)
        {
            string pattern = @"\b" + searchName + "\\S*";    //шаблон, по которому ищутся слова в строке
            Regex regex = new Regex(pattern); //регулярное выражение
            List<string> mymusic = new List<string>();
            List<Music> SerchMusic = new List<Music>();
            //получаем коллекцию соответствий и выводим на экран
            for (int i = 0; i < musics.Count; i++)
            {
                Music m = (Music)musics[i];
                foreach (var item in regex.Matches(m.name))
                    mymusic.Add(m.name);
                   
            }
            for (int i = 0; i < musics.Count; i++)
            {
                Music m = (Music)musics[i];
                foreach (var item in mymusic)
                {
                    if (item == m.name)
                        SerchMusic.Add(m);
                }
            }

                return SerchMusic;






            //            \b - означает, что нужно искать соответствия только в начале каждого слова в строке.
            //K - первая буква каждого слова
            //\S * -любые символы или их отсутствие(например если это просто единичная буква К в строке.


        } 
    }
}
