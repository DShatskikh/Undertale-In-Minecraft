using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SaveKeyString", menuName = "Data/SaveKeyString", order = 37)]
    public class SaveKeyString : ScriptableObject
    {
        public string DefaultValue;
    }
}