using System;
using System.Collections;
using Cinemachine;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class BedTeleport : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private CinemachineVirtualCamera _cinemachineVirtual;

        [SerializeField]
        private BadGameEnd _end;

        public void Use()
        {
            GameData.CoroutineRunner.StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            GameData.MusicPlayer.Stop();

            var characterTransform = GameData.CharacterController.transform;
            GameData.CharacterController.enabled = false;

            GameData.CharacterController.View.Flip(true);
            _spriteRenderer.GetComponent<BoxCollider2D>().enabled = false;
            
            _spriteRenderer.sortingOrder = -1;
            
            var isFakeHero = Lua.IsTrue("IsHaveCompanion(\"FakeHero\") == true");
            GameData.CompanionsManager.DeactivateAllCompanion();
            
            var characterJumpMoveToUpCommand = new MoveToPointCommand(characterTransform, characterTransform.position.AddY(1), 0.25f);
            yield return characterJumpMoveToUpCommand.Await();

            var rotationCommand = new RotationCommand(characterTransform, new Vector3(0, 0, -90), 0.5f);
            yield return rotationCommand.Await();
            
            var characterJumpToBedCommand = new MoveToPointCommand(characterTransform, transform.position.AddY(0.75f).AddX(-0.5f), 0.5f);
            yield return characterJumpToBedCommand.Await();

            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.JumpSound);

            _cinemachineVirtual.gameObject.SetActive(true);
            float size = 8;
            
            do
            {
                size -= 4 * Time.deltaTime;
                _cinemachineVirtual.m_Lens.OrthographicSize = size;
                yield return null;
            } while (size > 0.5f);

            yield return new WaitForSeconds(2);

            GameData.CharacterController.HatPoint.FreakShow(false);
            GameData.CharacterController.View.Sleep();

            yield return new WaitForSeconds(2);

            if (Lua.IsTrue("IsGenocide() == true"))
            {
                characterTransform.eulerAngles = Vector3.zero;
                GameData.CharacterController.View.Reset();
                GameData.CharacterController.enabled = true;
                GameData.LocationsManager.SwitchLocation("FakeHerobrineHome", 1);   
            }
            else
                _end.Use(isFakeHero);
        }
    }
}