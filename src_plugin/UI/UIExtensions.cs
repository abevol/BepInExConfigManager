using UnityEngine.UI;
using UnityEngine;

namespace ConfigManager.UI
{
    public static class UIExtensions
    {
        public static void SnapTo(this ScrollRect @this, RectTransform child)
        {
            Canvas.ForceUpdateCanvases();

            var contentPos = (Vector2)@this.transform.InverseTransformPoint(@this.content.position);
            var childPos = (Vector2)@this.transform.InverseTransformPoint(child.position);
            var endPos = contentPos - childPos - (Vector2)child.rect.size * 0.5f;
            // If no horizontal scroll, then don't change contentPos.x
            if (!@this.horizontal)
                endPos.x = @this.content.anchoredPosition.x;
            // If no vertical scroll, then don't change contentPos.y
            if (!@this.vertical)
                endPos.y = @this.content.anchoredPosition.y;
            @this.content.anchoredPosition = endPos;
        }

        public static bool IsInView(this ScrollRect @this, RectTransform child)
        {
            var contentPos = (Vector2)@this.transform.InverseTransformPoint(@this.content.position);
            var childPos = (Vector2)@this.transform.InverseTransformPoint(child.position);
            var endPos = contentPos - childPos - (Vector2)child.rect.size * 0.5f;

            Vector2 viewportPosition = @this.content.anchoredPosition;
            Rect viewportRect = new Rect(viewportPosition, @this.viewport.rect.size);
            Rect childRect = child.rect;
            Vector2 childPosition = (Vector2)@this.transform.InverseTransformPoint(child.position);

            var childTopLeft = contentPos - childPosition - (Vector2)childRect.size * 0.5f;
            var childBottomRight = contentPos - childPosition + (Vector2)childRect.size * 0.5f;

            return childTopLeft.y >= viewportRect.yMin && childBottomRight.y <= viewportRect.yMax;
        }
    }
}