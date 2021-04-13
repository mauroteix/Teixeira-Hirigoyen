using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;


namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestTrack
    {
        Track track;
        Category category;
        Playlist playlist;
        [TestInitialize]
        public void Initialize()
        {
            track = new Track();
            playlist = new Playlist();
            category = new Category();
            
            category.Id = 1;
            category.Name = "Musica";
            
            playlist.Id = 1;
            playlist.Name = "Musica para Mauro";
            



        }

        [TestMethod]
        public void NameEmpty()
        {
            track.Name = "";
            Assert.IsTrue(track.NameEmpty());
        }

        [TestMethod]
        public void RegisterName()
        {
            track.Name = "Mauro";
            Assert.AreEqual("Mauro", track.Name);
        }

        [TestMethod]
        public void AuthorEmpty()
        {
            track.Author = "";
            Assert.IsTrue(track.AuthorEmpty());
        }

        [TestMethod]
        public void RegisterAuthor()
        {
            track.Author = "NTVG";
            Assert.AreEqual("NTVG", track.Author);
        }

        [TestMethod]
        public void ImageEmpty()
        {
            track.Image = "";
            Assert.IsTrue(track.ImageEmpty());
        }

        [TestMethod]
        public void RegisterImage()
        {
            track.Image = "http://www.google.com";
            Assert.AreEqual("http://www.google.com", track.Image);
        }

        [TestMethod]
        public void RegisterSound()
        {
            track.Sound = "http://www.youtube.com";
            Assert.AreEqual("http://www.youtube.com", track.Sound);
        }

        [TestMethod]
        public void SoundEmpty()
        {
            track.Sound = "";
            Assert.IsTrue(track.SoundEmpty());
        }
       /* [TestMethod]
        public void RegisterCategory()
        {
            track.Category =category;
            Assert.IsTrue(track.Category.Id == category.Id);
        }

        [TestMethod]
        public void RegisterPlayList()
        {
            track.Playlist = playlist;
            Assert.IsTrue(track.Playlist.Id == playlist.Id);
        }*/
    }
}
