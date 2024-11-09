using System.Linq;
using UnityEngine;

[AddComponentMenu("Layout/Horizontal Layout Group With Ratio", 150)]
/// <summary>
/// Layout class for arranging child elements side by side.
/// </summary>
public class HorizontalLayoutGroupWithRatio : HorizontalOrVerticalLayoutGroupWithRatio {
    protected HorizontalLayoutGroupWithRatio() { }
    protected override void OnEnable() {
        base.OnEnable();
        for (int i = 0; i < transform.childCount; ++i) {
            try {
                transform.GetChild(i).GetComponent<AspectRatioFitterLayoutElement>().aspectMode =
                    AspectRatioFitterLayoutElement.AspectMode.HeightControlsWidth;
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
        CalcAlongAxis(0, false);
    }

    /// <summary>
    /// Called by the layout system. Also see ILayoutElement
    /// </summary>
    public override void CalculateLayoutInputVertical() {
        CalcAlongAxis(1, false);
    }

    /// <summary>
    /// Called by the layout system. Also see ILayoutElement
    /// </summary>
    public override void SetLayoutHorizontal() {
        SetChildrenAlongAxis(0, false);
    }

    /// <summary>
    /// Called by the layout system. Also see ILayoutElement
    /// </summary>
    public override void SetLayoutVertical() {
        SetChildrenAlongAxis(1, false);
    }
}
