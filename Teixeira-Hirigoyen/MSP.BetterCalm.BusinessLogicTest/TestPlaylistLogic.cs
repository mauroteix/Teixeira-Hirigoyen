using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestPlaylistLogic
    {
        Playlist playlist;
        List<Playlist> playlistList = new List<Playlist>();
        Mock<IData<Playlist>> repositoryPlaylist;
        PlaylistLogic playlistLogic;

        [TestInitialize]
        public void Initialize()
        {
            playlist = new Playlist()
            {
                Id = 0,
                Name = "Reggaeton",
                Description = "Old hits, daddy yankee",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            playlistList.Add(playlist);
            repositoryPlaylist = new Mock<IData<Playlist>>();
            repositoryPlaylist.Setup(r => r.GetAll()).Returns(playlistList);

            repositoryPlaylist.Setup(play => play.Get(0)).Returns(playlist);
            repositoryPlaylist.Setup(play => play.Add(playlist));
            playlistLogic = new PlaylistLogic(repositoryPlaylist.Object);
      
        }

        [TestMethod]
        public void GetPlaylist()
        {
            Playlist newPlaylist = playlistLogic.Get(playlist.Id);
            Assert.AreEqual(playlist, newPlaylist);
        }

        [TestMethod]
        public void GetPlaylistNotExist()
        {
            Assert.ThrowsException<EntityNotExists>(() => playlistLogic.Get(1));
        }

        [TestMethod]
        public void AddPlaylistOk()
        {
            Playlist playlistToAdd = new Playlist()
            {
                Id = 1,
                Name = "Baile",
                Description = "Lo mejor 2021",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };

            PlaylistCategory playlistCategory = new PlaylistCategory
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>()
                },
                IdCategory = 1,
                IdPlaylist = 1,
                Playlist = playlistToAdd

            };
            playlistToAdd.PlaylistCategory.Add(playlistCategory);
            playlistLogic.Add(playlistToAdd);
        }

        [TestMethod]
        public void AddPlaylistNameEmpty()
        {
            Playlist playlistToAdd = new Playlist()
            {
                Id = 1,
                Name = "",
                Description = "Lo mejor 2021",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };

            PlaylistCategory playlistCategory = new PlaylistCategory
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>()
                },
                IdCategory = 1,
                IdPlaylist = 1,
                Playlist = playlistToAdd

            };
            playlistToAdd.PlaylistCategory.Add(playlistCategory);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => playlistLogic.Add(playlistToAdd));
        }

        [TestMethod]
        public void AddPlaylistDescriptionLengthWrong()
        {
            Playlist playlistToAdd = new Playlist()
            {
                Id = 1,
                Name = "Variado",
                Description = "Lo mejor 2021 del mundo mundial mundoooooooooooooooooooooooooooo ksdksdsd   sdsd sd s ds d sd   sd sdsdsd sd sd sd  ds d" +
                " sdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsds sdsds sdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsdsd",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };

            PlaylistCategory playlistCategory = new PlaylistCategory
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>()
                },
                IdCategory = 1,
                IdPlaylist = 1,
                Playlist = playlistToAdd

            };
            playlistToAdd.PlaylistCategory.Add(playlistCategory);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => playlistLogic.Add(playlistToAdd));
        }

        [TestMethod]
        public void AddPlaylistCategoryEmpty()
        {
            Playlist playlistToAdd = new Playlist()
            {
                Id = 1,
                Name = "Variado",
                Description = "Lo mejor 2021",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => playlistLogic.Add(playlistToAdd));
        }
    }
}
