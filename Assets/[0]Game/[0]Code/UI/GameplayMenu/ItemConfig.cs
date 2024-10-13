using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Data/Items/ItemConfig", order = 53)]
    public class ItemConfig : ScriptableObject
    {
        public string Id;
        public Sprite Icon;
        public LocalizedString Name;
        public LocalizedString Description;
    }
}