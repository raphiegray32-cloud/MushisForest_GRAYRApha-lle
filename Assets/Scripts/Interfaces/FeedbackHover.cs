using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originalPosition + new Vector2(50f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.anchoredPosition= originalPosition;
    }
}
