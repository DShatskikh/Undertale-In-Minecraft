using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class ActSlotController : BaseSlotController
    {
        public ActSlotModel Model;

        private ActSlotView _view;
        
        private void Awake()
        {
            _view = GetComponent<ActSlotView>();
        }

        private IEnumerator Start()
        {
            var startPosition = transform.position;
            
            while (true)
            {
                var progress = 0f;
                var start = transform.position;
                var end = startPosition.AddX(Random.Range(-2, 2)).AddY(Random.Range(-1, 1));

                do
                {
                    transform.position = Vector2.Lerp(start, end, progress);
                    progress += Time.deltaTime / 6;
                    yield return null;
                } while (progress < 1);
            }
        }

        public override void SetSelected(bool isSelected)
        {
            Model.IsSelected = isSelected;
            UpdateView();
        }
        
        private void UpdateView()
        {
            _view.UpdateView(Model);
        }
    }
}