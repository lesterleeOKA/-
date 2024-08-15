using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CanvasGroup))]
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector2 originalPosition = Vector2.zero;
    public bool isDroppedOnValidTarget = false;
    [HideInInspector]
    public RectTransform rectTransform = null;
    private CanvasGroup cg = null;
    public Vector2 originalSize;


    protected virtual void Awake()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.cg = GetComponent<CanvasGroup>();
        this.originalPosition = this.rectTransform.anchoredPosition;
        this.originalSize = this.rectTransform.sizeDelta;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("OnBeginDrag");
        this.isDroppedOnValidTarget = false;
        if (this.cg != null)
        {
            this.cg.interactable = false;
            this.cg.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("dragging");
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        this.rectTransform.anchoredPosition = localPoint;
        if (this.rectTransform.sizeDelta != originalSize) { this.rectTransform.sizeDelta = originalSize; }
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<ItemSlot>() != null)
        {
            var itemSlot = eventData.pointerEnter.GetComponent<ItemSlot>();
            this.isDroppedOnValidTarget = true;
            this.rectTransform.anchoredPosition = itemSlot.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("OnDrop");
        }
        else
        {
            this.isDroppedOnValidTarget = false;
            this.rectTransform.anchoredPosition = originalPosition;
        }

        // Re-enable interaction and raycast blocking
        if (this.cg != null)
        {
            this.cg.interactable = true;
            this.cg.blocksRaycasts = true;
        }
    }


}
