using System.Collections;
using UnityEngine;

namespace Game
{
    public class HeartView : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _source;
        
        [SerializeField]
        private SpriteRenderer _shield;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private HeartModel _model;
        private Coroutine _coroutine;

        public void SetModel(HeartModel model)
        {
            _model = model;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _model.ShieldActivate += OnShieldActivate;
            _model.SpeedChange += ModelOnSpeedChange;
            _model.DirectionChange += OnDirectionChange;
            _model.Invulnerability += OnInvulnerability;
        }

        private void OnDestroy()
        {
            _model.ShieldActivate -= OnShieldActivate;
            _model.SpeedChange -= ModelOnSpeedChange;
            _model.DirectionChange -= OnDirectionChange;
            _model.Invulnerability -= OnInvulnerability;
        }

        private void ModelOnSpeedChange(float value)
        {
            _animator.SetFloat("Speed", value > 0 ? 1 : 0);
        }

        private void OnDirectionChange(Vector2 value)
        {
            if (value.x > 0) 
                Flip(false);
                
            if (value.x < 0) 
                Flip(true);
        }
        
        private void Flip(bool isFlip)
        {
            _spriteRenderer.flipX = isFlip;
        }
        
        private void OnShieldActivate(bool isActivate)
        {
            if (isActivate)
                _source.Play();

            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitTransparency(isActivate));
        }

        private void OnInvulnerability(bool value)
        {
            _spriteRenderer.color = value ? new Color(1, 163 / 255f, 163 / 255f) : Color.white;
        }
        
        private IEnumerator AwaitTransparency(bool isActivate)
        {
            var progress = 0f;
            var startA = _shield.color.a;
            var endA = isActivate ? 1 : 0;
            var duration = 0.1f;

            while (progress < 1)
            {
                progress += Time.deltaTime / duration;
                _shield.color = _shield.color.SetA(Mathf.Lerp(startA, endA, progress));
                yield return null;
            }
        }
    }
}