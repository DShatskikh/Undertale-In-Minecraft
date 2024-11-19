using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "SpeakAct", menuName = "Data/Acts/Speak", order = 71)]
    public class SpeakActConfig : BaseActConfig
    {
        [Range(-30, 30)]
        public int SuccessProgress;
        
        [Range(-30, 30)]
        public int FailedProgress;

        public Sprite[] ImageElements;
     
        public LocalizedString SuccessReaction;
        public LocalizedString SuccessSystemMessage;
        public LocalizedString FailedReaction;
        public LocalizedString FailedSystemMessage;
        
        public override Sprite GetIcon() =>
            GameData.AssetProvider.SpeakActIcon;
        
        public override int GetProgress() => 
            SuccessProgress > FailedProgress ? SuccessProgress : FailedProgress;

        public override void Use()
        {
            var screen = Instantiate(GameData.AssetProvider.SpeakActScreenPrefab, GameData.Battle.ActScreenContainer);
            screen.Init(this);
        }
    }
}