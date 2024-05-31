using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SaveKeyInt", menuName = "Data/SaveKeyInt", order = 36)]
    public class SaveKeyInt : ScriptableObject
    {
        public int DefaultValue;
    }
}