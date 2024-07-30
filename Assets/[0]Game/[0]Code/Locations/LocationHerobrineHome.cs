using System;
using System.Collections;
using RimuruDev;
using UnityEngine;
using UnityEngine.Events;
using YG;

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
        
        [SerializeField]
        private GameObject _mobileInput, _pcInput;

        [SerializeField]
        private GameObject _herobrine;
        
        private void OnEnable()
        {
            StartCoroutine(CutScene());
        }

        private IEnumerator CutScene()
        {
            if (YandexGame.savesData.IsTelephone)
            {
                _herobrine.SetActive(false);
                yield break;
            }

            yield return null;
            
            if (GameData.DeviceType == CurrentDeviceType.WebMobile)
                _mobileInput.SetActive(true);
            else
                _pcInput.SetActive(true);
            
            GameData.Character.UseArea.gameObject.SetActive(false);
            GameData.Character.transform.position = _startPoint.position;
            GameData.Character.gameObject.SetActive(true);
            yield return new WaitUntil(() => Vector3.Magnitude(GameData.Character.transform.position - _startPoint.position) > 2);
            _event.Invoke();
            
            GameData.Dialog.SetReplicas(_replicas);
            yield return new WaitUntil(() => !GameData.Dialog.gameObject.activeSelf);
            GameData.Character.enabled = true;
            GameData.Character.UseArea.gameObject.SetActive(true);
            _event2.Invoke();
        }
    }
}