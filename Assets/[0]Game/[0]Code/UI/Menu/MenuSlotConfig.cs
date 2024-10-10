using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "MenuSlot", menuName = "Data/MenuSlot", order = 52)]
    public class MenuSlotConfig : ScriptableObject
    {
        public LocalizedString Name;
        public Sprite Icon;
        public MenuSlotType MenuSlotType;
    }
}