using System;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField]
        protected EnemyConfig _config;

        public EnemyConfig GetConfig => _config;

        private void Start()
        {
            Load();
        }
        
        public virtual void StartBattle()
        {
            GameData.EnemyData = new EnemyData()
            {
                EnemyConfig = _config,
                Enemy = this,
            };

            if (GetComponent<Collider2D>())
                GetComponent<Collider2D>().enabled = false;
            
            GameData.Battle.StartBattle();
        }
        
        public abstract IEnumerator AwaitCustomEvent(string eventName, float value = 0);

        public virtual void Save()
        {
            Lua.Run($"Variable[IsWin_{_config.name}] = true");
        }

        private void Load()
        {
            if (Lua.IsTrue($"Variable[IsWin_{_config.name}] == true"))
                OnLoad();
        }

        protected virtual void OnLoad()
        {
            gameObject.SetActive(false);
        }
    }
}