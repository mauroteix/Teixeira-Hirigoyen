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
        CategoryTrack categoryTrack;
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
                Description = "Facil para dormir",
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistCategory = new List<PlaylistCategory>()
               
            };
            track = new Track
            {
                Id = 1,
                Image = "",
                Author = "",
                Name = "Mauro baila cumbia"
                
            };
            categoryTrack = new CategoryTrack
            {
                IdCategory = category.Id,
                Category = category,
                IdTrack = track.Id,
                Track = track,
            };

            playlistCategory = new PlaylistCategory
            {
                 IdCategory = category.Id,
                 Category = category,
                 IdPlaylist = playlist.Id,
                 Playlist = playlist
            };
            category.CategoryTrack.Add(categoryTrack);
            category.PlaylistCategory.Add(playlistCategory);

        }

        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Dormir", category.Name);
        }

        [TestMethod]
        public void RegisterDescription()
        {
            Assert.AreEqual("Facil para dormir", category.Description);
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

    }
}
