using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestCategory
    {

        Category category;
        Track track;
        Playlist playlist;
        Video video;
        CategoryTrack categoryTrack;
        CategoryVideo categoryVideo;
        PlaylistCategory playlistCategory;

        [TestInitialize]
        public void Initialize()
        {
            playlist = new Playlist
            {
                Id = 1,
                Name = "Musica para Mauro",
                Description = "De todo",
                PlaylistCategory = new List<PlaylistCategory>()
            };

            category = new Category
            {
                Name = "Dormir",
                Id = 1,
                CategoryTrack = new List<CategoryTrack>(),
                CategoryVideo = new List<CategoryVideo>(),
                PlaylistCategory = new List<PlaylistCategory>()
               
            };
            track = new Track
            {
                Id = 1,
                Image = "",
                Author = "",
                Name = "Mauro baila cumbia"
                
            };

            video = new Video
            {
                Id = 1,
                Author = "Rodrigo",
                LinkVideo = "www.youtube.com/baile",
                Name = "Baile",
                Hour = 1
            };
            categoryTrack = new CategoryTrack
            {
                IdCategory = category.Id,
                Category = category,
                IdTrack = track.Id,
                Track = track,
            };
            categoryVideo = new CategoryVideo
            {
                IdCategory = category.Id,
                Category = category,
                IdVideo = video.Id,
                Video = video,
            };
            playlistCategory = new PlaylistCategory
            {
                 IdCategory = category.Id,
                 Category = category,
                 IdPlaylist = playlist.Id,
                 Playlist = playlist
            };
            category.CategoryVideo.Add(categoryVideo);
            category.CategoryTrack.Add(categoryTrack);
            category.PlaylistCategory.Add(playlistCategory);

        }

        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Dormir", category.Name);
        }


        [TestMethod]
        public void RegisterCategoryTrack()
        {
            Assert.IsTrue(category.CategoryTrack.Count == 1);
        }

        [TestMethod]
        public void RegisterPlaylistCategory()
        {
            Assert.IsTrue(category.PlaylistCategory.Count == 1);
        }
        [TestMethod]
        public void RegisterImage()
        {
            category.Image = "http://www.google.com";
            Assert.AreEqual("http://www.google.com", category.Image);
        }

    }
}
