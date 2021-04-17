﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccessTest
{
    [TestClass]
    public class PlaylistRepositoryTest
    {
        List<Playlist> listPlaylist;
        PlaylistRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            listPlaylist = new List<Playlist>() {
                new Playlist()
                {
                    Id = 1,
                    Name = "Cumbia",
                    Image = "",
                    Description = "Musica para previa"
                },
                new Playlist()
                {
                    Id = 2,
                    Name = "Reggaeton",
                    Image = "",
                    Description = "Musica gozada"
                }
            };
        }

        [TestMethod]
        public void AddPlaylist()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            context.Add(listPlaylist[0]);
            context.SaveChanges();
            repository = new PlaylistRepository(context);
            var initial = repository.GetAll().Count();

            repository.Add(listPlaylist[1]);
            var final = repository.GetAll().Count();
            context.Database.EnsureDeleted();

            Assert.AreEqual(initial + 1, final);
        }

        [TestMethod]
        public void GetOnePlaylist()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listPlaylist.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new PlaylistRepository(context);
            var playlist = repository.Get(listPlaylist[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(listPlaylist[0].Id, playlist.Id);
        }

        [TestMethod]
        public void UpdatePlaylist()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listPlaylist.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new PlaylistRepository(context);
            listPlaylist[0].Name = "Pop";
            repository.Update(listPlaylist[0]);
            var playlist = repository.Get(listPlaylist[0].Id);
            context.Database.EnsureDeleted();

            Assert.AreEqual(playlist.Name, "Pop");
        }

    }
}
