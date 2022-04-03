using Microsoft.AspNetCore.Components;

namespace Razor.Components.Pages;

public class CounterBase : ComponentBase
{
    protected int CurrentCount { get; private set; }

    // ReSharper disable once UnusedMember.Global
    protected void IncrementCount() => CurrentCount++;
}