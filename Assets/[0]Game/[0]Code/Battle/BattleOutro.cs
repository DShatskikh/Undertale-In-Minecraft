using System.Collections;
using System.Linq;
using MoreMountains.Feedbacks;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class BattleOutro : MonoBehaviour
    {
        [SerializeField]
        private GameObject _battle;

        [SerializeField]
        private MMF_Player _feedback;

        private void OnEnable()
        {
            Lua.RegisterFunction("CloseBattle", this, SymbolExtensions.GetMethodInfo(() => StartOutro()));
        }

        private void OnDisable()
        {
            Lua.UnregisterFunction("CloseBattle");
        }
        
        public void StartOutro()
        {
            GameData.MaxHealth += GameData.EnemyData.EnemyConfig.WinPrize;
            EventBus.OnPlayerWin.Invoke(GameData.EnemyData.EnemyConfig);
            EventBus.OnPlayerWin = null;

            StartCoroutine(AwaitOutro());
        }
        
        public IEnumerator AwaitOutro()
        {
            yield return _feedback.PlayFeedbacksCoroutine(Vector3.zero);
            GameData.Character.enabled = true;
        }
        
        public void ChangeLocation()
        {
            GameData.Character.gameObject.SetActive(true);
            GameData.Heart.gameObject.SetActive(false);
            _battle.SetActive(false);
            GameData.Locations.ToArray()[GameData.LocationIndex].gameObject.SetActive(true);
        }
    }
}