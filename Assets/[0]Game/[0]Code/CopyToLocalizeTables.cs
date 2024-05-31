using UnityEngine;

namespace Game
{
    public class CopyToLocalizeTables : MonoBehaviour
    {
        #if UNITY_EDITOR
        [ContextMenu("CopyTo")]
        private void CopyTo()
        {
            foreach (var useMonolog in FindObjectsByType<OpenMonolog>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                
            }
        }

        [ContextMenu("CopyToSelect")]
        private void CopyToSelect()
        {
            foreach (var useSelect in FindObjectsByType<OpenSelect>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                
            }
            
            print("CopyToSelect");
        }
        #endif
    }
}