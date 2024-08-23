using TMPro;
using UnityEngine;

namespace Game
{
    public class BattleMessageBoxView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Shake _shake;

        public void SetShake(float shaking)
        {
            _shake.enabled = shaking > 0f;
            _shake.SetShake(shaking);
        }

        public void SetText(string text)
        {
            _label.text = text;
        }
    }
}