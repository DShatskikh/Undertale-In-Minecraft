using UnityEngine;

namespace Game
{
    public class FollowToCharacter : MonoBehaviour
    {
        private void Update()
        {
            if (GameData.CharacterController) 
                transform.position = GameData.CharacterController.transform.position;
        }
    }
}