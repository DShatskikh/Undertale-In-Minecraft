using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AttackAct", menuName = "Data/Acts/Attack", order = 72)]
    public class AttackActConfig : BaseActConfig
    {
        [Range(-7, 0)]
        public int Progress;
        
        public override Sprite GetIcon() => 
            GameData.AssetProvider.AttackActIcon;

        public override int GetProgress() => 
            Progress;

        public override void Use()
        {
            var screen = Instantiate(GameData.AssetProvider.AttackActScreenPrefab, GameData.Battle.ActScreenContainer);
            screen.Init(this);
        }
    }
}