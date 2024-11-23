using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "AttackAct", menuName = "Data/Acts/Attack", order = 72)]
    public class AttackActConfig : BaseActConfig
    {
        public LocalizedString Reaction;
        public Replica[] DeathMessage;

        public override Sprite GetIcon() => 
            GameData.AssetProvider.AttackActIcon;

        public override int GetProgress() =>
            -100;

        public override void Use()
        {
            var screen = Instantiate(GameData.AssetProvider.AttackActScreenPrefab, GameData.Battle.ActScreenContainer);
            screen.Init(this);
        }
    }
}