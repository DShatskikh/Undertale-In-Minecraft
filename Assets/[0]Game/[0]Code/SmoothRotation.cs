using System;
using Game;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 10f;
    public Rigidbody2D Rigidbody2D;

    private void Start()
    {
        target = GameData.Heart.transform;
    }

    private void Update()
    {
        var n = ((Vector2) target.position - (Vector2)transform.position).normalized;
        float rotation_z = Mathf.Atan2(n.y, n.x) * Mathf.Rad2Deg;
        Rigidbody2D.MoveRotation(Mathf.LerpAngle( Rigidbody2D.rotation, rotation_z + 180, Time.deltaTime * rotationSpeed));
    }
}