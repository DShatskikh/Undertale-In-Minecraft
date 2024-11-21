using UnityEngine;

namespace Game
{
    public class StarShell : StarBaseShell
    {
        public Vector3 Direction = new Vector3(1, 0);
        
        protected override Vector3 GetDirection()
        {
            return Direction;
        }
    }
}