namespace Blazor8State.Client
{
    /// <summary>
    /// Per-user session data. The object must be 
    /// serializable via JSON.
    /// </summary>
    public class Session : Dictionary<string, string>
    {
        /// <summary>
        /// Gets or sets the Session Id value.
        /// </summary>
        public string SessionId { get; set; } = string.Empty;
        public bool IsCheckedOut { get; set; }
    }
}
