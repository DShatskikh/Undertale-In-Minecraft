using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class LocationHerobrineHome : MonoBehaviour
    {
        [SerializeField] 
        private Transform _startPoint;

        [SerializeField] 
        private Replica[] _replicas;

        [SerializeField] 
        private UnityEvent _event, _event2;

        private void OnEnable()
        {
            StartCoroutine(CutScene());
        }

        private IEnumerator CutScene()
        {
            GameData.Character.transform.position = _startPoint.position;
            GameData.Character.gameObject.SetActive(true);
            EventBus.OnSubmit = null;
            yield return null;
            GameData.Character.UseArea.gameObject.SetActive(false);
            yield return new WaitUntil(() => Vector3.Magnitude(GameData.Character.transform.position - _startPoint.position) > 2);
            _event.Invoke();
            GameData.Dialog.Show(_replicas);
            yield return new WaitUntil(() => !GameData.Dialog.gameObject.activeSelf);
            GameData.Character.enabled = true;
            GameData.Character.UseArea.gameObject.SetActive(true);
            _event2.Invoke();
        }
    }
}