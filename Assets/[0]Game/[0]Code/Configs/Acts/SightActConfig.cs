using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SightAct", menuName = "Data/Acts/Sight", order = 74)]
    public class SightActConfig : BaseActConfig
    {
        [Range(-7, 7)]
        public int SuccessProgress;
        
        [Range(-7, 7)]
        public int FailedProgress;
        
        public override Sprite GetIcon() =>
            GameData.AssetProvider.SightActIcon;

        public override int GetProgress() => 
            SuccessProgress > FailedProgress ? SuccessProgress : FailedProgress;

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}