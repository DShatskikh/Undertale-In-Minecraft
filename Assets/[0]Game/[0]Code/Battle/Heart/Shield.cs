using System.Collections;
using UnityEngine;

namespace Game
{
    public class Shield
    {
        private Coroutine _coroutine;
        private readonly HeartModel _model;
        private const float DelayUse = 0.1f;

        public Shield(HeartModel model)
        {
            _model = model;
        }

        public void Execute(Vector2 position)
        {
            if (_coroutine == null)
            {
                if (IsShel(Physics2D.OverlapCircleAll(position, 0.55f)))
                    _coroutine = GameData.Startup.StartCoroutine(Use());
            }
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
            _model.AddTurnProgress(1);
            _model.SetIsShield(true);
            yield return new WaitForSeconds(DelayUse);
            _model.SetIsShield(false);
            _coroutine = null;
        }
    }
}