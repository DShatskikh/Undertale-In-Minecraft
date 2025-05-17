using TMPro;
using UnityEngine;

namespace Game
{
    public class MoneyLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        private void OnEnable()
        {
            EventBus.OnMoneyUpgrade += Upgrade;
        }
        
        private void OnDisable()
        {
            EventBus.OnMoneyUpgrade -= Upgrade;
        }

        private void Start()
        {
            Upgrade(GameData.Saver.LoadKey(GameData.MoneyKey));
        }

        private void Upgrade(int value)
        {
            _label.text = value.ToString();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}