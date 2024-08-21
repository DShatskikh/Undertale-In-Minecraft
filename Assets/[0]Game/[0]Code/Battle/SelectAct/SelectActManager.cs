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
                int rowIndex = acts.Length - i - 1;
                int columnIndex = 0;
                slot.SetSelected(false);
                _slots.Add(new Vector2(columnIndex, rowIndex), slot);
            }
            
            _currentIndex = new Vector2(0, acts.Length - 1);
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

        public override void OnSlotIndexChanged(Vector2 direction)
        {
            var nearestKey = _currentIndex;
            var nearestDistance = float.MaxValue;

            var startPoint = _currentSlot.transform.position;
            
            foreach (var slot in _slots)
            {
                var difference = slot.Value.transform.position - startPoint; 
                var distance = Vector2.Distance(startPoint, slot.Value.transform.position);

                if (slot.Key == nearestKey)
                    continue;
                    
                if (distance == 0)
                    continue;
                    
                if ((direction.x > 0 && (difference.x < 0 || Mathf.Abs(difference.x) < Mathf.Abs(difference.y))) ||
                    (direction.x < 0 && (difference.x > 0 || Mathf.Abs(difference.x) < Mathf.Abs(difference.y))) ||
                    (direction.y > 0 && (difference.y < 0 || Mathf.Abs(difference.y) < Mathf.Abs(difference.x))) ||
                    (direction.y < 0 && (difference.y > 0 || Mathf.Abs(difference.y) < Mathf.Abs(difference.x)))
                   )
                    continue;

                if (nearestDistance >= distance)
                {
                    nearestKey = slot.Key;
                    nearestDistance = distance;
                }
                    
                Debug.Log("" + slot.Value.gameObject.name + " direction: " + direction.x + " difference: " + difference.x + " startPoint: " + startPoint + " distance: " + distance + " nearestDistance: " + nearestDistance);
            }
            
            _currentSlot.SetSelected(false);
            _currentIndex = nearestKey;
            _currentSlot.SetSelected(true);
        }
    }
}