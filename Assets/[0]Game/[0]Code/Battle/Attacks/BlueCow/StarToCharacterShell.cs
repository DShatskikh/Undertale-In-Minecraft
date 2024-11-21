using UnityEngine;

namespace Game
{
    public class StarToCharacterShell : StarBaseShell
    {
        protected override Vector3 GetDirection()
        {
            return (GameData.HeartController.transform.position - transform.position).normalized;
        }
    }
}