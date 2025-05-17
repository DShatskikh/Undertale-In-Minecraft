using System;
using UnityEngine;

namespace Game
{
    public class ShowMoneyTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            GameData.MoneyLabel.Show();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            GameData.MoneyLabel.Hide();
        }
    }
}