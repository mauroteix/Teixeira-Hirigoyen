using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;


namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestTrack
    {
        Track track;
        [TestInitialize]
        public void Initialize()
        {
            track = new Track();
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
    }
}
