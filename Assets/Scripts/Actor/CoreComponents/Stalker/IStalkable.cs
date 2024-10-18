public interface IStalkable<T> {
    Observable<T> CurrentTarget { get; }
}
