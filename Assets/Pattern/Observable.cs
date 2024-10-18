using UnityEngine.Events;
/// <summary>
/// Invoke your action when its value is changed
/// </summary>
public class Observable<T> {
    private T m_value;
    public UnityEvent<T> OnChanged { get; } = new();
    public T Value {
        get => m_value;
        set {
            if (m_value.Equals(value) == false)
                OnChanged.Invoke(m_value = value);
        }
    }
    public override string ToString() => m_value.ToString();
    public static implicit operator string(Observable<T> obj) => obj.ToString();
}
