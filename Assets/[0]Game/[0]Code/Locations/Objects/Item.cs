using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class Item : UseObject
    {
        [SerializeField]
        private ItemConfig _config;

        [SerializeField]
        private UseSelect _useSelect;
        
        private void Start()
        {
            if (Lua.IsTrue($"Variable[\"{_config.Id}\"] == true")) 
                gameObject.SetActive(false);
        }

        public override void Use()
        {
            _useSelect.Use();
        }

        public void PickUp()
        {
            Lua.Run($"Variable[\"{_config.Id}\"] = true");
            gameObject.SetActive(false);
        }
    }
}