using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestPlaylist
    {
        Playlist playlist;
        [TestInitialize]
        public void Initialize()
        {
            playlist = new Playlist();
      

        }

        [TestMethod]
        public void NameEmpty()
        {
            playlist.Name = "";
            Assert.IsTrue(playlist.NameEmpty());
        }
    }
}
