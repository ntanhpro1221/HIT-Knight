using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public abstract class HorizontalOrVerticalLayoutGroupWithRatio : LayoutGroup {
    [SerializeField] protected float m_Spacing = 0;

    /// <summary>
    /// The spacing to use between layout elements in the layout group.
    /// </summary>
    public float spacing { get { return m_Spacing; } set { SetProperty(ref m_Spacing, value); } }

    [SerializeField] protected bool m_ChildForceExpandWidth = true;

    /// <summary>
    /// Whether to force the children to expand to fill additional available horizontal space.
    /// </summary>
    public bool childForceExpandWidth { get { return m_ChildForceExpandWidth; } set { SetProperty(ref m_ChildForceExpandWidth, value); } }

    [SerializeField] protected bool m_ChildForceExpandHeight = true;

    /// <summary>
    /// Whether to force the children to expand to fill additional available vertical space.
    /// </summary>
    public bool childForceExpandHeight { get { return m_ChildForceExpandHeight; } set { SetProperty(ref m_ChildForceExpandHeight, value); } }

    [SerializeField] protected bool m_ChildControlWidth = true;

    /// <summary>
    /// Returns true if the Layout Group controls the widths of its children. Returns false if children control their own widths.
    /// </summary>
    /// <remarks>
    /// If set to false, the layout group will only affect the positions of the children while leaving the widths untouched. The widths of the children can be set via the respective RectTransforms in this case.
    ///
    /// If set to true, the widths of the children are automatically driven by the layout group according to their respective minimum, preferred, and flexible widths. This is useful if the widths of the children should change depending on how much space is available.In this case the width of each child cannot be set manually in the RectTransform, but the minimum, preferred and flexible width for each child can be controlled by adding a LayoutElement component to it.
    /// </remarks>
    public bool childControlWidth { get { return m_ChildControlWidth; } set { SetProperty(ref m_ChildControlWidth, value); } }

    [SerializeField] protected bool m_ChildControlHeight = true;

    /// <summary>
    /// Returns true if the Layout Group controls the heights of its children. Returns false if children control their own heights.
    /// </summary>
    /// <remarks>
    /// If set to false, the layout group will only affect the positions of the children while leaving the heights untouched. The heights of the children can be set via the respective RectTransforms in this case.
    ///
    /// If set to true, the heights of the children are automatically driven by the layout group according to their respective minimum, preferred, and flexible heights. This is useful if the heights of the children should change depending on how much space is available.In this case the height of each child cannot be set manually in the RectTransform, but the minimum, preferred and flexible height for each child can be controlled by adding a LayoutElement component to it.
    /// </remarks>
    public bool childControlHeight { get { return m_ChildControlHeight; } set { SetProperty(ref m_ChildControlHeight, value); } }

    [SerializeField] protected bool m_ChildScaleWidth = false;

    /// <summary>
    /// Whether to use the x scale of each child when calculating its width.
    /// </summary>
    public bool childScaleWidth { get { return m_ChildScaleWidth; } set { SetProperty(ref m_ChildScaleWidth, value); } }

    [SerializeField] protected bool m_ChildScaleHeight = false;

    /// <summary>
    /// Whether to use the y scale of each child when calculating its height.
    /// </summary>
    public bool childScaleHeight { get { return m_ChildScaleHeight; } set { SetProperty(ref m_ChildScaleHeight, value); } }

    /// <summary>
    /// Whether the order of children objects should be sorted in reverse.
    /// </summary>
    /// <remarks>
    /// If False the first child object will be positioned first.
    /// If True the last child object will be positioned first.
    /// </remarks>
    public bool reverseArrangement { get { return m_ReverseArrangement; } set { SetProperty(ref m_ReverseArrangement, value); } }

    [SerializeField] protected bool m_ReverseArrangement = false;

    /// <summary>
    /// Calculate the layout element properties for this layout element along the given axis.
    /// </summary>
    /// <param name="axis">The axis to calculate for. 0 is horizontal and 1 is vertical.</param>
    /// <param name="isVertical">Is this group a vertical group?</param>
    protected void CalcAlongAxis(int axis, bool isVertical) {
        float combinedPadding = (axis == 0 ? padding.horizontal : padding.vertical);
        bool controlSize = (axis == 0 ? m_ChildControlWidth : m_ChildControlHeight);
        bool useScale = (axis == 0 ? m_ChildScaleWidth : m_ChildScaleHeight);
        bool childForceExpandSize = (axis == 0 ? m_ChildForceExpandWidth : m_ChildForceExpandHeight);

        float totalMin = combinedPadding;
        float totalPreferred = combinedPadding;
        float totalFlexible = 0;

        bool alongOtherAxis = (isVertical ^ (axis == 1));
        var rectChildrenCount = rectChildren.Count;
        for (int i = 0; i < rectChildrenCount; i++) {
            RectTransform child = rectChildren[i];
            float min, preferred, flexible;
            GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);

            if (useScale) {
                float scaleFactor = child.localScale[axis];
                min *= scaleFactor;
                preferred *= scaleFactor;
                flexible *= scaleFactor;
            }

            if (alongOtherAxis) {
                totalMin = Mathf.Max(min + combinedPadding, totalMin);
                totalPreferred = Mathf.Max(preferred + combinedPadding, totalPreferred);
                totalFlexible = Mathf.Max(flexible, totalFlexible);
            } else {
                totalMin += min + spacing;
                totalPreferred += preferred + spacing;

                // Increment flexible size with element's flexible size.
                totalFlexible += flexible;
            }
        }

        if (!alongOtherAxis && rectChildren.Count > 0) {
            totalMin -= spacing;
            totalPreferred -= spacing;
        }
        totalPreferred = Mathf.Max(totalMin, totalPreferred);
        SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, axis);
    }

    /// <summary>
    /// Set the positions and sizes of the child layout elements for the given axis.
    /// </summary>
    /// <param name="axis">The axis to handle. 0 is horizontal and 1 is vertical.</param>
    /// <param name="isVertical">Is this group a vertical group?</param>
    protected void SetChildrenAlongAxis(int axis, bool isVertical) {
        float size = rectTransform.rect.size[axis];
        bool controlSize = (axis == 0 ? m_ChildControlWidth : m_ChildControlHeight);
        bool useScale = (axis == 0 ? m_ChildScaleWidth : m_ChildScaleHeight);
        bool childForceExpandSize = (axis == 0 ? m_ChildForceExpandWidth : m_ChildForceExpandHeight);
        float alignmentOnAxis = GetAlignmentOnAxis(axis);

        bool alongOtherAxis = (isVertical ^ (axis == 1));
        int startIndex = m_ReverseArrangement ? rectChildren.Count - 1 : 0;
        int endIndex = m_ReverseArrangement ? 0 : rectChildren.Count;
        int increment = m_ReverseArrangement ? -1 : 1;
        if (alongOtherAxis) {
            float innerSize = size - (axis == 0 ? padding.horizontal : padding.vertical);

            for (int i = startIndex; m_ReverseArrangement ? i >= endIndex : i < endIndex; i += increment) {
                if (rectTransform.hasChanged == false) return;
                RectTransform child = rectChildren[i];
                float min, preferred, flexible;
                GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
                float scaleFactor = useScale ? child.localScale[axis] : 1f;

                float requiredSpace = Mathf.Clamp(innerSize, min, flexible > 0 ? size : preferred);
                float startOffset = GetStartOffset(axis, requiredSpace * scaleFactor);
                if (controlSize) {
                    SetChildAlongAxisWithScale(child, axis, startOffset, requiredSpace, scaleFactor);
                } else {
                    float offsetInCell = (requiredSpace - child.sizeDelta[axis]) * alignmentOnAxis;
                    SetChildAlongAxisWithScale(child, axis, startOffset + offsetInCell, scaleFactor);
                }
            }
            rectTransform.hasChanged = false;
        } else {
            float pos = (axis == 0 ? padding.left : padding.top);
            float itemFlexibleMultiplier = 0;
            float surplusSpace = size - GetTotalPreferredSize(axis);

            if (surplusSpace > 0) {
                if (GetTotalFlexibleSize(axis) == 0)
                    pos = GetStartOffset(axis, GetTotalPreferredSize(axis) - (axis == 0 ? padding.horizontal : padding.vertical));
                else if (GetTotalFlexibleSize(axis) > 0)
                    itemFlexibleMultiplier = surplusSpace / GetTotalFlexibleSize(axis);
            }

            float minMaxLerp = 0;
            if (GetTotalMinSize(axis) != GetTotalPreferredSize(axis))
                minMaxLerp = Mathf.Clamp01((size - GetTotalMinSize(axis)) / (GetTotalPreferredSize(axis) - GetTotalMinSize(axis)));

            for (int i = startIndex; m_ReverseArrangement ? i >= endIndex : i < endIndex; i += increment) {
                RectTransform child = rectChildren[i];
                float min, preferred, flexible;
                GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
                float scaleFactor = useScale ? child.localScale[axis] : 1f;

                float childSize = Mathf.Lerp(min, preferred, minMaxLerp);
                childSize += flexible * itemFlexibleMultiplier;
                if (controlSize) {
                    SetChildAlongAxisWithScale(child, axis, pos, childSize, scaleFactor);
                } else {
                    float offsetInCell = (childSize - child.sizeDelta[axis]) * alignmentOnAxis;
                    SetChildAlongAxisWithScale(child, axis, pos + offsetInCell, scaleFactor);
                }
                pos += childSize * scaleFactor + spacing;
            }
        }
    }

    private void GetChildSizes(RectTransform child, int axis, bool controlSize, bool childForceExpand,
        out float min, out float preferred, out float flexible) {
        if (!controlSize) {
            min = child.sizeDelta[axis];
            preferred = min;
            flexible = 0;
        } else {
            var ratio = child.GetComponent<AspectRatioFitterLayoutElement>();
            min = ratio?.aspectMode switch {
                AspectRatioFitterLayoutElement.AspectMode.HeightControlsWidth =>
                    axis == 0 ? child.rect.width : rectTransform.rect.height - ratio.VerticalPadding,
                AspectRatioFitterLayoutElement.AspectMode.WidthControlsHeight =>
                    axis == 1 ? child.rect.height : rectTransform.rect.width - ratio.HorizontalPadding,
                _ => LayoutUtility.GetMinSize(child, axis)
            };
            preferred = LayoutUtility.GetPreferredSize(child, axis);
            flexible = LayoutUtility.GetFlexibleSize(child, axis);
        }

        if (childForceExpand)
            flexible = Mathf.Max(flexible, 1);
    }


#if UNITY_EDITOR
    protected override void Reset() {
        base.Reset();

        // For new added components we want these to be set to false,
        // so that the user's sizes won't be overwritten before they
        // have a chance to turn these settings off.
        // However, for existing components that were added before this
        // feature was introduced, we want it to be on be default for
        // backwardds compatibility.
        // Hence their default value is on, but we set to off in reset.
        m_ChildControlWidth = false;
        m_ChildControlHeight = false;
    }

    private int m_Capacity = 10;
    private Vector2[] m_Sizes = new Vector2[10];

    protected virtual void Update() {
        if (Application.isPlaying)
            return;

        int count = transform.childCount;

        if (count > m_Capacity) {
            if (count > m_Capacity * 2)
                m_Capacity = count;
            else
                m_Capacity *= 2;

            m_Sizes = new Vector2[m_Capacity];
        }

        // If children size change in editor, update layout (case 945680 - Child GameObjects in a Horizontal/Vertical Layout Group don't display their correct position in the Editor)
        bool dirty = false;
        for (int i = 0; i < count; i++) {
            RectTransform t = transform.GetChild(i) as RectTransform;
            if (t != null && t.sizeDelta != m_Sizes[i]) {
                dirty = true;
                m_Sizes[i] = t.sizeDelta;
            }
        }

        if (dirty)
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
    }
#endif
}






//----------------------------------------

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.EventSystems;
//using UnityEngine.Pool;

//namespace UnityEngine.UI {
//    [DisallowMultipleComponent]
//    [ExecuteAlways]
//    [RequireComponent(typeof(RectTransform))]
//    /// <summary>
//    /// Abstract base class to use for layout groups.
//    /// </summary>
//    public abstract class LayoutGroup : UIBehaviour, ILayoutElement, ILayoutGroup {
//        [SerializeField] protected RectOffset m_Padding = new RectOffset();

//        /// <summary>
//        /// The padding to add around the child layout elements.
//        /// </summary>
//        public RectOffset padding { get { return m_Padding; } set { SetProperty(ref m_Padding, value); } }

//        [SerializeField] protected TextAnchor m_ChildAlignment = TextAnchor.UpperLeft;

//        /// <summary>
//        /// The alignment to use for the child layout elements in the layout group.
//        /// </summary>
//        /// <remarks>
//        /// If a layout element does not specify a flexible width or height, its child elements many not use the available space within the layout group. In this case, use the alignment settings to specify how to align child elements within their layout group.
//        /// </remarks>
//        public TextAnchor childAlignment { get { return m_ChildAlignment; } set { SetProperty(ref m_ChildAlignment, value); } }

//        [System.NonSerialized] private RectTransform m_Rect;
//        protected RectTransform rectTransform {
//            get {
//                if (m_Rect == null)
//                    m_Rect = GetComponent<RectTransform>();
//                return m_Rect;
//            }
//        }

//        protected DrivenRectTransformTracker m_Tracker;
//        private Vector2 m_TotalMinSize = Vector2.zero;
//        private Vector2 m_TotalPreferredSize = Vector2.zero;
//        private Vector2 m_TotalFlexibleSize = Vector2.zero;

//        [System.NonSerialized] private List<RectTransform> m_RectChildren = new List<RectTransform>();
//        protected List<RectTransform> rectChildren { get { return m_RectChildren; } }

//        public virtual void CalculateLayoutInputHorizontal() {
//            m_RectChildren.Clear();
//            var toIgnoreList = ListPool<Component>.Get();
//            for (int i = 0; i < rectTransform.childCount; i++) {
//                var rect = rectTransform.GetChild(i) as RectTransform;
//                if (rect == null || !rect.gameObject.activeInHierarchy)
//                    continue;

//                rect.GetComponents(typeof(ILayoutIgnorer), toIgnoreList);

//                if (toIgnoreList.Count == 0) {
//                    m_RectChildren.Add(rect);
//                    continue;
//                }

//                for (int j = 0; j < toIgnoreList.Count; j++) {
//                    var ignorer = (ILayoutIgnorer)toIgnoreList[j];
//                    if (!ignorer.ignoreLayout) {
//                        m_RectChildren.Add(rect);
//                        break;
//                    }
//                }
//            }
//            ListPool<Component>.Release(toIgnoreList);
//            m_Tracker.Clear();
//        }

//        public abstract void CalculateLayoutInputVertical();

//        /// <summary>
//        /// See LayoutElement.minWidth
//        /// </summary>
//        public virtual float minWidth { get { return GetTotalMinSize(0); } }

//        /// <summary>
//        /// See LayoutElement.preferredWidth
//        /// </summary>
//        public virtual float preferredWidth { get { return GetTotalPreferredSize(0); } }

//        /// <summary>
//        /// See LayoutElement.flexibleWidth
//        /// </summary>
//        public virtual float flexibleWidth { get { return GetTotalFlexibleSize(0); } }

//        /// <summary>
//        /// See LayoutElement.minHeight
//        /// </summary>
//        public virtual float minHeight { get { return GetTotalMinSize(1); } }

//        /// <summary>
//        /// See LayoutElement.preferredHeight
//        /// </summary>
//        public virtual float preferredHeight { get { return GetTotalPreferredSize(1); } }

//        /// <summary>
//        /// See LayoutElement.flexibleHeight
//        /// </summary>
//        public virtual float flexibleHeight { get { return GetTotalFlexibleSize(1); } }

//        /// <summary>
//        /// See LayoutElement.layoutPriority
//        /// </summary>
//        public virtual int layoutPriority { get { return 0; } }

//        // ILayoutController Interface

//        public abstract void SetLayoutHorizontal();
//        public abstract void SetLayoutVertical();

//        // Implementation

//        protected LayoutGroup() {
//            if (m_Padding == null)
//                m_Padding = new RectOffset();
//        }

//        protected override void OnEnable() {
//            base.OnEnable();
//            SetDirty();
//        }

//        protected override void OnDisable() {
//            m_Tracker.Clear();
//            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
//            base.OnDisable();
//        }

//        /// <summary>
//        /// Callback for when properties have been changed by animation.
//        /// </summary>
//        protected override void OnDidApplyAnimationProperties() {
//            SetDirty();
//        }

//        /// <summary>
//        /// The min size for the layout group on the given axis.
//        /// </summary>
//        /// <param name="axis">The axis index. 0 is horizontal and 1 is vertical.</param>
//        /// <returns>The min size</returns>
//        protected float GetTotalMinSize(int axis) {
//            return m_TotalMinSize[axis];
//        }

//        /// <summary>
//        /// The preferred size for the layout group on the given axis.
//        /// </summary>
//        /// <param name="axis">The axis index. 0 is horizontal and 1 is vertical.</param>
//        /// <returns>The preferred size.</returns>
//        protected float GetTotalPreferredSize(int axis) {
//            return m_TotalPreferredSize[axis];
//        }

//        /// <summary>
//        /// The flexible size for the layout group on the given axis.
//        /// </summary>
//        /// <param name="axis">The axis index. 0 is horizontal and 1 is vertical.</param>
//        /// <returns>The flexible size</returns>
//        protected float GetTotalFlexibleSize(int axis) {
//            return m_TotalFlexibleSize[axis];
//        }

//        /// <summary>
//        /// Returns the calculated position of the first child layout element along the given axis.
//        /// </summary>
//        /// <param name="axis">The axis index. 0 is horizontal and 1 is vertical.</param>
//        /// <param name="requiredSpaceWithoutPadding">The total space required on the given axis for all the layout elements including spacing and excluding padding.</param>
//        /// <returns>The position of the first child along the given axis.</returns>
//        protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding) {
//            float requiredSpace = requiredSpaceWithoutPadding + (axis == 0 ? padding.horizontal : padding.vertical);
//            float availableSpace = rectTransform.rect.size[axis];
//            float surplusSpace = availableSpace - requiredSpace;
//            float alignmentOnAxis = GetAlignmentOnAxis(axis);
//            return (axis == 0 ? padding.left : padding.top) + surplusSpace * alignmentOnAxis;
//        }

//        /// <summary>
//        /// Returns the alignment on the specified axis as a fraction where 0 is left/top, 0.5 is middle, and 1 is right/bottom.
//        /// </summary>
//        /// <param name="axis">The axis to get alignment along. 0 is horizontal and 1 is vertical.</param>
//        /// <returns>The alignment as a fraction where 0 is left/top, 0.5 is middle, and 1 is right/bottom.</returns>
//        protected float GetAlignmentOnAxis(int axis) {
//            if (axis == 0)
//                return ((int)childAlignment % 3) * 0.5f;
//            else
//                return ((int)childAlignment / 3) * 0.5f;
//        }

//        /// <summary>
//        /// Used to set the calculated layout properties for the given axis.
//        /// </summary>
//        /// <param name="totalMin">The min size for the layout group.</param>
//        /// <param name="totalPreferred">The preferred size for the layout group.</param>
//        /// <param name="totalFlexible">The flexible size for the layout group.</param>
//        /// <param name="axis">The axis to set sizes for. 0 is horizontal and 1 is vertical.</param>
//        protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis) {
//            m_TotalMinSize[axis] = totalMin;
//            m_TotalPreferredSize[axis] = totalPreferred;
//            m_TotalFlexibleSize[axis] = totalFlexible;
//        }

//        /// <summary>
//        /// Set the position and size of a child layout element along the given axis.
//        /// </summary>
//        /// <param name="rect">The RectTransform of the child layout element.</param>
//        /// <param name="axis">The axis to set the position and size along. 0 is horizontal and 1 is vertical.</param>
//        /// <param name="pos">The position from the left side or top.</param>
//        protected void SetChildAlongAxis(RectTransform rect, int axis, float pos) {
//            if (rect == null)
//                return;

//            SetChildAlongAxisWithScale(rect, axis, pos, 1.0f);
//        }

//        /// <summary>
//        /// Set the position and size of a child layout element along the given axis.
//        /// </summary>
//        /// <param name="rect">The RectTransform of the child layout element.</param>
//        /// <param name="axis">The axis to set the position and size along. 0 is horizontal and 1 is vertical.</param>
//        /// <param name="pos">The position from the left side or top.</param>
//        protected void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float scaleFactor) {
//            if (rect == null)
//                return;

//            m_Tracker.Add(this, rect,
//                DrivenTransformProperties.Anchors |
//                (axis == 0 ? DrivenTransformProperties.AnchoredPositionX : DrivenTransformProperties.AnchoredPositionY));

//            // Inlined rect.SetInsetAndSizeFromParentEdge(...) and refactored code in order to multiply desired size by scaleFactor.
//            // sizeDelta must stay the same but the size used in the calculation of the position must be scaled by the scaleFactor.

//            rect.anchorMin = Vector2.up;
//            rect.anchorMax = Vector2.up;

//            Vector2 anchoredPosition = rect.anchoredPosition;
//            anchoredPosition[axis] = (axis == 0) ? (pos + rect.sizeDelta[axis] * rect.pivot[axis] * scaleFactor) : (-pos - rect.sizeDelta[axis] * (1f - rect.pivot[axis]) * scaleFactor);
//            rect.anchoredPosition = anchoredPosition;
//        }

//        /// <summary>
//        /// Set the position and size of a child layout element along the given axis.
//        /// </summary>
//        /// <param name="rect">The RectTransform of the child layout element.</param>
//        /// <param name="axis">The axis to set the position and size along. 0 is horizontal and 1 is vertical.</param>
//        /// <param name="pos">The position from the left side or top.</param>
//        /// <param name="size">The size.</param>
//        protected void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size) {
//            if (rect == null)
//                return;

//            SetChildAlongAxisWithScale(rect, axis, pos, size, 1.0f);
//        }

//        /// <summary>
//        /// Set the position and size of a child layout element along the given axis.
//        /// </summary>
//        /// <param name="rect">The RectTransform of the child layout element.</param>
//        /// <param name="axis">The axis to set the position and size along. 0 is horizontal and 1 is vertical.</param>
//        /// <param name="pos">The position from the left side or top.</param>
//        /// <param name="size">The size.</param>
//        protected void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float size, float scaleFactor) {
//            if (rect == null)
//                return;

//            m_Tracker.Add(this, rect,
//                DrivenTransformProperties.Anchors |
//                (axis == 0 ?
//                    (DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.SizeDeltaX) :
//                    (DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.SizeDeltaY)
//                )
//            );

//            // Inlined rect.SetInsetAndSizeFromParentEdge(...) and refactored code in order to multiply desired size by scaleFactor.
//            // sizeDelta must stay the same but the size used in the calculation of the position must be scaled by the scaleFactor.

//            rect.anchorMin = Vector2.up;
//            rect.anchorMax = Vector2.up;

//            Vector2 sizeDelta = rect.sizeDelta;
//            sizeDelta[axis] = size;
//            rect.sizeDelta = sizeDelta;

//            Vector2 anchoredPosition = rect.anchoredPosition;
//            anchoredPosition[axis] = (axis == 0) ? (pos + size * rect.pivot[axis] * scaleFactor) : (-pos - size * (1f - rect.pivot[axis]) * scaleFactor);
//            rect.anchoredPosition = anchoredPosition;
//        }

//        private bool isRootLayoutGroup {
//            get {
//                Transform parent = transform.parent;
//                if (parent == null)
//                    return true;
//                return transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
//            }
//        }

//        protected override void OnRectTransformDimensionsChange() {
//            base.OnRectTransformDimensionsChange();
//            if (isRootLayoutGroup)
//                SetDirty();
//        }

//        protected virtual void OnTransformChildrenChanged() {
//            SetDirty();
//        }

//        /// <summary>
//        /// Helper method used to set a given property if it has changed.
//        /// </summary>
//        /// <param name="currentValue">A reference to the member value.</param>
//        /// <param name="newValue">The new value.</param>
//        protected void SetProperty<T>(ref T currentValue, T newValue) {
//            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
//                return;
//            currentValue = newValue;
//            SetDirty();
//        }

//        /// <summary>
//        /// Mark the LayoutGroup as dirty.
//        /// </summary>
//        protected void SetDirty() {
//            if (!IsActive())
//                return;

//            if (!CanvasUpdateRegistry.IsRebuildingLayout())
//                LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
//            else
//                StartCoroutine(DelayedSetDirty(rectTransform));
//        }

//        IEnumerator DelayedSetDirty(RectTransform rectTransform) {
//            yield return null;
//            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
//        }

//#if UNITY_EDITOR
//        protected override void OnValidate() {
//            SetDirty();
//        }

//#endif
//    }
//}

