using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using DialogueSystemTrigger = PixelCrushers.DialogueSystem.Wrappers.DialogueSystemTrigger;

namespace Game
{
    public class BlueCow : EnemyBase
    {
        [SerializeField]
        private DamageEvent _damageEvent;

        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;

        [SerializeField]
        private Transform _follow;

        [SerializeField]
        private StartBattleTrigger _startBattleTrigger;

        [SerializeField]
        private AudioClip _ost;

        [SerializeField]
        private GameObject _trigger;

        public void StartSpeck()
        {
            StartCoroutine(AwaitUse());
        }

        public override IEnumerator AwaitCustomEvent(string eventName, float value = 0)
        {
            if (eventName == "StartBattle")
            {
                
            }
            
            if (eventName == "Damage")
            {
                yield return _damageEvent.AwaitEvent(_config, value);
                
                if (_damageEvent.GetHealth <= 0)
                    gameObject.SetActive(false);
            }

            if (eventName == "EndBattle")
            {
                Lua.Run("Variable[\"BlueCowState\"] = 2");
                yield return _damageEvent.AwaitDeathEvent(_config, value);
            }
        }
        
        private IEnumerator AwaitUse()
        {
            if (Lua.IsTrue("Variable[\"BlueCowState\"] == 0"))
            {
                _dialogueSystemTrigger.OnUse();
                GameData.CinemachineVirtualCamera.Follow = _follow;
                GameData.MusicPlayer.Play(_ost);
                
                bool isEnd = false;
                EventBus.CloseDialog += () => isEnd = true;
                yield return new WaitUntil(() => isEnd);
                
                GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.View.transform;
            }
            else if (Lua.IsTrue("Variable[\"BlueCowState\"] == 1 and (IsHaveCompanion(\"FakeHero\") == true)"))
            {
                GameData.CinemachineVirtualCamera.Follow = _follow;
                GameData.MusicPlayer.Play(_ost);
                _dialogueSystemTrigger.OnUse();

                bool isEnd = false;
                EventBus.CloseDialog += () => isEnd = true;
                yield return new WaitUntil(() => isEnd);
                
                GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.View.transform;
                
                _trigger.SetActive(false);
                _startBattleTrigger.StartBattle();
            }
            else
            {
                
            }
        }
    }
}