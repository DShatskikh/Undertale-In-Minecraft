using System.Collections;
using UnityEngine.Localization;

namespace Game
{
    public interface IBattleController
    {
        string GetIndex { get; }
        IEnemy[] GetEnemies();
        void StartBattle();
        void Turn();
        IEnumerator AwaitEndIntro();
        IEnumerator AwaitActReaction(string actName, float value);
        IEnumerator AwaitDeadReaction(string enemyName);
        BattleArena GetArena();
        LocalizedString GetProgressName();
        int GetDamage();
    }
}