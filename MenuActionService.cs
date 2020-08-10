using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco
{
    public class MenuActionService
    {
        private List<MenuAction> menuActions = new List<MenuAction>();

        public void AddNewAction(int id, string actionName, string menuName)
        {
            MenuAction menuAction = new MenuAction() { Id = id, ActionName = actionName, MenuName = menuName };
            menuActions.Add(menuAction);
        }

        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();

            foreach(var menuAction in menuActions)
            {
                if(menuAction.MenuName==menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }
    }
}
