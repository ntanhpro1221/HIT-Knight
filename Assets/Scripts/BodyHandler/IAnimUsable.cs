/// <summary>
/// Handle animation through AnimInfo.
/// </summary>
public interface IAnimUsable {
    void PlayAnim(AnimInfo anim);
    void PlayAnim(AnimInfo anim, float normalizedTime);
    /// <summary>
    /// Set length of anim (how long it take to finish running)
    /// </summary>
    void PlayAnim(AnimInfo anim, float normalizedTime, float durationTime);
}
