using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public RawImage currentImage;
    private RectTransform rectTransform;

    private void Awake()
    {
        this.rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        var target = eventData.pointerDrag;

        if (target != null)
        {
            Debug.Log("OnDrop");
            //target.GetComponent<RectTransform>().anchoredPosition = this.rectTransform.anchoredPosition;
        }
        
        

        /*if (eventData.pointerDrag != null)
        {
            var target = eventData.pointerDrag;
            target.GetComponent<RectTransform>().anchoredPosition = this.rectTransform.anchoredPosition;
            /*if(this.currentImage != null )
            {
                this.currentImage.texture = target.GetComponent<RawImage>().texture;
            }
        }*/
    }
}
