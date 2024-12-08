using System;
using System.Collections;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class Chest : Saver, IUseObject
    {
        [SerializeField]
        private ItemConfig _itemConfig;

        [SerializeField]
        private int _count;

        [SerializeField]
        private Sprite _open, _close;

        [SerializeField]
        private AudioClip _openSound;
        
        private Data _saveData = new();
        
        [Serializable]
        private class Data
        {
            public bool IsOpen;
        }
        
        public void Use()
        {
            print("Use");
            //;
            
            Lua.Run("Variable[\"IsChestOpen\"] = " + (_saveData.IsOpen ? "true" : "false"));
            print($"IsChestOpen: {Lua.IsTrue("Variable[\"IsChestOpen\"] == false")}");

            Lua.Run($"Variable[\"ChestItemName\"] = \"{_itemConfig.name}\";  Variable[\"ChestItemCount\"] = {_count};");
            GetComponent<DialogueSystemTrigger>().OnUse();
        }

        public override string RecordData()
        {
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize<Data>(s, _saveData);
            _saveData ??= data;
        }
        
        private void Open()
        {
            StartCoroutine(AwaitOpen());
        }

        private IEnumerator AwaitOpen()
        {
            yield return new WaitForSeconds(0.5f);
            GameData.CharacterController.enabled = false;
            GameData.EffectSoundPlayer.Play(_openSound);
            GetComponent<SpriteRenderer>().sprite = _open;
            yield return new WaitForSeconds(0.5f);
            _saveData.IsOpen = true;
            Sequencer.Message("\"OpenChest\"");
        }
        
        private void Close()
        {
            
        }
    }
}