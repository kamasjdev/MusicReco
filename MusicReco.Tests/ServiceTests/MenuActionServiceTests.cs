using FluentAssertions;
using MusicReco.App.Concrete;
using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MusicReco.Tests.ServiceTests
{
    public class MenuActionServiceTests
    {
        [Fact]
        public void Should_GetEmptyList_When_GivenEmptyString()
        {
            //Arrange
            MenuActionService menuActionService = new MenuActionService();

            //Act
            List<MenuAction> result = menuActionService.GetMenuActionsByMenuName("");

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Should_GetMenuActions_When_GivenMenuName()
        {
            //Arrange
            MenuAction menuAction1 = new MenuAction(1, "Check it", "Test");
            MenuAction menuAction2 = new MenuAction(2, "Check it one more", "Test");
            MenuActionService menuActionService = new MenuActionService();
            menuActionService.AddItem(menuAction1);
            menuActionService.AddItem(menuAction2);

            //Act
            List<MenuAction> result = menuActionService.GetMenuActionsByMenuName("Test");

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().StartWith(menuAction1);

            //Clear
            menuActionService.RemoveItem(menuAction1);
            menuActionService.RemoveItem(menuAction2);
        }
    }
}
