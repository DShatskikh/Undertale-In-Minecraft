using TMPro;
using UnityEngine;

namespace Game
{
    public class PlatformText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _text.text = GameData.DeviceType.ToString();
        }
    }
}