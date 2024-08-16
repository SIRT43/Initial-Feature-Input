using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FTGAMEStudio.InitialSolution.Inputs
{
    [AddComponentMenu("Initial Solution/Inputs/Event Trigger")]
    public class EventTrigger : OutputableGraphic<PointerEventData>,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IBeginDragHandler,
        IEndDragHandler
    {
        public bool pointerEnter = false;
        public PointerEventInput onPointerEnter;
        public bool pointerDown = false;
        public PointerEventInput onPointerDown;
        public bool dragEvent = false;
        public PointerEventInput onBeginDrag;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!pointerEnter) return;
            onPointerEnter.SetValue(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!pointerEnter) return;
            onPointerEnter.SetValue(null);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!pointerDown) return;
            onPointerDown.SetValue(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!pointerDown) return;
            onPointerDown.SetValue(null);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!dragEvent) return;
            onBeginDrag.SetValue(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!dragEvent) return;
            onBeginDrag.SetValue(null);
        }

        public override void SetValue(PointerEventData value)
        {
            if (onPointerEnter != null) onPointerEnter.SetValue(value);
            if (onPointerDown != null) onPointerDown.SetValue(value);
            if (onBeginDrag != null) onBeginDrag.SetValue(value);
        }

        public override PointerEventData GetValue() => throw new InvalidOperationException("Invalid call, because PointerEventInput is not unique.");
    }
}
