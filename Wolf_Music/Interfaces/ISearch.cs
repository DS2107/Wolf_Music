using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf_Music.Interfaces
{
    interface ISearch
    {
        /// <summary>
        /// То что ищем
        /// </summary>
        string searchName { get; set; }

        // То что нашли
        string NameSearch();


    }
}
