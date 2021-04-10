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
    }
}
