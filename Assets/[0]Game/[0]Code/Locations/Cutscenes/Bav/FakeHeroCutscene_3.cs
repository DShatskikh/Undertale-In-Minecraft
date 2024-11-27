using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace Game
{
    public class FakeHeroCutscene_3 : BaseCutscene
    {
        [SerializeField]
        private EnderPost[] _posts;

        [SerializeField]
        private FakeHeroCutscene_4 _nextCutscene;
        
        [SerializeField]
        private Transform _chickenStartPoint;

        [SerializeField]
        private Animator _fakeHero;

        [SerializeField]
        private PlayableDirector _playableDirector;

        [SerializeField]
        private Transform _chicken;

        [SerializeField]
        private GameObject _dragon;

        [SerializeField]
        private AudioClip _dragonSound;

        [SerializeField]
        private AudioClip _bossTheme;
        
        [Header("Genocide Root")]
        [SerializeField]
        private DamageEvent _fakeHeroEnemy;
        
        [SerializeField]
        private Dragon _dragonEnemy;
        
        protected override IEnumerator AwaitCutscene()
        {
            GameData.CharacterController.enabled = false;
            _chicken.gameObject.SetActive(false);
            GameData.MusicPlayer.Play(_bossTheme);

            foreach (var post in _posts)
            {
                post.ActivateCrystal();
            }

            if (SaveLoadManager.GetData<DamageEvent.Data>("FakeHero_Dead").IsDead)
            {
                //GameData.CharacterController.enabled = true;
                yield return new WaitForSeconds(0.5f);
                _dragonEnemy.StartBattle();
                //_nextCutscene.StartCutscene();
                yield break;
            }
            
            var moveCharacterToPointCommand = new MoveToPointCommand(GameData.CharacterController.transform, _chickenStartPoint.position, 1);
            yield return moveCharacterToPointCommand.Await();

            var moveFakeHeroToPointCommand = new MoveToPointCommand(_fakeHero.transform, _fakeHero.transform.position, 0.5f);
            GameData.CompanionsManager.TryDeactivateCompanion("FakeHero");
            _fakeHero.gameObject.SetActive(true);
            _fakeHero.SetFloat("Speed", 1);
            _fakeHero.transform.position = GameData.CompanionsManager.GetCompanion("FakeHero").transform.position;
            
            yield return moveFakeHeroToPointCommand.Await();
            
            _fakeHero.SetFloat("Speed", 0);
            _fakeHero.gameObject.SetActive(false);
            GameData.CharacterController.gameObject.SetActive(false);
            
            _playableDirector.Play();
            var isEndTimeline = false;
            _playableDirector.stopped += (_) => isEndTimeline = true;

            yield return new WaitUntil(() => isEndTimeline);

            GameData.EffectSoundPlayer.Play(_dragonSound);
            
            GameData.CharacterController.transform.position = _chicken.position;
            _chicken.gameObject.SetActive(false);
            GameData.CharacterController.gameObject.SetActive(true);
            GameData.CinemachineVirtualCamera.Follow = GameData.CharacterController.transform;
            
            _dragon.SetActive(false);
            
            yield return AwaitDialog();
            GameData.CharacterController.enabled = false;
            
            _nextCutscene.StartCutscene();
            GameData.CharacterController.enabled = true;

            Lua.Run("Variable[\"FakeHeroState\"] = 3");
        }
    }
}