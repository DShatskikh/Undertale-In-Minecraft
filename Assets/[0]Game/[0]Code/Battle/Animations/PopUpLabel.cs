using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class PopUpLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        
        public IEnumerator AwaitAnimation(Vector2 startPosition, string startMessage, Color color, string message = null, AudioClip sound = null)
        {
            _label.text = startMessage;
            _label.color = color;
            _label.transform.position = startPosition;
            var duration = 0.25f;
            
            _label.gameObject.SetActive(true);
            var rectTransform = _label.GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            var progress = 0.0f;

            var startScale = new Vector2(2, 0.2f);
            _label.transform.localScale = startScale;

            while (progress < 1)
            {
                progress += Time.deltaTime / duration;
                _label.transform.localScale = Vector2.Lerp(startScale, Vector2.one, progress);
                _label.transform.position = Vector2.Lerp(startPosition, startPosition.AddX(1), progress);
                yield return null;
            }
            
            yield return new WaitForSeconds(0.25f);

            if (message != null)
            {
                _label.text = message;
            
                if (sound != null)
                    GameData.EffectSoundPlayer.Play(sound);

                duration = 1f;
                rectTransform.pivot = new Vector2(0.5f, 0);
                progress = 0;
                startPosition = _label.transform.position;

                EventBus.BattleProgressChange?.Invoke(GameData.Battle.SessionData.Progress);
            
                while (progress < 1)
                {
                    progress += Time.deltaTime / duration;
                    _label.color = _label.color.SetA(1 - progress);
                    _label.transform.localScale = Vector2.Lerp(Vector2.one, new Vector2(1, 2), progress);
                    _label.transform.position = Vector2.Lerp(startPosition, startPosition.AddY(1), progress);
                    yield return null;
                }
            }

            _label.gameObject.SetActive(false);
        } 
    }
}