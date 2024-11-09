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
            throw new System.NotImplementedException();
        }
    }
}