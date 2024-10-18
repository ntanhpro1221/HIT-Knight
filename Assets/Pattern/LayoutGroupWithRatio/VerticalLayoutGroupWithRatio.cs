using UnityEngine;

[AddComponentMenu("Layout/Vertical Layout Group With Ratio", 151)]
/// <summary>
/// Layout child layout elements below each other.
/// </summary>
public class VerticalLayoutGroupWithRatio : HorizontalOrVerticalLayoutGroupWithRatio {
    protected VerticalLayoutGroupWithRatio() { }
    protected override void OnEnable() {
        base.OnEnable();
        for (int i = 0; i < transform.childCount; ++i) {
            try {
                transform.GetChild(i).GetComponent<AspectRatioFitterLayoutElement>().aspectMode =
                    AspectRatioFitterLayoutElement.AspectMode.WidthControlsHeight;
            } catch { }
        }
    }
    protected override void OnDisable() {
        base.OnDisable();
        for (int i = 0; i < transform.childCount; ++i) {
            try {
                transform.GetChild(i).GetComponent<AspectRatioFitterLayoutElement>().aspectMode =
                    AspectRatioFitterLayoutElement.AspectMode.None;
            } catch { }
        }
    }


    /// <summary>
    /// Called by the layout system. Also see ILayoutElement
    /// </summary>
    public override void CalculateLayoutInputHorizontal() {
        base.CalculateLayoutInputHorizontal();
        CalcAlongAxis(0, true);
    }

    /// <summary>
    /// Called by the layout system. Also see ILayoutElement
    /// </summary>
    public override void CalculateLayoutInputVertical() {
        CalcAlongAxis(1, true);
    }

    /// <summary>
    /// Called by the layout system. Also see ILayoutElement
    /// </summary>
    public override void SetLayoutHorizontal() {
        SetChildrenAlongAxis(0, true);
    }

    /// <summary>
    /// Called by the layout system. Also see ILayoutElement
    /// </summary>
    public override void SetLayoutVertical() {
        SetChildrenAlongAxis(1, true);
    }
}
