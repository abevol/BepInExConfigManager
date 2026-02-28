using UnityEngine.UI;
using UnityEngine;

namespace ConfigManager.UI
{
    public static class UIExtensions
    {
        public static void SnapToSection(this ScrollRect @this, RectTransform section)
        {
            Canvas.ForceUpdateCanvases();

            var contentPos = (Vector2)@this.transform.InverseTransformPoint(@this.content.position);
            var childPos = (Vector2)@this.transform.InverseTransformPoint(section.position);
            var endPos = contentPos - childPos - (Vector2)section.rect.size * 0.5f;
            // If no horizontal scroll, then don't change contentPos.x
            if (!@this.horizontal)
                endPos.x = @this.content.anchoredPosition.x;
            // If no vertical scroll, then don't change contentPos.y
            if (!@this.vertical)
                endPos.y = @this.content.anchoredPosition.y;
            @this.content.anchoredPosition = endPos;
        }

        public static bool IsInSection(this ScrollRect @this, RectTransform section)
        {
            var contentPos = (Vector2)@this.transform.InverseTransformPoint(@this.content.position);
            var childPos = (Vector2)@this.transform.InverseTransformPoint(section.position);
            var endPos = contentPos - childPos + (Vector2)section.rect.size * 0.5f;
            // If horizontal scroll, then compare with contentPos.x
            if (@this.horizontal)
                return endPos.x >= @this.content.anchoredPosition.x;
            // If vertical scroll, then compare with contentPos.y
            if (@this.vertical)
                return endPos.y >= @this.content.anchoredPosition.y;
            return false;
        }
    }
}