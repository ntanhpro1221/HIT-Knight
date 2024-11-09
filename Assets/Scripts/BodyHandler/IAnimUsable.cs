/// <summary>
/// Handle animation through AnimInfo.
/// </summary>
public interface IAnimUsable {
    public void PlayAnim(AnimInfo anim);
    public void PlayAnim(AnimInfo anim, float normalizedTime);
    /// <summary>
    /// Set length of anim (how long it take to finish running).
    /// </summary>
    public void SetAnimLength(AnimInfo anim, float length);
}
