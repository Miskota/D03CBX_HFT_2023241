using D03CBX_HFT_2023241.Logic;
using D03CBX_HFT_2023241.Models;
using D03CBX_HFT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D03CBX_HFT_2023241.Test {

    [TestFixture]
    public class Tester {
        // Mock repository
        // 10 db Unit test
        // 5 Non-CRUD
        // 3 Create
        // 2 Szabadon választott

        AlbumLogic albumLogic;
        Mock<IRepository<Album>> mockAlbumRepo;

        WriterLogic writerLogic;
        Mock<IRepository<Writer>> mockWriterRepo;

        RecordLogic recordLogic;
        Mock<IRepository<Record>> mockRecordRepo;

        [SetUp]
        public void Init() {
            // Album Mock
            mockAlbumRepo = new Mock<IRepository<Album>>();
            mockAlbumRepo.Setup(m => m.ReadAll()).Returns(new List<Album>() {
                new Album("1#1#Album1#Rock#2002"),
                new Album("2#2#Album2#Pop#2004"),
                new Album("3#3#Album3#Jazz#1990"),
                new Album("4#4#Album4#Country#1990")
            }.AsQueryable());
            albumLogic = new AlbumLogic(mockAlbumRepo.Object);


            // Writer Mock
            mockWriterRepo = new Mock<IRepository<Writer>>();
            mockWriterRepo.Setup(m => m.ReadAll()).Returns(new List<Writer>() {

            }.AsQueryable());


            // Record Mock
            mockRecordRepo = new Mock<IRepository<Record>>();
            mockRecordRepo.Setup(m => m.ReadAll()).Returns(new List<Record>() {

            }.AsQueryable());
        }

        [Test]
        public void ListFromYearTest() {
            int year = 1990;
            var result = albumLogic.ListByYear(year);
            var expected = new List<string>() {
                "Album3",
                "Album4"
            };
            CollectionAssert.AreEqual(expected, result.ToList());
        }




        // Album Create tests
        [Test]
        public void CreateAlbumTestValid() {
            var album = new Album() {
                AlbumID = 1,
                WriterID = 1,
                AlbumName = "TestAlbum",
                Genre = Genre.Electro,
                ReleaseYear = 1990
            };
            albumLogic.Create(album);

            mockAlbumRepo.Verify(r => r.Create(album), Times.Once);
        }

        [Test]
        public void CreateAlbumTestInvalid() {
            var album = new Album() {
                AlbumID = 1,
                WriterID = 1,
                AlbumName = "X",
                Genre = Genre.Electro,
                ReleaseYear = 1990
            };

            try {
                albumLogic.Create(album);
            }
            catch (ArgumentException) {
            }

            mockAlbumRepo.Verify(r => r.Create(album), Times.Never);
        }
    }
}
