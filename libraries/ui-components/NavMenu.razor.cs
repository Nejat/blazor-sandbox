using System;

using Components.UI.Models;

using Microsoft.AspNetCore.Components;

using static System.String;

using INavItems = System.Collections.Generic.IEnumerable<Components.UI.Models.NavItem>;

namespace Components.UI;

public class NavMenuBase : ComponentBase
{
    [Parameter]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public string Title { get; set; } = Empty;

    [Parameter]
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public INavItems NavItems { get; set; } = Array.Empty<NavItem>();

    private bool CollapseNavMenu { get; set; } = true;

    protected string? NavMenuCssClass =>
        CollapseNavMenu
            ? "collapse"
            : default;

    // ReSharper disable once UnusedMember.Global
    protected void ToggleNavMenu () => CollapseNavMenu = !CollapseNavMenu;
}