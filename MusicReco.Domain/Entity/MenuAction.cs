﻿using MusicReco.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
        public string ActionName { get; set; }
        public string MenuName { get; set; }

        public MenuAction(int id, string actionName, string menuName)
        {
            Id = id;
            ActionName = actionName;
            MenuName = menuName;
        }
    }
}
