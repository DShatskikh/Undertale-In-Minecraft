using System;
using UnityEngine;

namespace Game
{
    public static class DirectionExtensions
    {
        public static Vector2 GetDirection8(Vector2 direction)
        {
            var x = direction.x;
            var y =  direction.y;
        
            if (x is > -0.5f and < 0.5f && y > 0.5f)
                return Vector2.up;

            if (y is > -0.5f and < 0.5f && x > 0.5f)
                return Vector2.right;
            
            if (x is > -0.5f and < 0.5f && y < -0.5f)
                return Vector2.down;
            
            if (y is > -0.5f and < 0.5f && x < -0.5f)
                return Vector2.left;
            
            if (x > 0f && y > 0f)
                return new Vector2(0.5f, 0.5f);
            
            if (x > 0f && y < 0f)
                return new Vector2(0.5f, -0.5f);
            
            if (x < 0f && y < 0f)
                return new Vector2(-0.5f, -0.5f);
                        
            if (x < 0f && y > 0f)
                return new Vector2(-0.5f, 0.5f);

            throw new Exception("Добавь направление");
        }
    }
}