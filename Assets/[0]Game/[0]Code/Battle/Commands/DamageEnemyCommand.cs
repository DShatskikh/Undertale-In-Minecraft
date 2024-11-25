using System.Collections;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Game
{
    public class DamageEnemyCommand : AwaitCommand
    {
        private readonly MMProgressBar _progressBar;
        private readonly int _startHealth;
        private readonly int _health;
        private readonly TMP_Text _label;
        private readonly int _damage;

        public DamageEnemyCommand(MMProgressBar progressBar, int startHealth, int health, int damage, TMP_Text label)
        {
            _progressBar = progressBar;
            _startHealth = startHealth;
            _health = health;
            _label = label;
            _damage = damage;
        }

        public override void Execute(UnityAction action)
        {
            GameData.CoroutineRunner.StartCoroutine(AwaitExecute(action));
        }

        protected override IEnumerator AwaitExecute(UnityAction action)
        {
            _progressBar.gameObject.SetActive(true);

            _label.text = $"{_health}/{_startHealth}";
            _progressBar.UpdateBar(_health, 0, _startHealth);
            
            yield return new WaitForSeconds(0.5f);

            var popUpLabel = GameData.Battle.AddProgressLabel;
            yield return popUpLabel.AwaitAnimation(GameData.EnemyData.Enemy.transform.position.AddY(1), $"-{_damage}", Color.red, $"-{_damage}", GameData.AssetProvider.HurtSound);
            
            _progressBar.gameObject.SetActive(false);

            action?.Invoke();
        }
    }
}