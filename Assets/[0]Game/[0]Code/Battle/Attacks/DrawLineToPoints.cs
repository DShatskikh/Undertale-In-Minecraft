using System.Linq;
using UnityEngine;

namespace Game
{
    public class DrawLineToPoints : MonoBehaviour
    {
        [SerializeField] 
        private LineRenderer _lineRenderer;
        
        [SerializeField] 
        private Transform[] _points;

        private void Update()
        {
            var lengthOfLineRenderer = 50;
            var points = new Vector3[lengthOfLineRenderer];
            var t = Time.time;
            for (int i = 0; i < lengthOfLineRenderer; i++)
            {
                points[i] = new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f);
            }
            _lineRenderer.SetPositions(points);
        }
    }
}