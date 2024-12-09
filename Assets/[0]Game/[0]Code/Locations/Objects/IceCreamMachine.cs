using System;
using PixelCrushers;
using PixelCrushers.DialogueSystem;

namespace Game
{
    public class IceCreamMachine : Saver, IUseObject
    {
        private Data _saveData = new();
        
        [Serializable]
        private class Data
        {
            //Темный рожок
        }
        
        public void Use()
        {
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
    }
}