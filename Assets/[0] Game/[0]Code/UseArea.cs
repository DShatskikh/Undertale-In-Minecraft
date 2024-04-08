using System;
using UnityEngine;

namespace Game
{
    public class UseArea : MonoBehaviour
    {
        private void Start()
        {
            GameData.UseButton.onClick.AddListener(() => GameData.UseButton.gameObject.SetActive(false));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out UseObject useObject))
            {
                GameData.UseButton.gameObject.SetActive(true);
                GameData.UseButton.onClick.AddListener(useObject.Use);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out UseObject useObject))
            {
                GameData.UseButton.onClick.RemoveListener(useObject.Use);
                
                if (GameData.UseButton)
                    GameData.UseButton.gameObject.SetActive(false);
            }
        }
    }
}