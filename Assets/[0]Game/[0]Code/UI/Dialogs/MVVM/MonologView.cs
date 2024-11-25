using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MonologView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label, _continueLabel, _hint;

        [SerializeField]
        private TextAnimatorPlayer _textAnimatorPlayer;
        
        [SerializeField]
        private Button _continueButton;

        [SerializeField]
        private AudioSource _audioSource;
        
        private MonologViewModel _viewModel;

        public void Init(MonologViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.IsShowed.Changed += IsShowedOnChanged;
            _viewModel.Text.Changed += OnLoad;
            _viewModel.IsEndWrite.Changed += IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed += ContinueTextOnChanged;
            _viewModel.Write += OnWrite;
            _viewModel.ShowedAll += OnShowedAllText;
            
            _label.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _viewModel.IsShowed.Changed -= IsShowedOnChanged;
            _viewModel.Text.Changed -= OnLoad;
            _viewModel.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed -= ContinueTextOnChanged;
            _viewModel.Write -= OnWrite;
            _viewModel.ShowedAll -= OnShowedAllText;
        }

        private void IsShowedOnChanged(bool value)
        {
            if (value)
            {
                _continueButton.gameObject.SetActive(false);
                _hint.gameObject.SetActive(false);
            }
            else
            {
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
                _label.gameObject.SetActive(false);
            }
        }

        private void OnShowedAllText()
        {
            _textAnimatorPlayer.SkipTypewriter();
        }

        private void IsEndWriteOnChanged(bool value)
        {
            if (value)
            {
                EventBus.SubmitUp -= _viewModel.ShowAll;
                EventBus.CancelUp -= _viewModel.ShowAll;

                _continueButton.gameObject.SetActive(true);
                _hint.gameObject.SetActive(false);
                _continueButton.onClick.AddListener(OnContinueClicked);
            }
        }

        private void OnContinueClicked()
        {
            _label.gameObject.SetActive(false);
            _continueButton.onClick.RemoveListener(OnContinueClicked);
            _continueButton.gameObject.SetActive(false);
            _viewModel.Next();
        }

        private void ContinueTextOnChanged(string value)
        {
            _continueLabel.text = value;
        }

        private void OnLoad(string value)
        {
            EventBus.SubmitUp += _viewModel.ShowAll;
            EventBus.CancelUp += _viewModel.ShowAll;
            
            _hint.gameObject.SetActive(true);
            
            _label.gameObject.SetActive(true);
            _label.text = value;
            _textAnimatorPlayer.StartShowingText();
        }

        private void OnWrite()
        { 
            _audioSource.Play();   
        }
    }
}