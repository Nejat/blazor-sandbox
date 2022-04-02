using Microsoft.AspNetCore.Components.Routing;

using static System.String;

namespace Components.UI.Models
{
    public class NavItem
    {
        public NavLinkMatch Match { get; set; }

        public string OpenIconicIcon { get; set; } = "";

        public string Route { get; set; } = Empty;

        public string Text { get; set; } = Empty;
    }
}
