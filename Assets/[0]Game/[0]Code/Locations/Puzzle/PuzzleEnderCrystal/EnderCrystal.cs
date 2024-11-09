using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game
{
    public class EnderCrystal : UseObject
    {
        [SerializeField]
        private EnderCrystalBarrier _barrier;

        [SerializeField]
        private SortingGroup _sortingGroup;

        [SerializeField]
        private Transform _shadow;
        
        public SortingGroup SortingGroup => _sortingGroup;
        
        public override void Use()
        {
            StartCoroutine(AwaitUse());
        }

        private IEnumerator AwaitUse()
        {
            GameData.CharacterController.enabled = false;

            GetComponent<Collider2D>().enabled = false;
            _sortingGroup.sortingOrder = 1;
            _shadow.gameObject.SetActive(false);
            
            var moveToPointCommand = new MoveToPointCommand(transform, GameData.CharacterController.View.transform.position.AddY(1.2f), 0.5f);
            yield return moveToPointCommand.Await();

            _barrier.Show(this);
            
            transform.SetParent(GameData.CharacterController.transform);
            
            GameData.CharacterController.enabled = true;
        }
    }
}