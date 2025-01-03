using System;

/// <summary>
/// Handle event that is called by animation.
/// </summary>
/// <typeparam name="T">Enum that describes all type of event that will be called by animation.</typeparam>
public interface IAnimHandlable<T> where T : Enum {
    void RegisterAnimEvent(T type, Action callback);
    void RemoveAnimEvent(T type, Action callback);
    /// <summary>
    /// Attach to animation event to call this event.
    /// </summary>
    void PostAnimEvent(T type);
}
