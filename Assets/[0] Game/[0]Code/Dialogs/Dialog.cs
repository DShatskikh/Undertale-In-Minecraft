using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] 
        private DialogView _view;

        [SerializeField] 
        private UIDocument _ui;
        
        [SerializeField]
        private AudioSource _audioSource;
        
        [SerializeField] 
        private AudioClip _clickSound;
        
        private Replica[] _replicas;
        private int _indexReplica;
        private Coroutine _coroutine;
        private string _finallyText;
        private string _currentText;

        public void SetReplicas(Replica[] replicas)
        {
            gameObject.SetActive(true);
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            var button = _ui.rootVisualElement.Q<Button>("Next_button");
            button.clicked += Next;
            EventBus.OnSubmit = Next;
            EventBus.OnCancel = ShowFinallyText;
            
            GameData.Character.enabled = false;
            _replicas = replicas;
            _indexReplica = 0;

            UpdateView();
            _coroutine = StartCoroutine(TypeText());
        }

        private void Next()
        {
            print("Work");
            
            if (!gameObject.activeSelf)
                Debug.LogError("Ошибка");

            if (_currentText != _finallyText)
            {
                ShowFinallyText();
                return;
            }

            print("Next");
            
            GameData.EffectAudioSource.clip = _clickSound;
            GameData.EffectAudioSource.Play();
            
            _indexReplica++;
            
            if (_indexReplica >= _replicas.Length)
            {
                Close();
                return;
            }

            UpdateView();
            _coroutine = StartCoroutine(TypeText());
        }

        private void ShowFinallyText()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _currentText = _finallyText;
            _view.SetText(_finallyText);
            print("ShowFinallyText");
        }
        
        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            _finallyText = _replicas[_indexReplica].LocalizationString.GetLocalizedString();
            _currentText = "";

            while (_countSymbol != _finallyText.Length)
            {
                if (_finallyText[_countSymbol] == '<')
                {
                    while (_finallyText[_countSymbol] != '>')
                    {
                        _currentText += _finallyText[_countSymbol];
                        _countSymbol++;
                    }
                }

                _currentText += _finallyText[_countSymbol];
                _view.SetText(_currentText);
                _audioSource.Play();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }

        private void UpdateView()
        {
            var replica = _replicas[_indexReplica];
            _view.SetText(replica.LocalizationString.GetLocalizedString());
            _view.SetIcon(replica.Icon);
        }

        private void Close()
        {
            print("Close");
            EventBus.OnSubmit = null;
            EventBus.OnCancel = null;
            GameData.Character.enabled = true;
            gameObject.SetActive(false);
            EventBus.OnCloseDialog?.Invoke();
            EventBus.OnCloseDialog = null;
        }
    }
}