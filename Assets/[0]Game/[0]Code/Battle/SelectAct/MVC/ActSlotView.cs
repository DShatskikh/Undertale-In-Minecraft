using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game
{
    public class ActSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon, _frame;

        [SerializeField]
        private TMP_Text _label;

        private Vector3 _startPosition;
        private Vector3 _start;
        private Vector3 _end;
        private float _progress = 1;
        
        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (_progress < 1f)
            {
                _progress += Time.deltaTime / 6;
                transform.position = Vector2.Lerp(_start, _end, _progress);
            }
            else
            {
                _start = transform.position;
                var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                _end = _startPosition.AddX(direction.x * 2).AddY(direction.y);
                _progress = 0;
            }
        }

        public void UpdateView(ActSlotModel model)
        {
            _label.text = "???";
            GameData.Startup.StartCoroutine(AwaitTextUpgrade(model.Act.Name));
            
            if (model.IsSelected)
            {
                _icon.sprite = GameData.AssetProvider.CharacterIcon;
                _frame.color = GameData.AssetProvider.SelectColor;
                _label.color = GameData.AssetProvider.SelectColor;
            }
            else
            {
                _icon.sprite = model.Act.GetIcon();
                _frame.color = GameData.AssetProvider.DeselectColor;
                _label.color = GameData.AssetProvider.DeselectColor;
            }
        }

        private IEnumerator AwaitTextUpgrade(LocalizedString text)
        {
            gameObject.SetActive(false);
            var operation = text.GetLocalizedStringAsync();
            
            while (!operation.IsDone)
                yield return null;

            _label.text = operation.Result;
            gameObject.SetActive(true);
        }
    }
}