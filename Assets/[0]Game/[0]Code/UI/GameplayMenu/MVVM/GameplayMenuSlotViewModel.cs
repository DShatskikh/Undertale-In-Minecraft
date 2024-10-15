using System;
using UnityEngine;

namespace Game
{
    public class GameplayMenuSlotViewModel : BaseSlotController
    {
        public GameplayMenuSlotModel Model;
        
        private GameplayMenuSlotView _view;
        private GameplayMenu _gameplayMenu;

        public void Init(GameplayMenu gameplayMenu)
        {
            _gameplayMenu = gameplayMenu;
        }
        
        private void Awake()
        {
            _view = GetComponent<GameplayMenuSlotView>();
        }

        private void Start()
        {
            _view.Init(Model, this);
        }

        public override void SetSelected(bool isSelect)
        {
            _view.Upgrade(isSelect, Model);
        }

        public override void Select()
        {
            _gameplayMenu.SelectSlot(this);
        }
    }
}