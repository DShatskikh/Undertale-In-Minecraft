using Febucci.UI;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;
using TMPro;
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

        [SerializeField]
        private GameObject _namePanel;
        
        [SerializeField]
        private TMP_Text _nameLabel;
        
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
            var actorName = DialogueManager.currentConversationState.subtitle.speakerInfo.nameInDatabase;
            var clipName = DialogueManager.masterDatabase.GetActor(actorName).LookupValue("AudioClip");
            var clipPath = "AudioClips/" + (clipName != "" ? clipName : "snd_txtlan_ch1");
            var clip = Resources.Load<AudioClip>(clipPath);
            _audioSource.clip = clip;

            var useDisplayName = DialogueManager.masterDatabase.GetActor(actorName).LookupValue("Display Name");
            _namePanel.SetActive(useDisplayName != string.Empty);
                
            _textAnimatorPlayer.StartShowingText();
        }

        public override void StopTyping()
        {
            _button.SetActive(true);
        }

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