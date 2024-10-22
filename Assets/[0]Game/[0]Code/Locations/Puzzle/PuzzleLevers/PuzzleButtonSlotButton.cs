using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class PuzzleButtonSlotButton :MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        private PuzzleButtonSlotView _slotView;

        public void Init(PuzzleButtonSlotView slotView)
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