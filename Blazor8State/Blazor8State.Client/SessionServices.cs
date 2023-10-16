namespace Blazor8State.Client
{
    public class SessionList : Dictionary<string, SessionServices>
    { }

    public class SessionServices : Dictionary<Type, object>
    { 
        public T GetService<T>(Type t)
        {
            return (T)this[t];
        }
    }
}
