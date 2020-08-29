using FluentAssertions;
using Moq;
using MusicReco.App.Abstract;
using MusicReco.App.Concrete;
using MusicReco.App.HelpersForManagers;
using MusicReco.App.Managers;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MusicReco.Tests.ManagerTests
{
    public class SongManagerTests
    {
        [Fact]
        public void Should_AddSong_When_CalledAddNewSong()
        {
            //Arrange
            Song song = new Song(1,"The Weeknd","The Hills",GenreName.Pop,2015,1,"");
            var mock = new Mock<ISongService>();
            mock.Setup(m => m.AddItem(song));
            var manager = new SongManager(new MenuView(), mock.Object);
            //Act
            var result = manager.AddNewSong(song);
            //Assert
            result.Should().NotBe(null);
            result.Should().NotBe(-1);
            result.Should().Be(song.Id);       
        }
        [Fact]
        public void Should_ReturnMinusOne_When_CalledAddNewSongWithNull()
        {
            //Arrange
            Song song = null;
            var mock = new Mock<ISongService>();
            mock.Setup(m => m.AddItem(song));
            var manager = new SongManager(new MenuView(), mock.Object);
            //Act
            var result = manager.AddNewSong(song);
            //Assert
            result.Should().NotBe(null);
            result.Should().Be(-1);
        }
        [Fact]
        public void Should_ReturnMinusOneOrSongId_When_Called_LikeChosenSong()
        {
            //Arrange
            Song song = new Song(3, "Alicja Majewska", "Żyć się chce",GenreName.Pop,2019, 1,"");
            var mock = new Mock<ISongService>();
            mock.Setup(m => m.CheckSongExistsInDatabase(song.Title)).Returns(song.Id);
            mock.Setup(m => m.CheckSongExistsInDatabase("Unknown Title")).Returns(-1);
            var manager = new SongManager(new MenuView(), mock.Object);
            //Act
            var result1 = manager.LikeChosenSong(song.Title);
            var result2 = manager.LikeChosenSong("Unknown Title");
            //Assert
            result1.Should().BeOfType(typeof(int));
            result1.Should().Be(song.Id);
            result1.Should().Be(3);

            result2.Should().BeOfType(typeof(int));
            result2.Should().Be(-1);
        }
        [Fact]
        public void Should_ReturnSongOrNull_When_CalledSearchSongToShowDetails()
        {
            //Arrange
            Song song = new Song(3, "Alicja Majewska", "Żyć się chce", GenreName.Pop, 2019, 1, "");
            var mock = new Mock<ISongService>();
            mock.Setup(m => m.CheckSongExistsInDatabase(song.Title)).Returns(song.Id);
            mock.Setup(m => m.CheckSongExistsInDatabase("Unknown Title")).Returns(-1);
            mock.Setup(m => m.GetSongById(song.Id)).Returns(song);
            var manager = new SongManager(new MenuView(), mock.Object);
            //Act
            var result1 = manager.SearchSongToShowDetails(song.Title);
            var result2 = manager.SearchSongToShowDetails("Unknown Title");
            //Assert
            result1.Should().NotBeNull();
            result1.Should().BeOfType(typeof(Song));
            result1.Should().Be(song);

            result2.Should().Be(null);
        }
    }
}
