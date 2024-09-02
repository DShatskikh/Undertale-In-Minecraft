using System;
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
        
        private DialogViewModel _viewModel;
        private AudioClip _sound;

        public void Init(DialogViewModel viewModel)
        {
            _viewModel = viewModel;
            
            _viewModel.IsShowed.Changed += IsShowedOnChanged;
            _viewModel.ContinueText.Changed += ContinueTextOnChanged;
            _viewModel.Icon.Changed += IconOnChanged;
            _viewModel.IsEndWrite.Changed += IsEndWriteOnChanged;
            _viewModel.LoadText += OnLoadText;
            _viewModel.Write += OnWrite;
            _viewModel.Next += OnNext;
            _viewModel.Text.Changed += TextOnChanged;
        }

        private void OnDestroy()
        {
            _viewModel.IsShowed.Changed -= IsShowedOnChanged;
            _viewModel.ContinueText.Changed -= ContinueTextOnChanged;
            _viewModel.Icon.Changed -= IconOnChanged;
            _viewModel.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _viewModel.LoadText -= OnLoadText;
            _viewModel.Write -= OnWrite;
            _viewModel.Next -= OnNext;
            _viewModel.Text.Changed -= TextOnChanged;
        }

        private void IsShowedOnChanged(bool value)
        {
            if (value)
            {
                _iconMmfPlayer.PlayFeedbacks();
                _hint.gameObject.SetActive(true);
                _continueButton.gameObject.SetActive(false);
            }
            else
            {
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            }
        }

        private void OnLoadText()
        {
            EventBus.SubmitUp = OnTap;
            EventBus.CancelUp = OnTap;
        }

        private void OnNext()
        {
            _iconMmfPlayer.PlayFeedbacks();
            GameData.EffectSoundPlayer.Play(_sound ? _sound : GameData.AssetProvider.ClickSound);
        }

        private void IsEndWriteOnChanged(bool value)
        {
            EventBus.Submit = null;
            EventBus.Cancel = null;
            
            if (value)
            {
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

        private void TextOnChanged(string value)
        {
            _label.text = value;
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
            
            _viewModel.ShowAllText();
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
    }
}