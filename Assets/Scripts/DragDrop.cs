using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector2 originalPosition = Vector2.zero;
    public  bool isDroppedOnValidTarget = false;
    private RectTransform rectTransform = null;
    private CanvasGroup cg = null;

    private void Awake()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.cg = GetComponent<CanvasGroup>();
        this.originalPosition = this.rectTransform.anchoredPosition;
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

    }
    public void OnEndDrag(PointerEventData eventData)
    {

        if (!isDroppedOnValidTarget)
        {
            SnapToClosestSlot(eventData);
        }

        if (cg != null)
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

    }

    private void SnapToClosestSlot(PointerEventData eventData)
    {
        ItemSlot closestSlot = null;
        float closestDistance = float.MaxValue;

        foreach (ItemSlot slot in FindObjectsOfType<ItemSlot>())
        {
            float distance = Vector2.Distance(rectTransform.position, slot.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSlot = slot;
            }
        }

        if (closestSlot != null)
        {
            rectTransform.anchoredPosition = (closestSlot.transform as RectTransform).anchoredPosition;
            isDroppedOnValidTarget = true;
        }
        else
        {
            // Return to original position if no valid slot found
            rectTransform.anchoredPosition = this.originalPosition;
        }
    }
}
