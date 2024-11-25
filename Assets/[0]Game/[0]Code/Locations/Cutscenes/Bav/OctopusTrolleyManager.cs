using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class OctopusTrolleyManager : MonoBehaviour
    {
        [SerializeField]
        private Collider2D _collider2D;

        [SerializeField]
        private Transform _octopus, _endPosition, _middlePosition;

        [SerializeField]
        private GameObject _trigger;

        [SerializeField]
        private UseSelect _useSelect;

        [SerializeField]
        private UseMonolog _useMonolog;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        private void Start()
        {
            var state = Lua.Run("return Variable[\"BavWorldStopState\"]").AsInt;

            switch (state)
            {
                case 1:
                    _octopus.transform.position = _middlePosition.position;
                    _trigger.SetActive(false);
                    _useSelect.gameObject.SetActive(true);
                    _useMonolog.gameObject.SetActive(false);
                    break;
                case 2:
                    _octopus.transform.position = _endPosition.position;
                    _collider2D.enabled = true;
                    _trigger.SetActive(false);
                    _useSelect.gameObject.SetActive(false);
                    _useMonolog.gameObject.SetActive(true);
                    _spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
                    break;
            }
        }
    }
}