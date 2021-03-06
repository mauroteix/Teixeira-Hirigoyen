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
using UruguayNatural.HandleError;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestPlaylistLogic
    {
        Playlist playlist;
        List<Playlist> playlistList = new List<Playlist>();
        Mock<IData<Playlist>> repositoryPlaylist;
        Mock<IData<Category>> repositoryCategory;
        Mock<IData<Track>> repositoryTrack;
        Mock<IData<Video>> repositoryVideo;
        PlaylistLogic playlistLogic;
        TrackLogic trackLogic;
        VideoLogic videoLogic;
        List<Category> categoryList = new List<Category>();
        List<Track> trackList = new List<Track>();
        Category category;
        Category secondCategory;

        [TestInitialize]
        public void Initialize()
        {
            category = new Category()
            {
                Id = 1,
                Name = "Dormir"
            };
            secondCategory = new Category()
            {
                Id = 2,
                Name = "Musica"
            };
            categoryList = new List<Category>();
            categoryList.Add(category);
            categoryList.Add(secondCategory);
            playlist = new Playlist()
            {
                Id = 0,
                Name = "Reggaeton",
                Description = "Old hits, daddy yankee",
                PlaylistCategory = new List<PlaylistCategory>() {
                     new PlaylistCategory
                     {
                          Category = category,
                          IdCategory = category.Id
                     }
                },
                PlaylistTrack = new List<PlaylistTrack>(),
                PlaylistVideo = new List<PlaylistVideo>()
            };
            playlistList.Add(playlist);
            repositoryPlaylist = new Mock<IData<Playlist>>();
            repositoryCategory = new Mock<IData<Category>>();
            repositoryTrack = new Mock<IData<Track>>();
            repositoryVideo = new Mock<IData<Video>>();

            repositoryPlaylist.Setup(r => r.GetAll()).Returns(playlistList);
            repositoryCategory.Setup(r => r.GetAll()).Returns(categoryList);
            repositoryTrack.Setup(r => r.GetAll()).Returns(trackList);

            repositoryPlaylist.Setup(play => play.Get(0)).Returns(playlist);
            repositoryPlaylist.Setup(play => play.Add(playlist));
            trackLogic = new TrackLogic(repositoryTrack.Object, repositoryCategory.Object,repositoryPlaylist.Object);
            videoLogic = new VideoLogic(repositoryVideo.Object, repositoryCategory.Object, repositoryPlaylist.Object);
            playlistLogic = new PlaylistLogic(repositoryPlaylist.Object, repositoryCategory.Object,trackLogic,videoLogic, repositoryTrack.Object, repositoryVideo.Object);
      
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
            playlistToAdd.PlaylistVideo = new List<PlaylistVideo>();
            playlistToAdd.PlaylistTrack = new List<PlaylistTrack>();
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

        [TestMethod]
        public void AddPlaylistValidateCategoryUnique()
        {
            Playlist playlistToAdd = new Playlist()
            {
                Id = 1,
                Name = "New music",
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
            PlaylistCategory playlistCategory2 = new PlaylistCategory
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
            playlistToAdd.PlaylistCategory.Add(playlistCategory2);
            Assert.ThrowsException<EntityAlreadyExist>(() => playlistLogic.Add(playlistToAdd));
        }

        [TestMethod]
        public void GetAllPlaylist()
        {
            List<Playlist> categoryList = playlistLogic.GetAll();
            Assert.AreEqual(categoryList.Count, 1);
        }

        [TestMethod]
        public void DeletePlaylist()
        {
            playlistLogic.Delete(playlist);
            var getLodg = playlistLogic.Get(playlist.Id);
        }

        [TestMethod]
        public void UpdatePlaylist()
        {
            playlist.Name = "Cumbia";
            playlist.Description = "Lo nuevo";
            playlistLogic.Update(playlist, playlist.Id);
        }

        
    }
}
