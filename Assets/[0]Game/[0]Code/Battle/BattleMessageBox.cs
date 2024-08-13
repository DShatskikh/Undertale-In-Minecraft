using TMPro;
using UnityEngine;

namespace Game
{
    public class BattleMessageBox : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Shake _shake;
        
        public void Show(string message, float shaking)
        { 
            gameObject.SetActive(true);
            _label.text = message;
            _shake.enabled = shaking > 0f;
            _shake.SetShake(shaking);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}