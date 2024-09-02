using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SelectView : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField]
        private Button _yesButton;
        
        [SerializeField]
        private Button _noButton;
        
        [SerializeField]
        private TMP_Text _label, _yesLabel, _noLabel;
        
        private SelectViewModel _selectViewModel;

        public void Init(SelectViewModel selectViewModel)
        {
            _selectViewModel = selectViewModel;
            _selectViewModel.YesResultString.Changed += OnYesResultStringChanged;
            _selectViewModel.NoResultString.Changed += OnNoResultStringChanged;
            _selectViewModel.Text.Changed += OnTextChanged;
            _selectViewModel.IsShowed.Changed += OnShowed;
            _selectViewModel.Write += OnWrite;
            _selectViewModel.LoadText += OnLoadText;
        }

        private void OnDestroy()
        {
            _selectViewModel.YesResultString.Changed -= OnYesResultStringChanged;
            _selectViewModel.NoResultString.Changed -= OnNoResultStringChanged;
            _selectViewModel.Text.Changed -= OnTextChanged;
            _selectViewModel.IsShowed.Changed -= OnShowed;
            _selectViewModel.Write -= OnWrite;
            _selectViewModel.LoadText -= OnLoadText;
        }

        private void OnLoadText()
        {
            EventBus.SubmitUp = _selectViewModel.ShowAll;
            EventBus.CancelUp = _selectViewModel.ShowAll;
        }

        private void OnShowed(bool value)
        {
            if (value)
            {
                _yesButton.gameObject.SetActive(false);
                _noButton.gameObject.SetActive(false);
                
                _selectViewModel.IsEndWrite.Changed += OnEndWriteChanged;
            }
            else
            {
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
                _selectViewModel.IsEndWrite.Changed -= OnEndWriteChanged;
            }
        }

        private void OnWrite()
        {
            _audioSource.Play();
        }

        private void OnEndWriteChanged(bool value)
        {
            EventBus.SubmitUp = null;
            EventBus.CancelUp = null;

            _yesButton.gameObject.SetActive(true);
            _noButton.gameObject.SetActive(true);

            _yesButton.onClick.AddListener(OnYesClicked);
            _noButton.onClick.AddListener(OnNoClicked);
        }

        private void OnYesClicked()
        {
            _yesButton.onClick.RemoveListener(OnYesClicked);
            _noButton.onClick.RemoveListener(OnNoClicked);
            
            _selectViewModel.OnSelectYes();
        }
        
        private void OnNoClicked()
        {
            _yesButton.onClick.RemoveListener(OnYesClicked);
            _noButton.onClick.RemoveListener(OnNoClicked);
            
            _selectViewModel.OnSelectNo();
        }

        private void OnTextChanged(string value)
        {
            _label.text = value;
        }

        private void OnNoResultStringChanged(string value)
        {
            _noLabel.text = value;
        }

        private void OnYesResultStringChanged(string value)
        {
            _yesLabel.text = value;
        }
    }
}