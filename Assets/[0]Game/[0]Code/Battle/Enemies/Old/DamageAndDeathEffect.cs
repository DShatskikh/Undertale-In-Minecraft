using System.Collections;
using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

namespace Game
{
    public class DamageAndDeathEffect : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private GameObject _fire;

        [SerializeField]
        private MMProgressBar _progressBar;

        [SerializeField] 
        private TMP_Text _label;
        
        [SerializeField]
        private GameObject _explosion;

        private IDamageAndDeath _enemy;

        public void Init(IDamageAndDeath enemy)
        {
            _enemy = enemy;
            _label.text = $"{enemy.Health}/{enemy.MaxHealth}";
            _progressBar.SetBar(enemy.Health, 0, enemy.MaxHealth);
        }

        public IEnumerator AwaitPlayDamage(int damage)
        {
            print($"Damage: {damage}");
            
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.DamageSound);
            GameData.CharacterController.View.ShowLine(transform.position.AddY(0.5f));
            yield return new WaitForSeconds(0.25f);
            GameData.CharacterController.View.HideLine();
            _fire.SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                _spriteRenderer.color = new Color(1, 163 / 255f, 163 / 255f);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
                yield return new WaitForSeconds(0.25f);
                _spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.25f);
            }

            _fire.SetActive(false);

            var damageCommand = new DamageEnemyCommand(_progressBar, _enemy.MaxHealth, _enemy.Health, damage, _label);
            yield return damageCommand.Await();
        }

        public IEnumerator AwaitPlayDeath()
        {
            var shake = DOTween.Sequence();
            shake.Append(transform.DOShakePosition(1, 0.5f, 10));
            yield return shake.WaitForCompletion();
            
            _explosion.SetActive(true);
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            _explosion.SetActive(false);
        }
    }
}