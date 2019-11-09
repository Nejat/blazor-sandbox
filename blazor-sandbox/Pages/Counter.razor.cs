using Microsoft.AspNetCore.Components;

namespace Sandbox.Blazor.Pages
{
    public class CounterBase : ComponentBase
    {
        protected int CurrentCount { get; private set; }

        protected void IncrementCount() => CurrentCount++;
    }
}
