public class ActorCore : Core {
    private IActor m_Actor;
    public IActor Actor
        => m_Actor ??= transform.parent.GetComponent<IActor>();
}
