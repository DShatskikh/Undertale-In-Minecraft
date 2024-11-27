using System.Collections;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Localization;
using DialogueSystemTrigger = PixelCrushers.DialogueSystem.Wrappers.DialogueSystemTrigger;

namespace Game
{
    public class BlueCow : EnemyBase
    {
        [SerializeField]
        private GameObject _cutscene;
        
        [SerializeField]
        private DamageEvent _damageEvent;

        [SerializeField]
        private DialogueSystemTrigger _dialogueSystemTrigger;

        [SerializeField]
        private Transform _follow;

        [SerializeField]
        private AudioClip _ost, _genocideOst;

        [SerializeField]
        private GameObject _trigger;

        [SerializeField]
        private SpriteRenderer _view;

        [SerializeField]
        private GameObject _bad;

        [SerializeField]
        private LocalizedString _winString;
        
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
                yield return _damageEvent.AwaitEvent(this, (int)value);
            }

            if (eventName == "EndBattle")
            {
                Lua.Run("Variable[\"BlueCowState\"] = 3");
                
                if (_damageEvent.GetHealth <= 0)
                {
                    yield return _damageEvent.AwaitDeathEvent(this, value);
                    _bad.SetActive(true);
                    GameData.MusicPlayer.Play(_genocideOst);
                    gameObject.SetActive(false);
                }
                else
                {
                    //var dialogCommand = new DialogCommand(_config.EndReplicas, null, null);
                    //yield return dialogCommand.Await();
                    
                    _dialogueSystemTrigger.OnUse();
                    
                    bool isEnd = false;
                    EventBus.CloseDialog += () => isEnd = true;
                    yield return new WaitUntil(() => isEnd);
                    
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.SpareSound);
                    var changeAlphaCommand = new ChangeAlphaCommand(_view, 0, 1);
                    yield return changeAlphaCommand.Await();

                    var monologueCommand = new MonologueCommand(_winString);
                    yield return monologueCommand.Await();
                    
                    gameObject.SetActive(false);
                }
            }
        }
        
        private IEnumerator AwaitUse()
        {
            if (Lua.IsTrue("Variable[\"BlueCowState\"] == 0"))
            {
                GameData.CharacterController.enabled = false;
                GameData.CinemachineVirtualCamera.Follow = _follow;
                GameData.MusicPlayer.Play(_ost);
                yield return new WaitForSeconds(0.25f);
                _dialogueSystemTrigger.OnUse();

                bool isEnd = false;
                EventBus.CloseDialog += () => isEnd = true;
                yield return new WaitUntil(() => isEnd);
                
                GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.View.transform;
            }
            else if (Lua.IsTrue("Variable[\"BlueCowState\"] == 2 and (IsGenocide() == false)"))
            {
                GameData.CharacterController.enabled = false;
                GameData.CinemachineVirtualCamera.Follow = _follow;
                GameData.MusicPlayer.Play(_ost);
                yield return new WaitForSeconds(0.25f);
                _dialogueSystemTrigger.OnUse();

                bool isEnd = false;
                EventBus.CloseDialog += () => isEnd = true;
                yield return new WaitUntil(() => isEnd);
                
                GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.View.transform;
                
                _bad.SetActive(true);
                _cutscene.SetActive(false);
            }
            else if (Lua.IsTrue("Variable[\"BlueCowState\"] == 2 and (IsGenocide() == true)"))
            {
                GameData.CharacterController.enabled = false;
                GameData.CinemachineVirtualCamera.Follow = _follow;
                GameData.MusicPlayer.Play(_ost);
                yield return new WaitForSeconds(0.25f);
                _dialogueSystemTrigger.OnUse();

                bool isEnd = false;
                EventBus.CloseDialog += () => isEnd = true;
                yield return new WaitUntil(() => isEnd);
                
                GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.View.transform;
                
                _trigger.SetActive(false);
                StartBattle();
            }
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize(s, _saveData);
            _saveData = data;

            if (data.IsDefeated)
            {
                gameObject.SetActive(false);   
                _bad.SetActive(true);
            }
        }
    }
}