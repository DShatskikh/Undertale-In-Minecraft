using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "DanceAct", menuName = "Data/Acts/Dance", order = 73)]
    public class DanceActConfig : BaseActConfig
    {
        public override Sprite GetIcon() => 
            GameData.AssetProvider.DanceActIcon;

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