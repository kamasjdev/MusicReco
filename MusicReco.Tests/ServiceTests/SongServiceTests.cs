using FluentAssertions;
using MusicReco.App.Concrete;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MusicReco.Tests.ServiceTests
{
    public class SongServiceTests
    {
        [Fact]
        public void Should_ReturnMinusOneOrSongId_When_CheckSongExistsInDatabase()
        {
            //Arrange
            SongService songService = new SongService();
            int lastId = songService.GetLastId();
            Song song1 = new Song(lastId + 1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            Song song2 = new Song(lastId + 2, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, "");
            songService.AddItem(song2);

            //Act
            var result1 = songService.CheckSongExistsInDatabase(song1.Title);
            var result2 = songService.CheckSongExistsInDatabase(song2.Title);
            //Assert
            result1.Should().NotBe(null);
            result1.Should().BeOfType(typeof(int));
            result1.Should().Be(-1);

            result2.Should().NotBe(null);
            result2.Should().BeOfType(typeof(int));
            result2.Should().Be(song2.Id);
            //Clear
            songService.RemoveItem(song2);
        }
        [Fact]
        public void Should_GetLastIdOfAllSongs_When_CalledGetLastId()
        {
            //Arrange
            SongService songService = new SongService();
            var countofSongs = songService.Items.Count;
            //Act
            var result = songService.GetLastId();
            //Assert
            result.Should().NotBe(null);
            result.Should().BeInRange(0,countofSongs);
            result.Should().Be(countofSongs);
        }
        [Fact]
        public void Should_LikeSong_When_CalledLikeSong()
        {
            //Arrange
            var songService = new SongService();
            int lastId = songService.GetLastId();
            Song song1 = new Song(lastId + 1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            Song song2 = new Song(lastId + 2, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, "");
            songService.AddItem(song1);
            //Act
            songService.LikeSong(song1.Id);
            songService.LikeSong(song2.Id);
            //Assert
            song1.Likes.Should().Be(2);
            song2.Likes.Should().Be(1);
            //Clear
            songService.RemoveItem(song1);

        }
        [Fact]
        public void Should_GetSongOrNull_When_CalledGetSongById()
        {
            //Arrange
            var songService = new SongService();
            int lastId = songService.GetLastId();
            Song song1 = new Song(lastId + 1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            Song song2 = new Song(lastId + 2, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, "");
            songService.AddItem(song1);
            //Act
            var result1 = songService.GetSongById(song1.Id);
            var result2 = songService.GetSongById(song2.Id);
            //Assert
            result1.Should().BeOfType(typeof(Song));
            result1.Should().Be(song1);

            result2.Should().BeNull();
            //Clear
            songService.RemoveItem(song1);
        }
    }
}
