using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class PuzzleLeverSlotLever : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        private PuzzleLeverSlotView _slotView;

        public void Init(PuzzleLeverSlotView slotView)
        {
            _slotView = slotView;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _slotView.OnPointerEnter(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _slotView.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _slotView.OnPointerUp(eventData);
        }
    }
}