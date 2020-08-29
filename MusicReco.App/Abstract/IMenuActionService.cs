using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.Abstract
{
    public interface IMenuActionService
    {
        List<MenuAction> Items { get; set; }
        void AddItem(MenuAction item);
        void RemoveItem(MenuAction item);
        List<MenuAction> GetMenuActionsByMenuName(string menuName);
    }
}

