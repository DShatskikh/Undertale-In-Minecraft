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

        public override void SetSelected(bool isSelected)
        {
            Model.IsSelected = isSelected;
            UpdateView();

            if (isSelected)
            {
                if (Model.IsSelectedOnce)
                {
                    GameData.Battle.AddProgress = Model.Act.GetProgress();
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