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
        public void Should_SongId_When_CheckSongExistsInDatabase()
        {
            //Arrange
            SongService songService = new SongService();
            int lastId = songService.GetLastId();
            Song song = new Song(lastId + 1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            songService.AddItem(song);
            //Act
            var result2 = songService.CheckSongExistsInDatabase(song.Title);
            //Assert
            result2.Should().NotBe(null);
            result2.Should().BeOfType(typeof(int));
            result2.Should().Be(song.Id);
            //Clear
            songService.RemoveItem(song);
        }
        [Fact]
        public void Should_ReturnMinusOne_When_CalledCheckSongExistsInDatabase_SongDoesntExistInDatabase()
        {
            //Arrange
            SongService songService = new SongService();
            int lastId = songService.GetLastId();
            Song song1 = new Song(lastId + 1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            Song song2 = new Song(lastId + 2, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, "");
            songService.AddItem(song2);

            //Act
            var result = songService.CheckSongExistsInDatabase(song1.Title);
            //Assert
            result.Should().NotBe(null);
            result.Should().BeOfType(typeof(int));
            result.Should().Be(-1);
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
        public void Should_GetSong_When_CalledGetSongById()
        {
            //Arrange
            var songService = new SongService();
            int lastId = songService.GetLastId();
            Song song = new Song(lastId + 1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            songService.AddItem(song);
            //Act
            var result = songService.GetSongById(song.Id);
            //Assert
            result.Should().BeOfType(typeof(Song));
            result.Should().Be(song);
            //Clear
            songService.RemoveItem(song);
        }
        [Fact]
        public void Should_ReturnNull_When_CalledGetSongById_SongDoesntExistInDatabase()
        {
            //Arrange
            var songService = new SongService();
            int lastId = songService.GetLastId();
            Song song = new Song(lastId + 1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            //Act
            var result = songService.GetSongById(song.Id);
            //Assert
            result.Should().BeNull();
        }
    }
}
