using System;

using Components.UI.Models;

using Microsoft.AspNetCore.Components;

using INavItems = System.Collections.Generic.IEnumerable<Components.UI.Models.NavItem>;

namespace Sandbox.Blazor.Shared;

public class MainLayoutBase : LayoutComponentBase
{
    [Inject]
    protected INavItems NavItems { get; private set; } = Array.Empty<NavItem>();
}