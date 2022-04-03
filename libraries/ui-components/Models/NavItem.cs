using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components.Routing;

using static System.String;

namespace Components.UI.Models;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global", Justification = "Public API")]
public class NavItem
{
    public NavLinkMatch Match { get; init; } = NavLinkMatch.Prefix;

    public string OpenIconicIcon { get; init; } = "";

    public string Route { get; init; } = Empty;

    public string Text { get; init; } = Empty;
}