using RimuruDev;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class SelectView : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField]
        private UIDocument _ui;

        private SelectViewModel _selectViewModel;
        private Button _yesButton;
        private Button _noButton;
        private Label _label;

        public void Init(SelectViewModel selectViewModel)
        {
            _selectViewModel = selectViewModel;
            _selectViewModel.YesResultString.Changed += OnYesResultStringChanged;
            _selectViewModel.NoResultString.Changed += OnNoResultStringChanged;
            _selectViewModel.Text.Changed += OnTextChanged;
            _selectViewModel.Showed += OnShowed;
            _selectViewModel.Closed += OnClosed;
            _selectViewModel.Write += OnWrite;
        }

        private void OnDestroy()
        {
            _selectViewModel.YesResultString.Changed -= OnYesResultStringChanged;
            _selectViewModel.NoResultString.Changed -= OnNoResultStringChanged;
            _selectViewModel.Text.Changed -= OnTextChanged;
            _selectViewModel.Showed -= OnShowed;
            _selectViewModel.Closed -= OnClosed;
            _selectViewModel.Write -= OnWrite;
        }

        private void OnWrite()
        {
            _audioSource.Play();
        }

        private void OnShowed()
        {
            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
            {
                _ui.rootVisualElement.Q<Label>("Z").text = "";
                _ui.rootVisualElement.Q<Label>("X").text = "";
            }
            
            _yesButton = _ui.rootVisualElement.Q<Button>("Yes_button");
            _noButton = _ui.rootVisualElement.Q<Button>("No_button");
            _label = _ui.rootVisualElement.Q<Label>("Label");
            _noButton = _ui.rootVisualElement.Q<Button>("No_button");
            _yesButton = _ui.rootVisualElement.Q<Button>("Yes_button");

            _yesButton.visible = false;
            _noButton.visible = false;

            EventBus.Submit = _selectViewModel.ShowAll;
            _yesButton.clicked += _selectViewModel.ShowAll;
            
            EventBus.Cancel = _selectViewModel.ShowAll;
            _noButton.clicked += _selectViewModel.ShowAll;
            
            _selectViewModel.IsEndWrite.Changed += OnEndWriteChanged;
        }

        private void OnEndWriteChanged(bool value)
        {
            _yesButton.visible = true;
            _noButton.visible = true;
            
            _yesButton.text = _selectViewModel.YesResultString.Value;
            _noButton.text = _selectViewModel.NoResultString.Value;
            
            EventBus.Submit = _selectViewModel.OnSelectTrue;
            _yesButton.clicked -= _selectViewModel.ShowAll;
            _yesButton.clicked += _selectViewModel.OnSelectTrue;
            
            EventBus.Cancel = _selectViewModel.OnSelectFalse;
            _noButton.clicked -= _selectViewModel.ShowAll;
            _noButton.clicked += _selectViewModel.OnSelectFalse;
        }

        private void OnTextChanged(string value)
        {
            _label.text = value;
        }

        private void OnClosed()
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
            _selectViewModel.IsEndWrite.Changed -= OnEndWriteChanged;
        }

        private void OnNoResultStringChanged(string value)
        {
            _noButton.text = value;
        }

        private void OnYesResultStringChanged(string value)
        {
            _yesButton.text = value;
        }
    }
}