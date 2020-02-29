using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Interfaces;

namespace Wolf_Music.Classes
{
    class Last_PlayList : PlayList, IPlayList
    {
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Create(string image, string name)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string name)
        {
            throw new NotImplementedException();
        }

        public bool Edit(string name)
        {
            throw new NotImplementedException();
        }

        public List<string> MyMusic()
        {
            throw new NotImplementedException();
        }
    }
}
