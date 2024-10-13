using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    [CreateAssetMenu(fileName = "Gide", menuName = "Data/Gide", order = 35)]
    public class GuideConfig : ScriptableObject
    {
        public Sprite Picture;
        public Sprite Icon;
        public LocalizedString Name;
        public LocalizedString Info;
    }
}