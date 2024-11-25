using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class DreamCutscene : BaseCutscene
    {
        [SerializeField]
        private Transform _follow;
        
        private void Start()
        {
            StartCutscene();
        }

        protected override IEnumerator AwaitCutscene()
        {
            GameData.CinemachineVirtualCamera.Follow = _follow;
            GameData.CinemachineVirtualCamera.transform.position = _follow.position;
            yield return new WaitForSeconds(5);
            //gameObject.SetActive(false);
            
            GameData.LocationsManager.gameObject.SetActive(false);
        }
    }
}