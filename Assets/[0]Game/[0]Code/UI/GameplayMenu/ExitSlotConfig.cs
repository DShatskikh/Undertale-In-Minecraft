using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "ExitSlot", menuName = "Data/ExitSlot", order = 51)]
    public class ExitSlotConfig : ScriptableObject
    {
        public Sprite Icon;
        public LocalizedString Name;
        public ExitSlotType ExitSlotType;
    }
}