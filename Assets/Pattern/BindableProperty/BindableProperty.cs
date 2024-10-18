using UnityEngine.Events;
/// <summary>
/// Invoke your action when its value is changed
/// </summary>
public class BindableProperty<T> {
    private T m_value;
    public UnityEvent<T> OnChanged { get; } = new();
    public T Value {
        get => m_value;
        set {
            if (!m_value.Equals(value))
                OnChanged.Invoke(m_value = value);
        }
    }
    public override string ToString() => m_value.ToString();
    public static implicit operator string(BindableProperty<T> obj) => obj.ToString();
}
