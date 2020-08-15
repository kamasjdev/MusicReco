using MusicReco.App.Common;
using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();

            foreach (var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }
    }
}
