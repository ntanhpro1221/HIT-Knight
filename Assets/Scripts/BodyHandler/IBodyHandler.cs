using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Handle body of object.
/// </summary>
/// <typeparam name="T">Enum that describes all type of event that will be called by animation</typeparam>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public abstract class IBodyHandler<T> : CoreComponent, IAnimUsable, IAnimHandlable<T> where T : Enum {
    private Animator animator;
    private Dictionary<T, Action> animEvents = new();
    private Dictionary<int, float> animLength = new();
    protected virtual void Awake() {
        animator = GetComponent<Animator>();
    }

    protected virtual void Reset() {
        if (animator != null) return;
        animator = GetComponent<Animator>();
    }
    public void RegisterAnimEvent(T type, Action callback) {
        if (animEvents.ContainsKey(type)) {
            animEvents[type] += callback;
        } else {
            animEvents.Add(type, null);
            animEvents[type] += callback;
        }
    }
    public void RemoveAnimEvent(T type, Action callback) {
        if (!animEvents.ContainsKey(type)) {
            Debug.Log("Not Found Type: " + type.GetType().Name);
            return;
        }
        animEvents[type] -= callback;
    }
    public void PostAnimEvent(T type) {
        if (!animEvents.ContainsKey(type)) return;

        Action callback = animEvents[type];
        if (callback != null) {
            callback();
        } else {
            Debug.Log("PostEvent " + type.GetType().Name + "but no listener remain, Remove this key");
            animEvents.Remove(type);
        }
    }
    public void PlayAnim(AnimInfo anim) {
        if (anim != null) {
            animator.Play(anim.Name);
        }
    }
    public void PlayAnim(AnimInfo anim, float normalizedTime) {
        if (anim != null) {
            animator.Play(anim.Name, 0, normalizedTime);
        }
    }
    public void PlayAnim(AnimInfo anim, float normalizedTime, float durationTime) {
        PlayAnim(anim, normalizedTime);
        if (animLength.ContainsKey(anim.HashName)) setSpeed();
        else StartCoroutine(setSpeed_AfterGetSpeed());

        void setSpeed() {
            float speed = animLength[anim.HashName] / durationTime;
            animator.SetFloat(anim.SpeedVarName, speed);
        }

        IEnumerator setSpeed_AfterGetSpeed() {
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).shortNameHash == anim.HashName);
            animLength.Add(anim.HashName, animator.GetCurrentAnimatorStateInfo(0).length);
            setSpeed();
        }
    }

    protected virtual void Update() { }
}