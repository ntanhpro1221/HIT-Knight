using UnityEngine;

public interface INavigator {
    Vector2 CurDir { get; set; }
    void LookAt(Vector2 pos);
    void FocusOn(Transform target);
    void FocusOn(BindableProperty<Transform> bindedTarget);
    void FocusOn(GameObject target);
    void FocusOn(BindableProperty<GameObject> bindedTarget);
    void StopFocus();
    void ResetRotation();
}
