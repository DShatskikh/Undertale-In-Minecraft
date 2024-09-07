using Febucci.UI;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        
        [SerializeField]
        private Button _continueButton;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label, _continueLabel, _hint;

        [SerializeField]
        private MMF_Player _iconMmfPlayer;
                
        [SerializeField]
        private TextAnimatorPlayer _textAnimatorPlayer;
        
        private DialogViewModel _viewModel;
        private AudioClip _sound;

        public void Init(DialogViewModel viewModel)
        {
            _viewModel = viewModel;
            
            _viewModel.IsShowed.Changed += IsShowedOnChanged;
            _viewModel.Text.Changed += OnLoad;
            _viewModel.IsEndWrite.Changed += IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed += ContinueTextOnChanged;
            _viewModel.Write += OnWrite;
            _viewModel.Icon.Changed += IconOnChanged;
            _viewModel.ShowedAll += OnShowedAllText;
        }

        private void OnDestroy()
        {
            _viewModel.IsShowed.Changed -= IsShowedOnChanged;
            _viewModel.Text.Changed -= OnLoad;
            _viewModel.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed -= ContinueTextOnChanged;
            _viewModel.Write -= OnWrite;
            _viewModel.Icon.Changed -= IconOnChanged;
            _viewModel.ShowedAll -= OnShowedAllText;
        }

        private void IsShowedOnChanged(bool value)
        {
            if (value)
            {
                GameData.EffectSoundPlayer.Play(_sound ? _sound : GameData.AssetProvider.ClickSound);
            }
            else
            {
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            }
        }

        private void IsEndWriteOnChanged(bool value)
        {
            if (value)
            {
                EventBus.SubmitUp -= _viewModel.ShowAll;
                EventBus.CancelUp -= _viewModel.ShowAll;
                
                _hint.gameObject.SetActive(false);
                _continueButton.gameObject.SetActive(true);
                _continueButton.onClick.AddListener(OnContinueClicked);
            }
            else
            {
                _hint.gameObject.SetActive(true);
                _continueButton.gameObject.SetActive(false);
            }
        }

        private void OnLoad(string value)
        {
            EventBus.SubmitUp += _viewModel.ShowAll;
            EventBus.CancelUp += _viewModel.ShowAll;
            
            _label.text = value;
            _hint.gameObject.SetActive(true);
            _iconMmfPlayer.PlayFeedbacks();
            _textAnimatorPlayer.StartShowingText();
        }

        public void SetSound(AudioClip sound)
        {
            _sound = sound;
        }

        private void OnContinueClicked()
        {
            _continueButton.onClick.RemoveListener(OnContinueClicked);
            _viewModel.TryNext();
        }

        private void OnTap()
        {
            EventBus.SubmitUp = null;
            EventBus.CancelUp = null;
            
            _viewModel.ShowAll();
        }

        private void IconOnChanged(Sprite value)
        {
            _icon.sprite = value;
        }

        private void OnWrite()
        {
            _audioSource.Play();
        }

        private void ContinueTextOnChanged(string value)
        {
            _continueLabel.text = value;
        }

        private void OnShowedAllText()
        {
            _textAnimatorPlayer.SkipTypewriter();
        }
    }
}