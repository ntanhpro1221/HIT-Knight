using UnityEngine;

public class AllPrefabRef : Singleton<AllPrefabRef> {
    [field: SerializeField]
    public PrefabRefDict Hero { get; private set; }
    [field: SerializeField]
    public PrefabRefDict Monster { get; private set; }
    [field: SerializeField]
    public PrefabRefDict Weapon { get; private set; }
    [field: SerializeField]
    public PrefabRefDict Bullet { get; private set; }
}