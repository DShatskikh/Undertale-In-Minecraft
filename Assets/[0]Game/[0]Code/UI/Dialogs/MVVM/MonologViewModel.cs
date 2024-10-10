using System;
using System.Collections;
using Febucci.UI;
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

        [SerializeField]
        private TextAnimatorPlayer _textAnimatorPlayer;

        private int _index;
        private LocalizedString[] _texts;
        private Coroutine _coroutine;
        private bool _isLoad;

        private AsyncOperationHandle<string> _continueTextOperation;

        private MonologModel _model;
        
        public readonly ReactiveProperty<string> Text = new();
        public readonly ReactiveProperty<string> ContinueText = new();
        public readonly ReactiveProperty<bool> IsEndWrite = new();
        public readonly ReactiveProperty<bool> IsShowed = new();
        
        private AudioClip _sound;
        public event Action Write;
        public event Action ShowedAll;
        
        private void Awake()
        {
            _model = new MonologModel();
            _model.Text.Changed += TextOnChanged;
            _model.IsEndWrite.Changed += IsEndWriteOnChanged;
            _textAnimatorPlayer.onTextShowed.AddListener(() => _model.IsEndWrite.Value = true);
            _textAnimatorPlayer.onTypewriterStart.AddListener(() => _model.IsEndWrite.Value = false);
            _textAnimatorPlayer.onCharacterVisible.AddListener((c) => Write?.Invoke());
            
            _view.Init(this);
        }

        private void OnDestroy()
        {
            _model.Text.Changed -= TextOnChanged;
            _model.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _textAnimatorPlayer.onTextShowed.RemoveListener(() => _model.IsEndWrite.Value = true);
            _textAnimatorPlayer.onTypewriterStart.RemoveListener(() => _model.IsEndWrite.Value = false);
            _textAnimatorPlayer.onCharacterVisible.RemoveListener((c) => Write?.Invoke());
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
            IsShowed.Value = true;
            GameData.InputManager.Hide();
            _sound = sound;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.CharacterController.enabled = false;
            _index = 0;
            _texts = texts;
            LoadText();
        }

        public void Next()
        {
            if (!IsEndWrite.Value)
            {
                ShowAll();
                return;
            }

            _isLoad = false;
            
            if (IsEndReplica())
            {
                Close();
                return;
            }

            GameData.EffectSoundPlayer.Play(_sound ? _sound : GameData.AssetProvider.ClickSound);
            LoadText();
        }

        private void LoadText()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitLoadText());
            _index++;
        }
        
        private IEnumerator AwaitLoadText()
        {
            var loadTextCommand = new LoadTextCommand(_texts[_index]);
            yield return loadTextCommand.Await();
            _model.Text.Value = loadTextCommand.Result;
            _isLoad = true;
        }

        public void ShowAll()
        {
            if (!_isLoad)
                return;
            
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            ShowedAll?.Invoke();
        }

        private void Close()
        {
            IsShowed.Value = false;
            _model.Text.Value = "";
            GameData.InputManager.Show();
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