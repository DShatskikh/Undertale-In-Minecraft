using Febucci.UI;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class TextAnimatorTypewriterEffect : AbstractTypewriterEffect
    {
        [Header("TextAnimator")]
        [SerializeField]
        private TextAnimatorPlayer _textAnimatorPlayer;
       
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private GameObject _button;
        
        [SerializeField]
        private MMF_Player _iconMmfPlayer;

        [SerializeField]
        private GameObject _hint;
        
        private bool _isPlaying;

        public override bool isPlaying => _isPlaying;

        public override void OnEnable()
        {
            base.OnEnable();
            gameObject.SetActive(true);
            GameData.InputManager.Hide();
            GameData.CharacterController.enabled = false;
            _textAnimatorPlayer.onCharacterVisible.AddListener((c) => OnWrite());
            _textAnimatorPlayer.onTextShowed.AddListener(Stop);
            _textAnimatorPlayer.onTypewriterStart.AddListener(OnTypewriterStart);
            _button.SetActive(false);
            
            EventBus.SubmitUp += ShowAllText;
            EventBus.CancelUp += ShowAllText;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _textAnimatorPlayer.onCharacterVisible.RemoveListener((c) => OnWrite());
            _textAnimatorPlayer.onTextShowed.RemoveListener(Stop);
            _textAnimatorPlayer.onTypewriterStart.RemoveListener(OnTypewriterStart);
            gameObject.SetActive(false);
            
            EventBus.SubmitUp -= ShowAllText;
            EventBus.CancelUp -= ShowAllText;
            
            GameData.InputManager.Show();
            GameData.CharacterController.enabled = true;
        }

        public override void Start() { }

        public override void Stop()
        {
            _isPlaying = false;
            _button.SetActive(true);
            _hint.SetActive(false);
            StopTyping();
        }

        public override void StartTyping(string text, int fromIndex = 0)
        {
            _textAnimatorPlayer.StartShowingText();
        }

        public override void StopTyping() { }

        private void OnWrite()
        {
            _audioSource.Play();
        }

        private void OnTypewriterStart()
        {
            _isPlaying = true;
            _button.SetActive(false);
            _hint.SetActive(true);
            _iconMmfPlayer.PlayFeedbacks();
        }

        private void ShowAllText()
        {
            _textAnimatorPlayer.SkipTypewriter();
        }
    }
}