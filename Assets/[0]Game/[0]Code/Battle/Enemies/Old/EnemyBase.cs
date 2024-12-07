using System;
using System.Collections;
using PixelCrushers;
using UnityEngine;

namespace Game
{
    public abstract class EnemyBase : Saver
    {
        [SerializeField]
        protected EnemyConfig _config;

        protected Data _saveData = new();

        public EnemyConfig GetConfig => _config;

        [Serializable]
        public class Data
        {
            public bool IsDefeated;
        }

        public override void OnEnable() { }
        public override void OnDisable() { }

        public override void Start()
        {
            base.Start();
            SaveSystem.RegisterSaver(this);
        }

        public override void OnDestroy()
        {
            SaveSystem.UnregisterSaver(this);
        }
        
        public virtual void StartBattle()
        {
            if (GetComponent<Collider2D>())
                GetComponent<Collider2D>().enabled = false;
            
            //GameData.Battle.StartBattle();
        }

        public abstract IEnumerator AwaitCustomEvent(string eventName, float value = 0);

        public void Defeat() => 
            _saveData.IsDefeated = true;

        public override string RecordData()
        {
            print($"RecordData {gameObject.name}: {_saveData.IsDefeated}");
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData ??= data;
            
            if (_saveData.IsDefeated)
                gameObject.SetActive(false);
        }
    }
}