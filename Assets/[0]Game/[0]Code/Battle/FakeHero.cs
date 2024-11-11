using System.Collections;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public class FakeHero : EnemyBase
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private GameObject _fire;

        [SerializeField]
        private int _startHealth;

        [SerializeField]
        private MMProgressBar _progressBar;

        [SerializeField] 
        private TMP_Text _label;

        [SerializeField]
        private GameObject _damageLine;
        
        [SerializeField]
        private LocalizedString _deathMessage;
        
        private int _health;

        private void Start()
        {
            _health = _startHealth;
            
            _label.text = $"{_health}/{_startHealth}";
            _progressBar.UpdateBar(_health, 0, _startHealth);
            _progressBar.gameObject.SetActive(false);
        }

        public override IEnumerator AwaitCustomEvent(string eventName)
        {
            if (eventName == "StartBattle")
            {
                _spriteRenderer.flipX = true;
            }

            if (eventName == "Damage")
            {
                yield return new WaitForSeconds(0.5f);
                _damageLine.SetActive(true);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.DamageSound);
                //yield return new WaitForSeconds(0.5f);
                _fire.SetActive(true);
                _damageLine.SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    _spriteRenderer.color = new Color(1, 163 / 255f, 163 / 255f);
                    GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
                    yield return new WaitForSeconds(0.5f);
                    _spriteRenderer.color = Color.white;
                    yield return new WaitForSeconds(0.5f);
                }

                _fire.SetActive(false);

                yield return new WaitForSeconds(0.5f);
                
                _progressBar.gameObject.SetActive(true);

                yield return new WaitForSeconds(0.5f);

                _health -= 5;
                _label.text = $"{_health}/{_startHealth}";
                _progressBar.UpdateBar(_health, 0, _startHealth);
                
                yield return new WaitForSeconds(1);
                
                _progressBar.gameObject.SetActive(false);

                if (_health <= 0)
                {
                    var messageCommand = new MessageCommand(GameData.Battle.EnemyMessageBox, _deathMessage);
                    yield return messageCommand.Await();
                    gameObject.SetActive(false);
                }
                else
                {
                    
                }
            }
        }
    }
}