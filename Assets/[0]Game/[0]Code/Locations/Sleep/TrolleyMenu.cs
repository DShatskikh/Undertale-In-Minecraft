using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Sleep
{
    public sealed class TrolleyMenu : MonoBehaviour
    {
        [SerializeField]
        private Button[] _stopButtons;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private AudioClip _sfx;
        
        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                if (EventSystem.current.currentSelectedGameObject == _stopButtons[0].gameObject)
                {
                    GameData.EffectAudioSource.clip = _sfx;
                    GameData.EffectAudioSource.Play();
                    
                    _stopButtons[0].onClick.Invoke();
                }
                else if (EventSystem.current.currentSelectedGameObject == _stopButtons[1].gameObject)
                {
                    GameData.EffectAudioSource.clip = _sfx;
                    GameData.EffectAudioSource.Play();
                    
                    _stopButtons[1].onClick.Invoke();
                }
                else if (EventSystem.current.currentSelectedGameObject == _stopButtons[2].gameObject)
                {
                    GameData.EffectAudioSource.clip = _sfx;
                    GameData.EffectAudioSource.Play();
                    
                    _stopButtons[2].onClick.Invoke();
                }
                else if (EventSystem.current.currentSelectedGameObject == _cancelButton.gameObject)
                {
                    GameData.EffectAudioSource.clip = _sfx;
                    GameData.EffectAudioSource.Play();
                    
                    _cancelButton.onClick.Invoke();
                }
            }

            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (EventSystem.current.currentSelectedGameObject == _stopButtons[0].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[1].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _stopButtons[1].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[2].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _cancelButton.gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[2].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                }
                else
                {
                    if (EventSystem.current.currentSelectedGameObject == _stopButtons[2].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[1].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _stopButtons[1].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[0].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _cancelButton.gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[0].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                }
            }

            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (EventSystem.current.currentSelectedGameObject == _stopButtons[0].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[1].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _stopButtons[2].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[1].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _cancelButton.gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_stopButtons[1].gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                }
                else
                {
                    if (EventSystem.current.currentSelectedGameObject == _stopButtons[0].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _stopButtons[1].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                    else if (EventSystem.current.currentSelectedGameObject == _stopButtons[2].gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
                        GameData.EffectAudioSource.clip = _sfx;
                        GameData.EffectAudioSource.Play();
                    }
                }
            }
        }

        public void Open(Trolley trolley, StopEnum stopEnum)
        {
            GameData.Character.enabled = false;
            gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_stopButtons[(int)stopEnum].gameObject);

            for (var index = 0; index < _stopButtons.Length; index++)
            {
                var stopButton = _stopButtons[index];
                var i = index;
                stopButton.onClick.AddListener(() => trolley.StartMove(i));
            }
        }

        public void Close()
        {
            GameData.Character.enabled = true;
            
            foreach (var stopButton in _stopButtons)
            {
                stopButton.onClick.RemoveAllListeners();
            }
            
            gameObject.SetActive(false);
        }
    }
}