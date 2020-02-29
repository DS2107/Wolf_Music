using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wolf_Music.Interfaces;

namespace Wolf_Music.Classes
{
    class PlayMusic : IPlayMusic, IMusic
    {
        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FullName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string album { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Image { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string full_name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string music_album_playlist { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IMusic.time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<string> GetFullNameFile()
        {
            throw new NotImplementedException();
        }

        public string GetFullPath()
        {
            throw new NotImplementedException();
        }


        bool IPlayMusic.PlayMusic(string name, string FullName, string album, string Image)
        {
            throw new NotImplementedException();
        }
    } // MusicPlay
}
