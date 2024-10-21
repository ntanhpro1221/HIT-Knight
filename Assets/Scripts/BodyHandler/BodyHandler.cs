using System;
/// <summary>
/// Handle body of object.
/// </summary>
/// <typeparam name="T">Enum that describes all type of event that will be called by animation</typeparam>
public abstract class IBodyHandler<T> : IAnimUsable, IAnimHandlable<T> where T : Enum {
    public void AddAnimEvent(T type, Action callback) {
        throw new NotImplementedException();
    }
    public void InvokeAnimEvent(T type) {
        throw new NotImplementedException();
    }
    public void PlayAnim(AnimInfo anim) {
        throw new NotImplementedException();
    }
    public void PlayAnim(AnimInfo anim, float normalizedTime) {
        throw new NotImplementedException();
    }
    public void SetAnimLength(AnimInfo anim, float length) {
        throw new NotImplementedException();
    }
}
