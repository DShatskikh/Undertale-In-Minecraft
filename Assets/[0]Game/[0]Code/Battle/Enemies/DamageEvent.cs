using System.Collections;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;

namespace Game
{
    public class DamageEvent : MonoBehaviour
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
        private GameObject _explosion;

        [SerializeField]
        private MMF_Player _shake;
        
        private int _health;

        public int GetHealth => _health;
        
        private void Start()
        {
            _health = _startHealth;
            
            _label.text = $"{_health}/{_startHealth}";
            _progressBar.UpdateBar(_health, 0, _startHealth);
            _progressBar.gameObject.SetActive(false);
        }
        
        public IEnumerator AwaitEvent(EnemyConfig config, float value = 0)
        {
            GameData.CharacterController.View.ShowLine(transform.position.AddY(0.5f));
            GameData.EffectSoundPlayer.Play(GameData.AssetProvider.DamageSound);
            yield return new WaitForSeconds(0.5f);
            _fire.SetActive(true);
            GameData.CharacterController.View.HideLine();

            for (int i = 0; i < 4; i++)
            {
                _spriteRenderer.color = new Color(1, 163 / 255f, 163 / 255f);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
                yield return new WaitForSeconds(0.25f);
                _spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(0.25f);
            }

            _fire.SetActive(false);

            yield return new WaitForSeconds(0.25f);

            _progressBar.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            _health -= (int)value + 100;
            _label.text = $"{_health}/{_startHealth}";
            _progressBar.UpdateBar(_health, 0, _startHealth);

            yield return new WaitForSeconds(1);

            _progressBar.gameObject.SetActive(false);

            AttackActConfig attackConfig = null;

            foreach (var act in config.Acts)
            {
                if (act is AttackActConfig attackActConfig)
                {
                    attackConfig = attackActConfig;
                    break;
                }
            }

            if (_health > 0)
            {
                var messageCommand = new MessageCommand(GameData.Battle.EnemyMessageBox, attackConfig.Reaction);
                yield return messageCommand.Await();
                var addProgressCommand = new AddProgressCommand(-100, GameData.Battle.AddProgressLabel,
                    GameData.Battle.AddProgressData);
                addProgressCommand.Execute(null);
                yield return new WaitForSeconds(1f);
                var startTurn = new StartEnemyTurnCommand();
                startTurn.Execute(null);
            }
            else
            {
                _shake.PlayFeedbacks();

                var messageCommand = new MessageCommand(GameData.Battle.EnemyMessageBox, attackConfig.DeathMessage);
                yield return messageCommand.Await();

                _shake.StopFeedbacks();

                _explosion.SetActive(true);
                GameData.EffectSoundPlayer.Play(GameData.AssetProvider.BombSound);
                yield return new WaitForSeconds(0.3f);
                _spriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.2f);
                _explosion.SetActive(false);
                gameObject.SetActive(false);
                _spriteRenderer.enabled = true;

                GameData.CommandManager.StopExecute();
                GameData.Battle.EndBattle();
                EventBus.Kill?.Invoke();

                var kills = Lua.Run("Variable[KILLS]").AsInt;
                Lua.Run($"Variable[KILLS] = {kills}");
            }
        }
    }
}