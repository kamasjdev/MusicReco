using FluentAssertions;
using MusicReco.App.HelpersForManagers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MusicReco.Tests.HelpersForManagersTests
{
    public class HelperMethodsTests
    {
        [Fact]
        public void Should_ChangeFromStringToIntList_When_CalledChangeIdFromStrToList()
        {
            //Arrange
            string choice1 = "1,2,3,4";
            string choice2 = ",5,6,7,";
            string choice3 = ",8,,10,9,,";
            string choice4 = "10,11";
            string choice5 = "599,122,654,123,12,76,";
            var helper = new HelperMethods();
            //Act
            var result1 = helper.ChangeIdFromStrToList(choice1);
            var result2 = helper.ChangeIdFromStrToList(choice2);
            var result3 = helper.ChangeIdFromStrToList(choice3);
            var result4 = helper.ChangeIdFromStrToList(choice4);
            var result5 = helper.ChangeIdFromStrToList(choice5);
            //Assert
            result1.Should().BeOfType(typeof(List<int>));
            result1.Should().HaveCount(4);
            result1.Should().ContainInOrder(1, 2, 3, 4);

            result2.Should().HaveCount(3);
            result2.Should().ContainInOrder(5,6,7);

            result3.Should().HaveCount(3);
            result3.Should().ContainInOrder(8,10,9);

            result4.Should().HaveCount(2);
            result4.Should().ContainInOrder(10,11);

            result5.Should().HaveCount(6);
            result5.Should().ContainInOrder(599, 122, 654, 123, 12, 76);
        }
    }
}
