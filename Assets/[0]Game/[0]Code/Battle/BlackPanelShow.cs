using System;
using UnityEngine;

namespace Game
{
    public class BlackPanelShow : MonoBehaviour
    {
        private void Start()
        {
            GameData.Battle.BlackPanel.Show();
        }
    }
}