using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class SymbolPlatePuzzleManager : MonoBehaviour
    {
        [SerializeField]
        private string _key;

        [SerializeField]
        private List<SymbolPlatePuzzle> _plates;

        [SerializeField]
        private PlaySoundEffect _winSound, _notLoseSound;

        [SerializeField]
        private UnityEvent _event;

        private string _currentKey;
        private bool _isFinish;
        
        public void ResetPuzzle()
        {
            if (_isFinish)
                return;
            
            if (_key == _currentKey)
            {
                _isFinish = true;
                _event.Invoke();
                _winSound.Play();
            }
            else
            {
                _currentKey = string.Empty;
                
                foreach (var plate in _plates) 
                    plate.ResetPlate();
                
                _notLoseSound.Play();
            }
        }
        
        
        public void AddSymbol(char symbol) => 
            _currentKey += symbol;

        public void AllActivateNoEvent()
        {
            foreach (var plate in _plates) 
                plate.PressView();

            _isFinish = true;
        }
    }
}