using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SaveKeyBool", menuName = "Data/SaveKeyBool", order = 35)]
    public class SaveKeyBool : ScriptableObject
    {
        public bool DefaultValue;
    }
}