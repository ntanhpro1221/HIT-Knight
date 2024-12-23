/// <summary>
/// How a buff affect to raw stat. <br/>
/// (final stat) = (raw stat) + (sum of all Add type buffs) + (sum of all Mul type buffs) * (raw stat).
/// </summary>
public enum BuffType {
    /// <summary>
    /// Add directly buff value to raw stat.
    /// </summary>
    Add,
    /// <summary>
    /// Increase raw stat by (buff value)%
    /// </summary>
    Mul
}
