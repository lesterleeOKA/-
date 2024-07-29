using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector2 orginalPosition = Vector2.zero;
    public  bool isDroppedOnValidTarget = false;
    private RectTransform rectTransform = null;
    private CanvasGroup cg = null;

    private void Awake()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.cg = GetComponent<CanvasGroup>();
        this.orginalPosition = this.rectTransform.anchoredPosition;
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
        this.rectTransform.anchoredPosition += eventData.delta;
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null && eventData.pointerEnter != null)
        {
            ItemSlot itemSlot = eventData.pointerEnter.GetComponent<ItemSlot>();
            if (itemSlot != null)
            {
                this.isDroppedOnValidTarget = true;
            }
        }

        if (!isDroppedOnValidTarget)
        {
            this.rectTransform.anchoredPosition = this.orginalPosition;
        }

        //Debug.Log("OnEndDrag");
        if (this.cg != null)
        {
            this.cg.interactable = true;
            this.cg.blocksRaycasts = true;
        }

    }
}
