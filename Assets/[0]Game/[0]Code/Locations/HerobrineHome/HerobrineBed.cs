using System.Collections;
using UnityEngine;
using UnityEngine.Localization;

namespace Game
{
    public sealed class HerobrineBed : UseObject
    {
        [SerializeField]
        private LocalizedString[] _replica_normal;
        
        [SerializeField]
        private LocalizedString[] _replica_noneItem;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private Sprite _bedNoneItem;

        [SerializeField]
        private ExitLocation _exitLocation;
        
        [SerializeField]
        private LocalizedString _select_noneItem; // Лечь спать?
        
        private void Start()
        {
            if (YG.YandexGame.savesData.IsNoneItem)
            {
                _spriteRenderer.sprite = _bedNoneItem;
            }
        }

        public override void Use()
        {
            if (YG.YandexGame.savesData.IsNoneItem)
            {
                StartCoroutine(Await());
            }
            else
            {
                GameData.Monolog.Show(_replica_normal);
            }
        }

        private IEnumerator Await()
        {
            GameData.Monolog.Show(_replica_noneItem);
            yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
            GameData.Select.Show(_select_noneItem, Sleep, () => { });
        }

        private void Sleep()
        {
            StartCoroutine(AwaitSleep());
        }
        
        private IEnumerator AwaitSleep()
        {
            GameData.Character.enabled = false;
            yield return null;
            GameData.Character.enabled = true;
            _exitLocation.Exit();
        }
    }
}