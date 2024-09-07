using Febucci.UI;
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
        private TMP_Text _label, _yesLabel, _noLabel, _hint;
        
        [SerializeField]
        private TextAnimatorPlayer _textAnimatorPlayer;
        
        private SelectViewModel _selectViewModel;

        public void Init(SelectViewModel selectViewModel)
        {
            _selectViewModel = selectViewModel;
            _selectViewModel.YesResultString.Changed += OnYesResultStringChanged;
            _selectViewModel.NoResultString.Changed += OnNoResultStringChanged;
            _selectViewModel.Text.Changed += OnLoadingText;
            _selectViewModel.IsShowed.Changed += IsShowedOnChanged;
            _selectViewModel.Write += OnWrite;
            _selectViewModel.ShowedAll += OnShowedAllText;
            _selectViewModel.IsEndWrite.Changed += OnEndWriteChanged;
        }

        private void OnDestroy()
        {
            _selectViewModel.YesResultString.Changed -= OnYesResultStringChanged;
            _selectViewModel.NoResultString.Changed -= OnNoResultStringChanged;
            _selectViewModel.Text.Changed -= OnLoadingText;
            _selectViewModel.IsShowed.Changed -= IsShowedOnChanged;
            _selectViewModel.Write -= OnWrite;
            _selectViewModel.ShowedAll -= OnShowedAllText;
            _selectViewModel.IsEndWrite.Changed -= OnEndWriteChanged;
        }

        private void IsShowedOnChanged(bool value)
        {
            if (value)
            {
                _yesButton.gameObject.SetActive(false);
                _noButton.gameObject.SetActive(false);
                _hint.gameObject.SetActive(false);
            }
            else
            {
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
                _label.gameObject.SetActive(false);
            }
        }

        private void OnLoadingText(string value)
        {
            EventBus.SubmitUp += _selectViewModel.ShowAll;
            EventBus.CancelUp += _selectViewModel.ShowAll;
            
            _hint.gameObject.SetActive(true);
            _label.gameObject.SetActive(true);
            _label.text = value;
            _textAnimatorPlayer.StartShowingText();
        }

        private void OnWrite()
        {
            _audioSource.Play();
        }

        private void OnEndWriteChanged(bool value)
        {
            if (value)
            {
                _hint.gameObject.SetActive(false);
            
                EventBus.SubmitUp -= _selectViewModel.ShowAll;
                EventBus.CancelUp -= _selectViewModel.ShowAll;

                _yesButton.gameObject.SetActive(true);
                _noButton.gameObject.SetActive(true);

                _yesButton.onClick.AddListener(OnYesClicked);
                _noButton.onClick.AddListener(OnNoClicked);
            }
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

        private void OnNoResultStringChanged(string value)
        {
            _noLabel.text = value;
        }

        private void OnYesResultStringChanged(string value)
        {
            _yesLabel.text = value;
        }
        
        private void OnShowedAllText()
        {
            _textAnimatorPlayer.SkipTypewriter();
        }
    }
}