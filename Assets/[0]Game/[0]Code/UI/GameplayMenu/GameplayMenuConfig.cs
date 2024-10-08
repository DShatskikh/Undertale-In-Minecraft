using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "GameplayMenu", menuName = "Data/GameplayMenu", order = 50)]
    public class GameplayMenuConfig : ScriptableObject
    {
        public GameplayMenuSlotType SlotType;
        public LocalizedString Name;
        public Sprite Icon;
    }
}