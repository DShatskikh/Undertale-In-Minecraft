using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game
{
    public class TimerActivateGroup : MonoBehaviour
    {
        [SerializeField]
        private float _duration = 1;

        [SerializeField]
        private GameObject[] _attacks;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            Use();
        }

        public void Use()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = GameData.Startup.StartCoroutine(TimerProcess());
        }

        private IEnumerator TimerProcess()
        {
            foreach (var attack in _attacks)
            {
                attack.SetActive(true);
                yield return new WaitForSeconds(_duration);
            }
        }
    }
}