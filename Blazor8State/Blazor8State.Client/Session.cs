using System.Runtime.CompilerServices;

namespace Blazor8State.Client
{
    /// <summary>
    /// Per-user session data. The object must be 
    /// serializable via JSON.
    /// </summary>
    public class Session : Dictionary<string, string>
    {
        public Action Changed { get; set; }

        public void OnChanged()
        {
            Changed?.Invoke();
        }
    }
}
