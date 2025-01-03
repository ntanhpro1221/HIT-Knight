using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnSiteNavigator : Navigator {
    protected override Transform Root => transform;

    protected override SpriteRenderer SR => GetComponent<SpriteRenderer>();
}
