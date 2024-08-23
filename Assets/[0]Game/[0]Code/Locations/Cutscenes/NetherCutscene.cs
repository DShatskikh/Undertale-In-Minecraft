using System.Collections;
using UnityEngine;

namespace Game
{
    public class NetherCutscene : MonoBehaviour
    {
        [SerializeField]
        private GameObject _muhomor, _golem, _golemStay, _portalTrigger;

        [SerializeField]
        private UseDialog _useDialog;
        
        private IEnumerator Start()
        {
            GameData.CharacterController.enabled = false;
            
            _muhomor.transform.position = GameData.CompanionsManager.GetCompanion(CompanionType.Mushroom).transform.position;
            GameData.CompanionsManager.TryDeactivateCompanion(CompanionType.Mushroom);
            _muhomor.SetActive(true);
            _muhomor.GetComponent<Animator>().SetFloat("Speed", 3);

            yield return new WaitForSeconds(1);
            _golemStay.SetActive(false);
            _golem.SetActive(true);
            
            yield return new WaitForSeconds(1);
            _useDialog.Use();
            
            yield return new WaitForSeconds(3);
            _portalTrigger.SetActive(true);
            GameData.CharacterController.enabled = true;
        }
    }
}