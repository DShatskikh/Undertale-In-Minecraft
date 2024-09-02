using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MonologView : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;
        
        [SerializeField]
        private TMP_Text _label, _continueLabel, _hint;
        
        [SerializeField]
        private Button _continueButton;
        
        private MonologViewModel _viewModel;

        public void Init(MonologViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.IsShowed.Changed += IsShowedOnChanged;
            _viewModel.Write += OnWrite;
            _viewModel.Text.Changed += TextOnChanged;
            _viewModel.IsEndWrite.Changed += IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed += ContinueTextOnChanged;
            _viewModel.LoadText += OnLoadText;
        }

        private void OnDestroy()
        {
            _viewModel.IsShowed.Changed -= IsShowedOnChanged;
            _viewModel.Write -= OnWrite;
            _viewModel.Text.Changed -= TextOnChanged;
            _viewModel.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed -= ContinueTextOnChanged;
            _viewModel.LoadText -= OnLoadText;
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
            }
        }

        private void OnLoadText()
        {
            EventBus.SubmitUp = _viewModel.ShowAll;
            EventBus.CancelUp = _viewModel.ShowAll;
            
            _hint.gameObject.SetActive(true);
        }

        private void IsEndWriteOnChanged(bool value)
        {
            if (value)
            {
                EventBus.SubmitUp = null;
                EventBus.CancelUp = null;

                _continueButton.gameObject.SetActive(true);
                _hint.gameObject.SetActive(false);
                _continueButton.onClick.AddListener(OnContinueClicked);
            }
            else
            {
                
            }
        }

        private void OnContinueClicked()
        {
            _continueButton.onClick.RemoveListener(OnContinueClicked);
            _continueButton.gameObject.SetActive(false);
            _viewModel.Next();
        }
        
        private void OnWrite()
        {
            _audioSource.Play();
        }

        private void ContinueTextOnChanged(string value)
        {
            _continueLabel.text = value;
        }

        private void TextOnChanged(string value)
        {
            _label.text = value;
        }
    }
}