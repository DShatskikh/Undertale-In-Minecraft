using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public interface IEnemy
    {
        BaseActConfig[] GetActs { get; }
        EnemyConfig GetConfig { get; }
        void Init(IBattleController battleController);
        IEnumerator AwaitEndIntro();
        IEnumerator AwaitReaction(string actName, float value);
        Sprite GetSprite();
        LocalizedString GetName();
    }
}