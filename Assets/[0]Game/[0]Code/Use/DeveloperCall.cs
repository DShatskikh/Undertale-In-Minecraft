using System;
using UnityEngine;

namespace Game
{
    public class DeveloperCall : MonoBehaviour
    {
        [Header("Ключи")]
        [SerializeField]
        private SaveKeyBool _badKey;
        
        [SerializeField]
        private SaveKeyBool _goodKey;
        
        [SerializeField]
        private SaveKeyBool _strangeKey;

        [SerializeField]
        private SaveKeyBool _openDeveloperRoomKey;
        
        [SerializeField]
        private SaveKeyInt _currentEndKey;
        
        [SerializeField]
        private SaveKeyInt _previousEndKey;

        [SerializeField]
        private SaveKeyInt _palesosKey;
        
        [Header("Монологи")]
        [SerializeField]
        private OpenMonolog _error;
        
        [Header("1 концовка")]
        [SerializeField]
        private OpenMonolog _good1;

        [SerializeField]
        private OpenMonolog _bad1;
        
        [SerializeField]
        private OpenMonolog _strange1;
        
        [Header("2 концовка")]
        [SerializeField]
        private OpenMonolog _notStrange2;
        
        [SerializeField]
        private OpenMonolog _notOther2;
        
        [Header("3 концовка")]
        [SerializeField]
        private OpenMonolog _finally;
        
        [Header("Другое")]
        [SerializeField]
        private OpenMonolog _openDeveloperRoom;
        
        [SerializeField]
        private OpenDialog _palesos;

        private Saver _saver => GameData.Saver;
        
        public void Use()
        {
            if (_saver.LoadKey(_palesosKey) != 3)
                GetMonolog().Use();
            else
            {
                _palesos.Use();
                EventBus.OnCloseDialog += () => GetMonolog().Use();
            }
        }

        private int GetCountEnds()
        {
            bool isBad = _saver.LoadKey(_badKey);
            bool isGood = _saver.LoadKey(_goodKey);
            bool isStrange = _saver.LoadKey(_strangeKey);
            
            var countEnds = 0;

            if (isBad)
                countEnds++;
            
            if (isGood)
                countEnds++;
            
            if (isStrange)
                countEnds++;

            return countEnds;
        }

        private OpenMonolog GetMonolog()
        {
            var countEnds = GetCountEnds();
            var currentEnd = (Endings)GameData.Saver.LoadKey(_currentEndKey);
            var previousEnd = (Endings)GameData.Saver.LoadKey(_currentEndKey);

            if (currentEnd == Endings.Strange && !_saver.LoadKey(_openDeveloperRoomKey))
                EventBus.OnCloseDialog += () => _openDeveloperRoom.Use();
            
            if (countEnds == 1)
            {
                switch (currentEnd)
                {
                    case Endings.None:
                        break;
                    case Endings.Bad:
                        return _bad1;
                    case Endings.Good:
                        return _good1;
                    case Endings.Strange:
                        return _strange1;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (countEnds == 2)
            {
                
            }

            print(countEnds);
            print(currentEnd);
            return _error;
        }
    }
}