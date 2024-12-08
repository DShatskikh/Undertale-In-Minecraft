using System.Collections;
using UnityEngine;

namespace Game
{
    public interface IBattle
    {
        IBattle Init();
        IBattle SetIntro(IBattleIntro intro);
        IBattle SetOutro(IBattleOutro outro);
        IBattle SetBattleTheme(AudioClip theme);
        void StartBattle(IBattleController battleController);
        IEnumerator AwaitEndBattle();
        void PlayerTurn();
        IEnemy SelectEnemy { get; set; }
    }
}