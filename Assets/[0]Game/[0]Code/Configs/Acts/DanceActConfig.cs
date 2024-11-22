using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "DanceAct", menuName = "Data/Acts/Dance", order = 73)]
    public class DanceActConfig : BaseActConfig
    {
        [Header("Success")]
        public LocalizedString SuccessSystemMessage;
        public LocalizedString SuccessReaction;
        
        [Range(-100, 100)]
        public int SuccessProgress;
        
        [Header("Failed")]
        public LocalizedString FailedSystemMessage;
        public LocalizedString FailedReaction;
        
        [Range(-100, 100)]
        public int FailedProgress;
        
        public override Sprite GetIcon() => 
            GameData.AssetProvider.DanceActIcon;

        public override int GetProgress() => 
            SuccessProgress > FailedProgress ? SuccessProgress : FailedProgress;

        public override void Use()
        {
            var screen = Instantiate(GameData.AssetProvider.DanceActScreenPrefab, GameData.Battle.ActScreenContainer);
            screen.Init(this);
        }
    }
    
    public enum Arrow
    {
        Up,
        Down,
        Right,
        Left
    }
}