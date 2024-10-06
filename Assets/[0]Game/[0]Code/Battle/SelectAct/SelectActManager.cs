using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SelectActManager : UIPanelBase
    {
        private List<ActSlotModel> _models = new List<ActSlotModel>();
        
        private void OnEnable()
        {
            var assetProvider = GameData.AssetProvider;
            var acts = GameData.EnemyData.EnemyConfig.Acts;
            var distance = new Vector2(2, 1.5f);

            for (int i = 0; i < acts.Length; i++)
            {
                var model = _models.Count == acts.Length ? _models[i] : new ActSlotModel(acts[i]);
                var position = GameData.Battle.transform.position.AddX(i % 2 == 0 ? -distance.x : distance.x)
                    .AddY(i < 2 ? -distance.y : distance.y).AddY(1.5f);
                var slot = Instantiate(assetProvider.ActSlotPrefab, position, Quaternion.identity, transform);
                slot.Model = model;
                int rowIndex = i / 2;
                int columnIndex = i % 2;
                slot.SetSelected(false);
                _slots.Add(new Vector2(columnIndex, rowIndex), slot);
            }
            
            _currentIndex = new Vector2(0, (acts.Length - 1) / 2);
            _slots[_currentIndex].SetSelected(true);
        }
        
        private void OnDisable()
        {
            _models = new List<ActSlotModel>();
            
            foreach (var slot in _slots)
            {
                var model = ((ActSlotController)slot.Value).Model;
                model.IsSelected = false;
                _models.Add(model);
                Destroy(slot.Value.gameObject);
            }

            _slots = new Dictionary<Vector2, BaseSlotController>();
            GameData.Battle.AddProgress = null;
            EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
        }
        
        public override void OnSubmit()
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            var slot = (ActSlotController)_currentSlot;
            slot.Model.IsSelectedOnce = true;
            GameData.Battle.Turn(slot.Model.Act);
            gameObject.SetActive(false);
        }

        public override void OnCancel() { }
    }
}