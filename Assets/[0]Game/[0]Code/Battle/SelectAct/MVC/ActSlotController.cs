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

        private void Start()
        {
            GameData.Startup.StartCoroutine(AwaitMove());
        }
        
        private IEnumerator AwaitMove()
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

            if (isSelected)
            {
                if (Model.IsSelectedOnce)
                {
                    GameData.Battle.AddProgress = Model.Act.Progress;
                    EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
                }
                else
                {
                    GameData.Battle.AddProgress = 0;
                    EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
                }
            }
        }
        
        private void UpdateView()
        {
            _view.UpdateView(Model);
        }
    }
}