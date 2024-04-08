using UnityEngine;

namespace Super_Auto_Mobs
{
    public static class MathfExtensions
    {
        public static int RepeatInt(int value, int length)
        {
            return (value % length + length) % length;
        }
        
        public static Vector3[] CreateSineWave(Vector3 start, Vector3 end, int resolution = 50, float amplitude = 1, float frequency = 1) 
        {
            Vector3[] points = new Vector3[resolution];
            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);
            
            for (int i = 0; i < resolution; i++) {
                float t = (float)i / (float)(resolution - 1);
                float x = t * distance;
                float y = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * t);
                points[i] = start + x * direction + y * Vector3.up;
            }
            
            return points;
        }

        public static int GetPercent(int count)
        {
            return (int)Mathf.Floor((count / 100f));
        }
    }
}