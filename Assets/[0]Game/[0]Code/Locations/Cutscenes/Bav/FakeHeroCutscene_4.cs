using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class FakeHeroCutscene_4 : BaseCutscene
    {
        [SerializeField]
        private EnderPost[] _posts;
        
        [SerializeField]
        private Transform _cameraTarget;
        
        [SerializeField]
        private Transform _cameraFinishTarget;

        [SerializeField]
        private GameObject _trigger;

        [SerializeField]
        private FakeHeroRunningCircle _fakeHeroRunningCircle;

        [FormerlySerializedAs("_fakeHeroDamage")]
        [Header("Genocide Root")]
        [SerializeField]
        private DamageAndDeathEffect fakeHeroDamageAndDeath;

        [SerializeField]
        private Dragon _dragon;
        
        protected override IEnumerator AwaitCutscene()
        {
            //if (!SaveLoadManager.GetData<DamageAndDeathEffect.Data>("FakeHero_Dead").IsDead)
            //    _fakeHeroRunningCircle.gameObject.SetActive(true);
            
            var isAllCrystalDeactivate = true;
            
            while (isAllCrystalDeactivate)
            {
                isAllCrystalDeactivate = false;
                
                foreach (var post in _posts)
                {
                    if (post.IsActiveCrystal)
                    {
                        isAllCrystalDeactivate = true;
                        break;
                    }
                } 
                
                yield return new WaitForSeconds(1);
            }

            /*if (SaveLoadManager.GetData<DamageAndDeathEffect.Data>("FakeHero_Dead").IsDead)
            {
                //_dragon.StartBattle();
                yield break;
            }*/
            
            GameData.CharacterController.enabled = false;

            _cameraTarget.transform.position = _cameraTarget.transform.position.SetY(GameData.CinemachineVirtualCamera.transform.position.y);
            GameData.CinemachineVirtualCamera.Follow = _cameraTarget;

            var cameraMoveToPointCommand = new MoveToPointCommand(_cameraTarget.transform,
                _cameraTarget.transform.position.SetY(_cameraFinishTarget.position.y), 1);
            yield return cameraMoveToPointCommand.Await();
            
            yield return new WaitForSeconds(1f);
            
            yield return AwaitDialog();
            GameData.CharacterController.enabled = false;
            
            yield return new WaitForSeconds(0.5f);
            
            var cameraMoveToCharacterCommand = new MoveToPointCommand(_cameraTarget.transform, GameData.CharacterController.transform.position, 1);
            yield return cameraMoveToCharacterCommand.Await();
            
            _trigger.SetActive(true);
            
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;
            GameData.CharacterController.enabled = true;
            
            Lua.Run("Variable[\"FakeHeroState\"] = 4");
        }
    }
}