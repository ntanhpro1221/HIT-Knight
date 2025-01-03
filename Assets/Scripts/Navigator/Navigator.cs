using UnityEngine;

public abstract class Navigator : CoreComponent, INavigator, ISerializationCallbackReceiver {
    #region JUST SHOW SOMETHING IN INSPECTOR
    [SerializeField] private Vector2 CurDirectionShower;

    public void OnBeforeSerialize() {
        try { CurDirectionShower = CurDir;
        } catch { }
    }

    public void OnAfterDeserialize() { }
    #endregion

    /// <summary>
    /// store direction because cannot modify transform
    /// </summary>
    private Vector2 storedDir = Vector2.right;

    protected abstract Transform Root { get; }
    protected abstract SpriteRenderer SR { get; }
    protected Transform target;
    protected BindableProperty<Transform> bindedTarget;
    protected BindableProperty<GameObject> bindedTargetObj;

    private void Update() {
        if (target != null) 
            CurDir = target.position - Root.position;
    }

    public virtual Vector2 CurDir {
        get => storedDir;
        set {
            storedDir = value;
            if (value.x != 0) SR.flipX = value.x < 0;
        }
    }

    public void LookAt(Vector2 pos) {
        CurDir = pos - (Vector2)Root.position;
    }
    
    private void FocusOn_WithoutClear(Transform target)
        => this.target = target;

    private void FocusOn_WithoutClear(GameObject target)
        => this.target = target == null ? null : target.transform;

    public void FocusOn(Transform target) {
        StopFocus();
        FocusOn_WithoutClear(target);
    }

    public void FocusOn(GameObject target) {
        StopFocus();
        FocusOn_WithoutClear(target);
    }

    public void FocusOn(BindableProperty<Transform> bindedTarget) {
        StopFocus();
        FocusOn_WithoutClear(bindedTarget.Value);
        this.bindedTarget = bindedTarget;
        bindedTarget.OnChanged.AddListener(FocusOn_WithoutClear);
    }
    
    public void FocusOn(BindableProperty<GameObject> bindedTarget) {
        StopFocus();
        FocusOn_WithoutClear(bindedTarget.Value);
        this.bindedTargetObj = bindedTarget;
        bindedTarget.OnChanged.AddListener(FocusOn_WithoutClear);
    }

    public void StopFocus() {
        bindedTarget?.OnChanged.RemoveListener(FocusOn); bindedTarget = null;
        bindedTargetObj?.OnChanged.RemoveListener(FocusOn); bindedTargetObj = null;
        target = null;
    }

    public void ResetRotation() {
        CurDir = Vector2.right;
    }
}
