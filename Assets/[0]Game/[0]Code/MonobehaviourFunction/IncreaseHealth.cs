using PixelCrushers.DialogueSystem;
using UnityEngine;
using YG;

namespace Game
{
    public class IncreaseHealth : MonoBehaviour
    {
        [SerializeField]
        private int _value = 2;
        
        public void Increase()
        {
            Lua.Run($"Variable[\"MaxHealth\"] += {_value}");
            GameData.HeartController.Health = Lua.Run("return Variable[\"MaxHealth\"]").AsInt;
        }
    }
}