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
    public class PlaylistManagerTests
    {
        [Fact]
        public void Should_ReturnPlaylistId_When_CalledAddNewPlaylist()
        {
            //Arrange
            Playlist p = new Playlist(1, "Testing vol 1");
            var songServiceMock = new Mock<ISongService>();
            var mock = new Mock<IPlaylistService>();
            mock.Setup(m => m.AddItem(p));
            var playlistManager = new PlaylistManager(new MenuView(), songServiceMock.Object, mock.Object);
            //Act
            var result1 = playlistManager.AddNewPlaylist(p);
            //Assert
            result1.Should().BeOfType(typeof(int));
            result1.Should().Be(p.Id);
            result1.Should().Be(1);
        }
        [Fact]
        public void Should_ReturnMinusOne_When_CalledAddNewPlaylistWithNull()
        {
            //Arrange
            Playlist p = null;

            var mock = new Mock<IPlaylistService>();
            mock.Setup(m => m.AddItem(p));
            var playlistManager = new PlaylistManager(new MenuView(), new SongService(), mock.Object);
            //Act
            var result = playlistManager.AddNewPlaylist(p);
            //Assert
            result.Should().BeOfType(typeof(int));
            result.Should().Be(-1);
        }
        [Fact]
        public void Should_AddSongsToPlaylistAndReturnHowAdded_When_CalledUpdatePlaylist()
        {
            //Arrange
            Playlist playlistToUpdate = new Playlist(1, "Testing"); // its empty at the beginning.
            Song song1 = new Song(1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, ""); 
            Song song2 = new Song(2, "Alicja Majewska", "Żyć się chce", GenreName.Pop, 2019, 1, ""); 
            Song song3 = new Song(3, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, ""); 
            List<Song> allSongs = new List<Song>();
            allSongs.Add(song1);
            allSongs.Add(song2);
            allSongs.Add(song3);
            List<int> chosedSongIds = new List<int>(); 
            chosedSongIds.Add(1);
            chosedSongIds.Add(3);  //add ids of song1, song3.
            var playlistServiceMock = new Mock<IPlaylistService>();
            var songServiceMock = new Mock<ISongService>();
            songServiceMock.Setup(m => m.GetAllItems()).Returns(allSongs);
            playlistServiceMock.Setup(m => m.GetPlaylistById(1)).Returns(playlistToUpdate);
            playlistServiceMock.Setup(m => m.ReturnSongsAsidePlaylist(allSongs, playlistToUpdate)).Returns(allSongs);

            var playlistManager = new PlaylistManager(new MenuView(), songServiceMock.Object,playlistServiceMock.Object);
            //Act
            var result1 = playlistManager.UpdatePlaylist(1, chosedSongIds);
            //Assert
            result1.Should().BeOfType(typeof(int));
            result1.Should().Be(2);
            playlistToUpdate.Content.Should().HaveCount(2);
            playlistToUpdate.Content.Should().Contain(song1);
            playlistToUpdate.Content.Should().Contain(song3);
            playlistToUpdate.Content.Should().NotContain(song2);
        }
        [Fact]
        public void Should_ReturnZero_When_CalledUpdatePlaylistWithEmptySongIds()
        {
            //Arrange
            Playlist playlistToUpdate = new Playlist(1, "Testing"); // its empty at the beginning.
            Song song1 = new Song(1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            Song song2 = new Song(2, "Alicja Majewska", "Żyć się chce", GenreName.Pop, 2019, 1, "");
            Song song3 = new Song(3, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, "");
            List<Song> allSongs = new List<Song>();
            allSongs.Add(song1);
            allSongs.Add(song2);
            allSongs.Add(song3);

            var playlistServiceMock = new Mock<IPlaylistService>();
            var songServiceMock = new Mock<ISongService>();
            songServiceMock.Setup(m => m.GetAllItems()).Returns(allSongs);
            playlistServiceMock.Setup(m => m.GetPlaylistById(1)).Returns(playlistToUpdate);
            playlistServiceMock.Setup(m => m.ReturnSongsAsidePlaylist(allSongs, playlistToUpdate)).Returns(allSongs);

            var playlistManager = new PlaylistManager(new MenuView(), songServiceMock.Object, playlistServiceMock.Object);
            //Act
            var result = playlistManager.UpdatePlaylist(1, new List<int>());
            //Assert
            result.Should().BeOfType(typeof(int));
            result.Should().Be(0);
        }
    }
}
