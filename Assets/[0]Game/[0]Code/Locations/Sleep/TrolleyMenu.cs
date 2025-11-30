using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Sleep
{
    public sealed class TrolleyMenu : MonoBehaviour
    {
        [SerializeField]
        private Button[] _stopButtons;

        public void Open(Trolley trolley, StopEnum stopEnum)
        {
            GameData.Character.enabled = false;
            gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_stopButtons[(int)stopEnum].gameObject);

            for (var index = 0; index < _stopButtons.Length; index++)
            {
                var stopButton = _stopButtons[index];
                var i = index;
                stopButton.onClick.AddListener(() => trolley.StartMove(i));
            }
        }

        public void Close()
        {
            GameData.Character.enabled = true;
            
            foreach (var stopButton in _stopButtons)
            {
                stopButton.onClick.RemoveAllListeners();
            }
            
            gameObject.SetActive(false);
        }
    }
}