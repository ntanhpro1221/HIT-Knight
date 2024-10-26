using System;

/// <summary>
/// Handle event that is called by animation.
/// </summary>
/// <typeparam name="T">Enum that describes all type of event that will be called by animation.</typeparam>
public interface IAnimHandlable<T> where T : Enum {
    public void RegisterAnimEvent(T type, Action callback);
    public void RemoveAnimEvent(T type, Action callback);
    /// <summary>
    /// Attach to animation event to call this event.
    /// </summary>
    public void PostAnimEvent(T type);
}
