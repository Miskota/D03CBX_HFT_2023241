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
                new Writer("1#1980#John Smith"),
                new Writer("2#1995#Jane Doe"),
                new Writer("3#1972#Michael Johnson"),
                new Writer("4#1988#Emily White"),
                new Writer("5#1965#David Brown"),
                new Writer("6#1990#Sophie Green"),
                new Writer("7#1982#Robert Taylor"),
                new Writer("8#1978#Emma Anderson"),
                new Writer("9#1998#Daniel Miller"),
                new Writer("10#1985#Olivia Davis")
            }.AsQueryable());
            writerLogic = new WriterLogic(mockWriterRepo.Object);


            // Record Mock
            mockRecordRepo = new Mock<IRepository<Record>>();
            mockRecordRepo.Setup(m => m.ReadAll()).Returns(new List<Record>() {
                new Record("1#1#Song1#100#240#Rock"),
                new Record("2#1#Song2#150#180#Pop"),
                new Record("3#2#Song3#80#300#Jazz"),
                new Record("4#2#Song4#120#200#Metal"),
                new Record("5#3#Song5#200#180#Classic"),
                new Record("6#3#Song6#90#220#HipHop"),
                new Record("7#4#Song7#70#320#Electro"),
                new Record("8#4#Song8#180#160#Funk"),
                new Record("9#5#Song9#110#210#Country"),
                new Record("10#5#Song10#130#190#Disco")
            }.AsQueryable());
            recordLogic = new RecordLogic(mockRecordRepo.Object);
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

        [Test]
        public void Top10PlaysTest() {
            var expected = new List<Record>() {
                new Record("5#3#Song5#200#180#Classic"),
                new Record("8#4#Song8#180#160#Funk"),
                new Record("2#1#Song2#150#180#Pop"),
                new Record("10#5#Song10#130#190#Disco"),
                new Record("4#2#Song4#120#200#Metal"),
                new Record("9#5#Song9#110#210#Country"),
                new Record("1#1#Song1#100#240#Rock"),
                new Record("6#3#Song6#90#220#HipHop"),
                new Record("3#2#Song3#80#300#Jazz"),
                new Record("7#4#Song7#70#320#Electro")
            };

            var result = recordLogic.Top10Plays();

            CollectionAssert.AreEqual(expected.ToList(), result.ToList());
        }
    }
}
