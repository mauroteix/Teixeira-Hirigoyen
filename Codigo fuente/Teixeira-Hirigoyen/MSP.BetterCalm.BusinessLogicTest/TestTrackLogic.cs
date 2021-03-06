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
    public class TestTrackLogic
    {

        Track track;
        List<Track> trackList = new List<Track>();
        Mock<IData<Track>> repositoryTrack;
        Mock<IData<Category>> repositoryCategory;
        Mock<IData<Playlist>> repositoryPlaylist;
        TrackLogic trackLogic;
        List<Category> categoryList = new List<Category>();
        List<Playlist> playlistList = new List<Playlist>();
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
            track = new Track()
            {
                Id = 0,
                Name = "Gasolina",
                Author = "Daddy yankee",
                Sound = "www.youtube.com/daddyyanke/gasolina.mp3",
                Hour = 0,
                MinSeconds = 2.50,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            CategoryTrack categoryTrack = new CategoryTrack
            {
                IdCategory = 1
            };
            track.CategoryTrack.Add(categoryTrack);
            trackList.Add(track);
            repositoryTrack = new Mock<IData<Track>>();
            repositoryCategory = new Mock<IData<Category>>();
            repositoryPlaylist = new Mock<IData<Playlist>>();

            repositoryTrack.Setup(r => r.GetAll()).Returns(trackList);
            repositoryCategory.Setup(r => r.GetAll()).Returns(categoryList);
            repositoryPlaylist.Setup(r => r.GetAll()).Returns(playlistList);

            repositoryTrack.Setup(play => play.Get(0)).Returns(track);
            trackLogic = new TrackLogic(repositoryTrack.Object, repositoryCategory.Object, repositoryPlaylist.Object);

        }

        [TestMethod]
        public void GetTrack()
        {
            Track newTrack = trackLogic.Get(track.Id);
            Assert.AreEqual(track, newTrack);
        }

        [TestMethod]
        public void GetTrackNotExist()
        {
            Assert.ThrowsException<EntityNotExists>(() => trackLogic.Get(1));
        }

        [TestMethod]
        public void AddTrackOk()
        {
            Track trackToAdd = new Track()
            {
                Id = 1,
                Name = "Fiel",
                Author = "Wisin",
                Sound = "www.youtube.com/fiel.mp3",
                Hour = 0,
                MinSeconds = 2.60,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };

            CategoryTrack categoryTrack = new CategoryTrack
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>()
                },
                IdCategory = 1,
                IdTrack = 1,
                Track = trackToAdd

            };
            trackToAdd.CategoryTrack.Add(categoryTrack);
            trackLogic.Add(trackToAdd);
        }

        [TestMethod]
        public void AddTrackNameEmpty()
        {
            Track trackToAdd = new Track()
            {
                Id = 2,
                Name = "",
                Author = "Daddy",
                Sound = "www.youtube.com/fielid.mp3",
                Hour = 0,
                MinSeconds = 2.80,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };

            CategoryTrack categoryTrack = new CategoryTrack
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>()
                },
                IdCategory = 1,
                IdTrack = 2,
                Track = trackToAdd

            };
            trackToAdd.CategoryTrack.Add(categoryTrack);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => trackLogic.Add(trackToAdd));
        }

        [TestMethod]
        public void AddTrackSoundEmpty()
        {
            Track trackToAdd = new Track()
            {
                Id = 2,
                Name = "Noite",
                Author = "Daddy",
                Sound = "",
                Hour = 0,
                MinSeconds = 2.80,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };

            CategoryTrack categoryTrack = new CategoryTrack
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>()
                },
                IdCategory = 1,
                IdTrack = 2,
                Track = trackToAdd

            };
            trackToAdd.CategoryTrack.Add(categoryTrack);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => trackLogic.Add(trackToAdd));
        }

        [TestMethod]
        public void AddTrackWithoutCategoryFail()
        {
            Track trackToAdd = new Track()
            {
                Id = 2,
                Name = "Gasoline",
                Author = "Daddy",
                Sound = "www.youtube.com/fielid.mp3",
                Hour = 0,
                MinSeconds = 2.80,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => trackLogic.Add(trackToAdd));
        }


        [TestMethod]
        public void DeleteTrack()
        {
            trackLogic.Delete(track);
            var getTrack = trackLogic.Get(track.Id);
        }

        [TestMethod]
        public void DeleteTrackNotExist()
        {
            Track trackToDelete = new Track()
            {
                Id = 10,
                Name = "Perreo",
                Author = "Daddy Yankee",
                Sound = "www.youtube.com/hola.mp3",
                Hour = 0,
                MinSeconds = 2.80,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            Assert.ThrowsException<EntityNotExists>(() => trackLogic.Delete(trackToDelete));
        }

        [TestMethod]
        public void UpdateTrack()
        {
            Track trackToAdd = new Track()
            {
                Id = 1,
                Name = "Joseee",
                Author = "Wisin",
                Sound = "www.youtube.com/fiel.mp3",
                Hour = 0,
                MinSeconds = 2.60,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };

            CategoryTrack categoryTrack = new CategoryTrack
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>()
                },
                IdCategory = 1,
                IdTrack = 1,
                Track = trackToAdd

            };
            trackToAdd.CategoryTrack.Add(categoryTrack);
            trackLogic.Update(trackToAdd, track.Id);
            Assert.AreEqual("Joseee", trackLogic.Get(0).Name);
        }

        [TestMethod]
        public void GetAllTrack()
        {
            List<Track> tracks = trackLogic.GetAll();
            Assert.AreEqual(tracks.Count, 1);
        }
        [TestMethod]
        public void ExistTrackByName()
        {
            track = new Track()
            {
                Id = 0,
                Name = "Gasolina",
                Author = "Daddy yankee",
                Sound = "www.youtube.com/daddyyanke/gasolina.mp3",
                Hour = 0,
                MinSeconds = 2.50,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            Assert.IsTrue(trackLogic.ExistTrackByName(track));
        }
        [TestMethod]
        public void ValidateTrack()
        {
            Assert.IsTrue(trackLogic.ValidateTrackToAdd(track));
        }
        [TestMethod]
        public void ValidateTrackNameEmpty()
        {
            track.Name = "";
            Assert.IsFalse(trackLogic.ValidateTrackToAdd(track));
        }
        [TestMethod]
        public void ValidateTrackSoundEmpty()
        {
            track.Sound = "";
            Assert.IsFalse(trackLogic.ValidateTrackToAdd(track));
        }
        [TestMethod]
        public void GetTrackByName()
        {
            Track newtrack = trackLogic.GetTrackByName("Gasolina");
            Assert.AreEqual(newtrack.Name, "Gasolina");
        }
    }
}
