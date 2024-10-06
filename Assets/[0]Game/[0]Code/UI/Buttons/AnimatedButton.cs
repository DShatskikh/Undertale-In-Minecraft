using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public abstract class AnimatedButton : ButtonBase, IPointerDownHandler, IPointerUpHandler
    {
        protected ButtonViewBase _view;

        protected override void OnAwake()
        {
            if (TryGetComponent(out ButtonViewBase view))
                _view = view;
            else
                Debug.LogWarning("Не найден ButtonView");
        }

        private void OnEnable()
        {
            Enable();
        }

        private void OnDisable()
        {
            _view.Disable();
            Disable();
        }

        public void OnPointerDown(PointerEventData eventData) => 
            _view.Down();

        public void OnPointerUp(PointerEventData eventData) => 
            _view.Up();

        public override void OnClick() { }
        protected abstract void Enable();
        protected abstract void Disable();
    }
}