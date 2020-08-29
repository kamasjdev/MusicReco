using FluentAssertions;
using MusicReco.App.HelpersForManagers;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MusicReco.Tests.HelpersForManagersTests
{
    public class RecommendationTests
    {
        [Fact]
        public void Should_ReturnRecommendedSongs_When_CalledRecoBasedOnArtist()
        {
            //Arrange
            Recommendation reco = new Recommendation();
            List<Song> songs= new List<Song>();
            Song song1 = new Song(1, "Alicja Majewska", "Żyć się chce", GenreName.Pop, 2019, 1, "");
            Song song2 = new Song(2, "Alicja Majewska", "Byc kobietą", GenreName.Pop, 1999, 1, "");
            Song song3 = new Song(3, "Lady Pank", "Tacy sami", GenreName.Rock, 1988, 1, "");
            songs.Add(song1);
            songs.Add(song2);
            songs.Add(song3);
            //Act
            List<Song> result1 = new List<Song>();
            List<Song> result2 = new List<Song>();
            List<Song> result3 = new List<Song>();
            result1.AddRange(reco.RecoBasedOnArtist(songs, "Alicja Majewska"));
            result2.AddRange(reco.RecoBasedOnArtist(songs, "Lady Pank"));
            result3.AddRange(reco.RecoBasedOnArtist(songs, "Eminem"));

            //Assert
            result1.Should().HaveCount(2);
            result1.Should().ContainInOrder(song1,song2);
            
            result2.Should().HaveCount(1);
            result2.Should().Contain(song3);

            result3.Should().BeEmpty();
        }
        [Fact]
        public void Should_ReturnRecommendedSongs_When_CalledRecoBasedOnGenre()
        {
            //Arrange
            Recommendation reco = new Recommendation();
            List<Song> songs = new List<Song>();
            Song song1 = new Song(1, "Alicja Majewska", "Żyć się chce", GenreName.Pop, 2019, 1, "");
            Song song2 = new Song(2, "Alicja Majewska", "Byc kobietą", GenreName.Pop, 1999, 1, "");
            Song song3 = new Song(3, "Lady Pank", "Tacy sami", GenreName.Rock, 1988, 1, "");
            songs.Add(song1);
            songs.Add(song2);
            songs.Add(song3);
            //Act
            List<Song> result1 = new List<Song>();
            List<Song> result2 = new List<Song>();
            List<Song> result3 = new List<Song>();
            result1.AddRange(reco.RecoBasedOnGenre(songs, 4));
            result2.AddRange(reco.RecoBasedOnGenre(songs, 5));
            result3.AddRange(reco.RecoBasedOnGenre(songs, 1));

            //Assert
            result1.Should().HaveCount(2);
            result1.Should().ContainInOrder(song1, song2);

            result2.Should().HaveCount(1);
            result2.Should().Contain(song3);

            result3.Should().BeEmpty();
        }
        [Fact]
        public void Should_ReturnRecommendedSongs_When_CalledRecoBasedOnYear()
        {
            //Arrange
            Recommendation reco = new Recommendation();
            List<Song> songs = new List<Song>();
            Song song1 = new Song(1, "Alicja Majewska", "Żyć się chce", GenreName.Pop, 2019, 1, "");
            Song song2 = new Song(2, "Alicja Majewska", "Byc kobietą", GenreName.Pop, 1999, 1, "");
            Song song3 = new Song(3, "Lady Pank", "Tacy sami", GenreName.Rock, 1988, 1, "");
            songs.Add(song1);
            songs.Add(song2);
            songs.Add(song3);
            //Act
            List<Song> result1 = new List<Song>();
            List<Song> result2 = new List<Song>();
            List<Song> result3 = new List<Song>();
            result1.AddRange(reco.RecoBasedOnYear(songs, new int[2] {1999,2020}));
            result2.AddRange(reco.RecoBasedOnYear(songs, new int[2] { 1980, 1990 }));
            result3.AddRange(reco.RecoBasedOnYear(songs, new int[2] { 1900, 1950 }));

            //Assert
            result1.Should().HaveCount(2);
            result1.Should().ContainInOrder(song1, song2);

            result2.Should().HaveCount(1);
            result2.Should().Contain(song3);

            result3.Should().BeEmpty();
        }
    }
}
