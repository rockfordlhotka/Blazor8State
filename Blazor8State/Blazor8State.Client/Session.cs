namespace Blazor8State.Client
{
    /// <summary>
    /// Per-user session data. The object must be 
    /// serializable via JSON.
    /// </summary>
    public class Session : Dictionary<string, string>
    { }
}
