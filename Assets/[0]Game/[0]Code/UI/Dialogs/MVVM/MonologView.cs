using RimuruDev;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public class MonologView : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField]
        private UIDocument _ui;
        
        private Label _label;
        private Button _continueButton;
        private MonologViewModel _viewModel;

        public void Init(MonologViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Showed += OnShowed;
            _viewModel.Closed += OnClosed;
            _viewModel.Write += OnWrite;
            _viewModel.Text.Changed += TextOnChanged;
            _viewModel.IsEndWrite.Changed += IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed += ContinueTextOnChanged;
        }

        private void OnDestroy()
        {
            _viewModel.Showed -= OnShowed;
            _viewModel.Closed -= OnClosed;
            _viewModel.Write -= OnWrite;
            _viewModel.Text.Changed -= TextOnChanged;
            _viewModel.IsEndWrite.Changed -= IsEndWriteOnChanged;
            _viewModel.ContinueText.Changed -= ContinueTextOnChanged;
        }

        private void OnShowed()
        {
            _label = _ui.rootVisualElement.Q<Label>("Label");
            _continueButton = _ui.rootVisualElement.Q<Button>("Next_button");
            
            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
                _ui.rootVisualElement.Q<Label>("Z").text = "";
            
            _continueButton.clicked += _viewModel.Next;
            EventBus.Submit = _viewModel.Next;
            EventBus.Cancel = _viewModel.ShowFinallyText;
        }

        private void OnClosed()
        {
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.ClickSound);
        }

        private void OnWrite()
        {
            _audioSource.Play();
        }

        private void ContinueTextOnChanged(string value)
        {
            _continueButton.text = value;
        }

        private void TextOnChanged(string value)
        {
            _label.text = value;
        }

        private void IsEndWriteOnChanged(bool value)
        {
            
        }
    }
}