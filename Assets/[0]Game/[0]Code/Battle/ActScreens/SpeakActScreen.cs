using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SpeakActScreen : UIPanelBase
    {
        [SerializeField, Range(1f, 1000f)]
        private float _radius = 90;
        
        [SerializeField]
        private float _speed = 1.28f;
        
        [Header("Hint")]
        [SerializeField]
        private Transform _hintContainer;

        [SerializeField]
        private TMP_Text _comma;

        [SerializeField]
        private Image _iconForm;
        
        [SerializeField]
        private Sprite _leftBracket, _rightBracket;
        
        [Header("Slots")]
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private Transform _arrow;

        [SerializeField]
        private Transform[] _arrowPoints;
        
        private List<Image> _hintElements = new List<Image>();
        private float _angle;
        private bool _isOtherSide;
        private SpeakActConfig _config;
        private int _selectHintIndex;
        private int _selectY;

        public void Init(SpeakActConfig config)
        {
            Activate(true);
            _config = config;

            var leftBracket = Instantiate(_iconForm, _hintContainer);
            leftBracket.sprite = _leftBracket;
            
            for (int i = 0; i < config.ImageElements.Length; i++)
            {
                var hint = Instantiate(_iconForm, _hintContainer);
                hint.sprite = config.ImageElements[i];
                hint.color = new Color(0.75f, 0.75f, 0.75f, 1f);
                _hintElements.Add(hint);

                if (i < config.ImageElements.Length - 1)
                {
                    var comma = Instantiate(_comma, _hintContainer);
                }
            }
            
            var rightBracket = Instantiate(_iconForm, _hintContainer);
            rightBracket.sprite = _rightBracket;

            for (int i = 0; i < config.ImageElements.Length; i++)
            {
                var slot = Instantiate(GameData.AssetProvider.SpeakAtcSlotPrefab, _container);
                slot.Init(config.ImageElements[i]);
                slot.SetSelected(false);
                _slots.Add(new Vector2(0, i), slot);
            }

            HintUpgrade();
            StartCoroutine(AwaitMoveSlots());
        }

        public override void OnSubmitDown()
        {
            
        }

        public override void OnSubmitUp()
        {
            if (_slots.Count != 0)
            {
                _isOtherSide = !_isOtherSide;

                var slotPair = GetCurrentSlot();
                _slots.Remove(slotPair.Key);
                bool isEqual = _hintElements[_selectHintIndex].sprite == ((SpeakAtcSlot)slotPair.Value).GetIcon;
                _hintElements[_selectHintIndex].sprite = ((SpeakAtcSlot)slotPair.Value).GetIcon;
                Destroy(slotPair.Value.gameObject);

                var pairs = new Dictionary<Vector2, BaseSlotController>();
                int i = 0;

                foreach (var slot in _slots)
                {
                    pairs.Add(new Vector2(0, i), slot.Value);
                    i++;
                }

                _slots = pairs;
                
                _selectHintIndex++;
                HintUpgrade(isEqual);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            }
            
            if (_slots.Count == 0)
            {
                Activate(false);

                var isSuccess = true;

                for (int i = 0; i < _hintElements.Count; i++)
                {
                    if (_config.ImageElements[i] != _hintElements[i].sprite)
                    {
                        isSuccess = false;
                        break;
                    }
                }

                var commands = new List<CommandBase>
                {
                    new MessageCommand(GameData.Battle.MessageBox, isSuccess ? _config.SuccessSystemMessage : _config.FailedSystemMessage),
                    new MessageCommand(GameData.Battle.EnemyMessageBox, isSuccess ? _config.SuccessReaction : _config.FailedReaction),
                    new AddProgressCommand(isSuccess ? _config.SuccessProgress : _config.FailedProgress, GameData.Battle.AddProgressLabel, GameData.Battle.AddProgressData),
                    new StartEnemyTurnCommand()
                };

                print(isSuccess);
                
                GameData.CommandManager.StartCommands(commands);

                Destroy(gameObject);
            }
        }

        public override void OnCancel()
        {
            
        }

        public override void OnSlotIndexChanged(Vector2 direction)
        {
            _selectY -= (int)direction.y;

            if (_selectY > 1)
                _selectY = 1;
            else if (_selectY < -1) 
                _selectY = -1;

            if (_arrow.position == _arrowPoints[_selectY + 1].position)
                return;
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SelectSound);
            _arrow.position = _arrowPoints[_selectY + 1].position;
        }
        
        private IEnumerator AwaitMoveSlots()
        {
            while (true)
            {
                _angle += (_isOtherSide ? -_speed : _speed) * Time.deltaTime;
                _angle %= 360;

                foreach (var slot in _slots)
                    slot.Value.transform.position =
                        _container.position + GetPosition(_angle, (int)slot.Key.y, _slots.Count);

                GetCurrentSlot();
                yield return null;
            }
        }
        
        private Vector3 GetPosition(float angle, int index, int length) 
        {
            var objectAngle = angle + (360f / length) * index; // Угол для текущего объекта
            var x = Mathf.Cos(objectAngle * Mathf.Deg2Rad) * _radius; // Вычисляем координату X
            var y = Mathf.Sin(objectAngle * Mathf.Deg2Rad) * _radius; // Вычисляем координату Z

            return new Vector3(x, y, 0); // Возвращаем новую позицию
        }

        private KeyValuePair<Vector2, BaseSlotController> GetCurrentSlot()
        {
            switch (_selectY)
            {
                case 0:
                    return GetLeftSlot();
                case -1:
                    return GetUpSlot();
                case 1:
                    return GetDownSlot();
            }

            return new KeyValuePair<Vector2, BaseSlotController>();
        }
        
        private KeyValuePair<Vector2, BaseSlotController> GetLeftSlot()
        {
            var minX = float.MaxValue;
            var minXSlot = new KeyValuePair<Vector2, BaseSlotController>();

            foreach (var slot in _slots)
            {
                slot.Value.SetSelected(false);
                
                if (slot.Value.transform.position.x < minX)
                {
                    minX = slot.Value.transform.position.x;
                    minXSlot = slot;
                }
            }
            
            minXSlot.Value.SetSelected(true);
            return minXSlot;
        }
        
        private KeyValuePair<Vector2, BaseSlotController> GetUpSlot()
        {
            var maxY = float.MinValue;
            var maxYSlot = new KeyValuePair<Vector2, BaseSlotController>();

            foreach (var slot in _slots)
            {
                slot.Value.SetSelected(false);
                
                if (slot.Value.transform.position.y > maxY)
                {
                    maxY = slot.Value.transform.position.y;
                    maxYSlot = slot;
                }
            }
            
            maxYSlot.Value.SetSelected(true);
            return maxYSlot;
        }

        private KeyValuePair<Vector2, BaseSlotController> GetDownSlot()
        {
            var minY = float.MaxValue;
            var minYSlot = new KeyValuePair<Vector2, BaseSlotController>();

            foreach (var slot in _slots)
            {
                slot.Value.SetSelected(false);
                
                if (slot.Value.transform.position.y < minY)
                {
                    minY = slot.Value.transform.position.y;
                    minYSlot = slot;
                }
            }
            
            minYSlot.Value.SetSelected(true);
            return minYSlot;
        }
        
        private void HintUpgrade(bool isEqual = true)
        {
            if (_selectHintIndex != 0)
                _hintElements[_selectHintIndex - 1].color = isEqual ? Color.white : Color.red;
            
            if (_slots.Count != 0)
                _hintElements[_selectHintIndex].color = GameData.AssetProvider.SelectColor;
        }
    }
}