using System.Collections;
using UnityEngine;

namespace Game
{
    public class Shield
    {
        private Coroutine _coroutine;
        private readonly HeartModel _model;
        private const float ShieldActiveTime = 0.15f;
        private const float ShieldDeactivateTime = 0.1f;

        public Shield(HeartModel model)
        {
            _model = model;
        }

        public void Execute(Vector2 position)
        {
            if (_coroutine == null)
            {
                if (IsShel(Physics2D.OverlapBoxAll(position, Vector2.one * 1f, 0)))
                    _coroutine = GameData.Startup.StartCoroutine(Use());
            }
        }

        public void Off()
        {
            if (_coroutine != null)
            {
                GameData.Startup.StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _model.SetIsShield(false);
        }

        private bool IsShel(Collider2D[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Shell shell))
                    return true;
            }

            return false;
        }

        private IEnumerator Use()
        {
            _model.AddTurnProgress(2);
            _model.SetIsShield(true);
            yield return new WaitForSeconds(ShieldActiveTime);
            _model.SetIsShield(false);
            yield return new WaitForSeconds(ShieldDeactivateTime);
            _coroutine = null;
        }
    }
}