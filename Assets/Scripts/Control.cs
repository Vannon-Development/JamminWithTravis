using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
    public float walkSpeed;

    private Vector3 _walk;

    private void FixedUpdate()
    {
        transform.position += _walk * Time.deltaTime;
    }

    private void OnLook(InputValue value)
    {
        
    }

    private void OnMouseLook(InputValue value)
    {
        var dir = value.Get<Vector2>() * 0.6f;
        if (dir.magnitude >= 0.0001)
        {
            var val = transform.rotation.eulerAngles;
            if (val.x > 180) val.x -= 360;
            val.x = Mathf.Clamp(val.x - dir.y, -30, 30);
            val.y += dir.x;
            transform.rotation = Quaternion.Euler(val);
        }
    }

    private void OnMove(InputValue value)
    {
        var dir = value.Get<Vector2>();
        if (dir.magnitude >= 0.0001)
            _walk = new Vector3(dir.x, 0, dir.y) * walkSpeed;
        else
            _walk = Vector3.zero;
    }
}
