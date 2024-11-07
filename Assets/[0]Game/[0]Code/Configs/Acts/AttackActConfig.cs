using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AttackAct", menuName = "Data/Acts/Attack", order = 72)]
    public class AttackActConfig : BaseActConfig
    {
        public override Sprite GetIcon() => 
            GameData.AssetProvider.AttackActIcon;

        public override int GetProgress()
        {
            throw new System.NotImplementedException();
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}