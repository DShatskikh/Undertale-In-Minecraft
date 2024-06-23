using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game
{
    public class Tools : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        private GameObject[] _locations;

        [FormerlySerializedAs("_inclusion")] [SerializeField]
        private GameObject _introduction;

        [SerializeField]
        private GameObject[] _ends;

        [SerializeField]
        private Character _character;
        
        [ContextMenu("Сброс в состояние билда")]
        private void BuildState()
        {
            _character.gameObject.SetActive(false);
            
            foreach (var location in _locations)
            {
                location.SetActive(false);
            }
            
            foreach (var end in _ends)
            {
                end.SetActive(false);
            }
            
            _introduction.SetActive(false);
        }

        [ContextMenu("Начало игры")]
        private void StartGame()
        {
            _character.gameObject.SetActive(true);

            foreach (var location in _locations)
            {
                location.SetActive(false);
            }

            foreach (var end in _ends)
            {
                end.SetActive(false);
            }
            
            _introduction.SetActive(true);
        }
        
        [ContextMenu("Дом Херобрина")]
        private void HerobrineHome()
        {
            _character.gameObject.SetActive(true);
            _character.transform.position = _locations[0].transform.position;

            foreach (var location in _locations)
            {
                location.SetActive(false);
            }
            
            _locations[0].SetActive(true);

            foreach (var end in _ends)
            {
                end.SetActive(false);
            }
            
            _introduction.SetActive(false);
        }

        [ContextMenu("Удалить сохранения")]
        private void ResetSave()
        {
            foreach (var saver in FindObjectsByType<SaveLoadBase>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                saver.Reset();
            }
            
            GameData.Saver.Reset();
            SceneManager.LoadScene(0);
        }
#endif
    }
}