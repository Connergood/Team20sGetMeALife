using System;
using System.Collections.Generic;
using System.Text;

namespace GetMeALife.Models
{
    public enum MenuItemType
    {
        NewLife,
        Host,
        LifeChoices,
        SearchRadius,
        CostLimit,
        Notify,
        LogOut
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
