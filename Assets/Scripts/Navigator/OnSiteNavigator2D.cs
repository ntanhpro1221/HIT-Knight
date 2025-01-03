using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnSiteNavigator2D : Navigator2D {
    protected override Transform Root => transform;

    protected override SpriteRenderer SR => GetComponent<SpriteRenderer>();
}