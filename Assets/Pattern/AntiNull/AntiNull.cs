using System;
using Unity.VisualScripting;

public class AntiNull<T> {
    private T m_Value;
    public T Value {
        get => m_Value ??= source();
        set => m_Value = value;
    }

    private readonly Func<T> source;

    public AntiNull(T value) {
        m_Value = value;
    }

    public AntiNull(Func<T> source) {
        this.source = source;
    }
}
