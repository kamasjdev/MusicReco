using MusicReco.App.Common;
using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }
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

        private void Initialize()
        {
            AddItem(new MenuAction(1, "Add new song to the database.", "Main"));
            AddItem(new MenuAction(2, "Recommend me something.", "Main"));
            AddItem(new MenuAction(3, "Like a chosen song.", "Main"));
            AddItem(new MenuAction(4, "Show more information about a chosen song.", "Main"));
            AddItem(new MenuAction(5, "Create new playlist or add songs to existing one.", "Main"));
            AddItem(new MenuAction(6, "Show your playlists.", "Main"));
            AddItem(new MenuAction(7, "Exit.", "Main"));

            AddItem(new MenuAction(1, "Hip-Hop", "AddSongMenu"));
            AddItem(new MenuAction(2, "Electronic", "AddSongMenu"));
            AddItem(new MenuAction(3, "Jazz", "AddSongMenu"));
            AddItem(new MenuAction(4, "Pop", "AddSongMenu"));
            AddItem(new MenuAction(5, "Rock", "AddSongMenu"));
            AddItem(new MenuAction(6, "Classical", "AddSongMenu"));

            AddItem(new MenuAction(1, "Based on an artist", "RecommendMenu"));
            AddItem(new MenuAction(2, "Based on a genre", "RecommendMenu"));
            AddItem(new MenuAction(3, "Based on a year of release", "RecommendMenu"));
           
            AddItem(new MenuAction(1, "Create new playlist.", "PlaylistMenu"));
            AddItem(new MenuAction(2, "Add songs to an existing playlist.", "PlaylistMenu"));    
        }
    }

}
