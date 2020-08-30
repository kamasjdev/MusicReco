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
    public class PlaylistServiceTests
    {
        [Fact]
        public void Should_ReturnLastId_When_CalledGetLastId()
        {
            //Arrange
            PlaylistService playlistService = new PlaylistService();
            var countofSongs = playlistService.Items.Count;
            //Act
            var result = playlistService.GetLastId();
            //Assert
            result.Should().NotBe(null);
            result.Should().BeInRange(0, countofSongs);
            result.Should().Be(countofSongs);
        }
        [Fact]
        public void Should_ReturnSongsAsidePlaylist()
        {
            //Arrange
            PlaylistService playlistService = new PlaylistService();
            Song song1 = new Song(1, "The Weeknd", "The Hills", GenreName.Pop, 2015, 1, "");
            Song song2 = new Song(2, "Alicja Majewska", "Żyć się chce", genre: GenreName.Pop, 2019, 1, "");
            Song song3 = new Song(3, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, "");
            Song song4 = new Song(4, "Lady Pank", "Tacy sami", GenreName.Rock, 1988, 1, "");
            List <Song> databaseSongs = new List<Song>();
            databaseSongs.Add(song1);
            databaseSongs.Add(song2);
            databaseSongs.Add(song3);
            databaseSongs.Add(song4);
            Playlist playlistToUpdate = new Playlist(1, "Testing");
            playlistToUpdate.Content.Add(song1);
            playlistToUpdate.Content.Add(song4);
            //Act
            var result = playlistService.ReturnSongsAsidePlaylist(databaseSongs, playlistToUpdate);
            //Asset
            result.Should().BeOfType(typeof(List<Song>));
            result.Should().HaveCount(2);
            result.Should().Contain(song2);
            result.Should().Contain(song3);
            result.Should().ContainInOrder(song2, song3);
        }
        [Fact]
        public void Should_ReturnPlaylist_When_CalledGetPlaylistById()
        {
            //Arrange
            Playlist playlist1 = new Playlist(1, "Test vol. 1");
            Playlist playlist2 = new Playlist(2, "Test vol. 2");
            PlaylistService playlistService = new PlaylistService();
            playlistService.AddItem(playlist1);


            //Act
            var result1 = playlistService.GetPlaylistById(playlist1.Id);
            var result2 = playlistService.GetPlaylistById(playlist2.Id);
            //Assset
            result1.Should().BeOfType(typeof(Playlist));
            result1.Should().Be(playlist1);

            result2.Should().Be(null);
        }
        [Fact]
        public void Should_AddSongToPlaylist_When_CalledSuchMethod()
        {
            //Arrange
            Playlist playlist1 = new Playlist(1, "Test vol. 1");
            Playlist playlist2 = new Playlist(2, "Test vol. 2");
            Song song1 = new Song(1, "Selah Sue", "Crazy Vibes", GenreName.Jazz, 2011, 1, "");
            Song song2 = new Song(2, "Lady Pank", "Tacy sami", GenreName.Rock, 1988, 1, "");
            PlaylistService playlistService = new PlaylistService();
            //Act
            playlistService.AddSongToPlaylist(playlist1, song1);
            playlistService.AddSongToPlaylist(playlist1, song2);
            playlistService.AddSongToPlaylist(playlist2, song1);
            //Assert
            playlist1.Content.Should().HaveCount(2);
            playlist1.Content.Should().Contain(song1);
            playlist1.Content.Should().Contain(song2);
            playlist2.Content.Should().HaveCount(1);
            playlist2.Content.Should().Contain(song1);
        }
    }
}
