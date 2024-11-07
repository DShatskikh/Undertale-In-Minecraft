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
        
        public static Vector2 GetDirection4(Vector2 direction)
        {
            var x = direction.x;
            var y =  direction.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    return Vector2.left;
                else
                    return Vector2.right;
            }
            else
            {
                if (y > 0f)
                    return Vector2.up;
                else
                    return Vector2.down;
            }

            throw new Exception("Добавь направление");
        }
        
        public static Vector3 GetAngle(this ArrowDirection arrowDirection)
        {
            var angle = arrowDirection switch
            {
                ArrowDirection.Up => 0,
                ArrowDirection.RightUp => -45,
                ArrowDirection.Right => -90,
                ArrowDirection.RightDown => -135,
                ArrowDirection.Down => -180,
                ArrowDirection.LeftDown => -225,
                ArrowDirection.Left => -270,
                ArrowDirection.LeftUp => -315,
                _ => throw new ArgumentOutOfRangeException(nameof(arrowDirection), arrowDirection, null)
            };

            return new Vector3(0, 0, angle);
        }
    }
}