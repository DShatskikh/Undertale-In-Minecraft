using System;
using UnityEngine;

namespace Game
{
    public class GameplayMenuSlotViewModel : BaseSlotController
    {
        public GameplayMenuSlotModel Model;
        
        private GameplayMenuSlotView _view;

        private void Awake()
        {
            _view = GetComponent<GameplayMenuSlotView>();
        }

        private void Start()
        {
            _view.Init(Model);
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect, Model);
        }
    }
}