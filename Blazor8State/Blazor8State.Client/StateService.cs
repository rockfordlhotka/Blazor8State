namespace Blazor8State.Client
{
  public class StateService
  {
    public string State { get; set; } = string.Empty;
    public Guid ServiceId { get; private set; } = Guid.NewGuid();
  }
}
