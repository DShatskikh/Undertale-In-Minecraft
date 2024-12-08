using System;
using UnityEngine;

namespace Game
{
    public class BattleTestStarter : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour _battleController;
        
        private void Start()
        {
            ((IBattleController)_battleController).StartBattle();
        }
    }
}