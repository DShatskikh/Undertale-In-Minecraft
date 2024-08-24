using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SelectActManager : UIPanelBase
    {
        private void OnEnable()
        {
            var assetProvider = GameData.AssetProvider;
            var acts = GameData.EnemyData.EnemyConfig.Acts;
            var distance = new Vector2(2, 1.5f);

            for (int i = 0; i < acts.Length; i++)
            {
                var model = new ActSlotModel(acts[i]);
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
            foreach (var slot in _slots) 
                Destroy(slot.Value.gameObject);

            _slots = new Dictionary<Vector2, BaseSlotController>();
        }
        
        public override void OnSubmit()
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            GameData.Battle.Turn(((ActSlotController)_currentSlot).Model.Act);
            gameObject.SetActive(false);
        }

        public override void OnCancel() { }
    }
}