using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "SpeakAct", menuName = "Data/Acts/Speak", order = 71)]
    public class SpeakActConfig : BaseActConfig
    {
        [Range(-7, 7)]
        public int SuccessProgress;
        
        [Range(-7, 7)]
        public int FailedProgress;

        public Sprite[] ImageElements;
     
        public LocalizedString SuccessReaction;
        public LocalizedString FailedReaction;
        
        public override Sprite GetIcon() =>
            GameData.AssetProvider.SpeakActIcon;
        
        public override int GetProgress() => 
            SuccessProgress;

        public override void Use()
        {
            var screen = Instantiate(GameData.AssetProvider.SpeakActScreenPrefab, GameData.Battle.ActScreenContainer);
            screen.Init(this);
        }
    }
}