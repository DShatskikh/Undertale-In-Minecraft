using System;
using System.Collections;
using Game.Commands;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game
{
    public class MonologViewModel : MonoBehaviour
    {
        [SerializeField]
        private MonologView _view;

        [SerializeField]
        private LocalizedString _continueString;

        private int _index;
        private LocalizedString[] _texts;
        private Coroutine _coroutine;
        private string _finallyText;
        private string _currentText;
        
        private AsyncOperationHandle<string> _continueTextOperation;
        private AsyncOperationHandle<string> _finallyTextOperation;

        private MonologModel _model;
        
        public readonly ReactiveProperty<string> Text = new ReactiveProperty<string>();
        public readonly ReactiveProperty<string> ContinueText = new ReactiveProperty<string>();
        public readonly ReactiveProperty<bool> IsEndWrite = new ReactiveProperty<bool>();
        
        private AudioClip _sound;
        public event Action Showed;
        public event Action Closed;
        public event Action Write;
        
        private void Awake()
        {
            _model = new MonologModel();
            _model.Text.Changed += TextOnChanged;
            _model.IsEndWrite.Changed += IsEndWriteOnChanged;
            
            _view.Init(this);
        }

        private void OnDestroy()
        {
            _model.Text.Changed -= TextOnChanged;
            _model.IsEndWrite.Changed -= IsEndWriteOnChanged;
        }

        private void TextOnChanged(string value)
        {
            Text.Value = value;
        }

        private void IsEndWriteOnChanged(bool value)
        {
            IsEndWrite.Value = value;
        }

        private IEnumerator Start()
        {
            var loadTextCommand = new LoadTextCommand(_continueString);
            yield return loadTextCommand.Await().ContinueWith(() => ContinueText.Value = loadTextCommand.Result);
        }

        public void Show(LocalizedString[] texts, AudioClip sound = null)
        {
            gameObject.SetActive(true);
            Showed?.Invoke();
            _model.Text.Value = "";
            GameData.ToMenuButton.gameObject.SetActive(false);
            _sound = sound;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.CharacterController.enabled = false;

            _index = 0;
            _texts = texts;
            Next();
        }

        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            
            _finallyTextOperation = _texts[_index].GetLocalizedStringAsync();
            
            while (!_finallyTextOperation.IsDone)
            {
                yield return null;
            }
            
            _finallyText = _finallyTextOperation.Result;
            
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
                _model.Text.Value = _currentText;
                Write?.Invoke();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }

        public void Next()
        {
            if (_currentText != _finallyText)
            {
                ShowFinallyText();
                return;
            }

            if (IsEndReplica())
            {
                Close();
                return;
            }

            GameData.EffectSoundPlayer.Play(_sound ? _sound : GameData.AssetProvider.ClickSound);

            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypeText());
            _index++;
        }

        public void ShowFinallyText()
        {
            if (!_finallyTextOperation.IsDone)
                return;
            
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _currentText = _finallyText;
            _model.Text.Value = _finallyText;
        }

        private void Close()
        {
            Closed?.Invoke();
            GameData.ToMenuButton.gameObject.SetActive(true);
            EventBus.Submit = null;
            EventBus.Cancel = null;
            GameData.CharacterController.enabled = true;
            gameObject.SetActive(false);
            EventBus.CloseMonolog?.Invoke();
            EventBus.CloseMonolog = null;
        }

        private bool IsEndReplica()
        {
            return _index >= _texts.Length;
        }
    }
}