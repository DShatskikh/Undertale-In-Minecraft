using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class PlatePuzzle : MonoBehaviour
    {
        [SerializeField]
        private Plate[] _plates;

        [SerializeField] 
        private UnityEvent _finishPuzzleEvent, _deactivateEvent;

        [SerializeField]
        private PlaySoundEffect _playSound;
        
        private Coroutine _coroutine;

        private void OnEnable()
        {
            StartProcess();
        }

        public void StartProcess()
        {
            foreach (var plate in _plates)
            {
                if (plate.IsActive)
                    return;
            }
            
            _coroutine = StartCoroutine(Process());
        }

        public void ActivateNoEvent()
        {
            print("ActivateNoEvent");
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            foreach (var plate in _plates)
            {
                plate.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (var plate in _plates)
            {
                plate.Deactivate();
            }
            
            print("_deactivateEvent");
            _deactivateEvent.Invoke();
        }
        
        private IEnumerator Process()
        {
            bool _isFinish = false;
            
            while (!_isFinish)
            {
                _isFinish = true;
                
                foreach (var place in _plates)
                {
                    if (!place.IsActive)
                    {
                        _isFinish = false;
                        break;
                    }
                }

                yield return null;
            }

            enabled = false;
            _finishPuzzleEvent?.Invoke();
            GameData.Saver.Save();
            yield return new WaitForSeconds(0.2f);
            _playSound.Play();
        }
    }
}