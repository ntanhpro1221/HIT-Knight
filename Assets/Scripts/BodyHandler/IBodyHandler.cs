using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handle body of object.
/// </summary>
/// <typeparam name="T">Enum that describes all type of event that will be called by animation</typeparam>
[RequireComponent(typeof(Animator))]
public abstract class IBodyHandler<T> : Component, IAnimUsable, IAnimHandlable<T> where T : Enum
{
    private Animator animator;
    private Dictionary<T, Action> AnimEvents = new Dictionary<T, Action>();
    protected virtual void Awake() {
        animator = GetComponent<Animator>(); 
    }

    protected virtual void Reset()
    {
        if(animator!=null) return;
        animator = GetComponent<Animator>(); 
    }
    public void RegisterAnimEvent(T type, Action callback) {
        if (AnimEvents.ContainsKey(type))
        {
            AnimEvents[type] += callback;
        }
        else
        {
            AnimEvents.Add(type,null);
            AnimEvents[type] += callback;
        }
    }
    public void RemoveAnimEvent(T type, Action callback) {
        if (!AnimEvents.ContainsKey(type))
        {
            Debug.Log("Not Found Type: " + type.GetType().Name);
            return;
        }
        AnimEvents[type] -= callback;
    }
    public void PostAnimEvent(T type) {
        if (!AnimEvents.ContainsKey(type))
        {
            Debug.Log("Event has no Listener");
            return;
        }

        Action callback = AnimEvents[type];
        if (callback != null)
        {
            callback();
        }
        else
        {
            Debug.Log("PostEvent " + type.GetType().Name + "but no listener remain, Remove this key");
            AnimEvents.Remove(type);
        }
    }
    public void PlayAnim(AnimInfo anim) {
        if (anim != null) {
            animator.Play(anim.name); 
            animator.SetFloat(anim.speedVar, 1f);
        }
    }
    public void PlayAnim(AnimInfo anim, float normalizedTime) {
        if (anim != null) {
            animator.Play(anim.name, 0, normalizedTime); 
        }
    }
    public void SetAnimLength(AnimInfo anim, float length)
    {
        if (anim == null)
        {
            Debug.LogWarning("AnimInfo is null.");
            return;
        }

        if (animator == null)
        {
            Debug.LogWarning("Animator not found on this GameObject.");
            return;
        }
        float currentLength = animator.GetCurrentAnimatorStateInfo(0).length;
        if (currentLength > 0)
        {
            float speed = currentLength / length;
            animator.SetFloat(anim.speedVar, speed);
        }
        else
        {
            Debug.LogWarning("Current animation length is 0 or invalid.");
        }
    }
}
