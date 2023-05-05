using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
    public float walkSpeed;

    private Vector3 _walk;
    private Vector3 _look;

    private void FixedUpdate()
    {
        var pos = transform.position + (transform.rotation * _walk * Time.fixedDeltaTime);
        pos.y = 6;
        transform.position = pos;
        if (_look.magnitude >= 0.0001)
        {
            var val = transform.rotation.eulerAngles;
            if (val.x > 180) val.x -= 360;
            val.x = Mathf.Clamp(val.x - _look.y, -30, 30);
            val.y += _look.x;
            transform.rotation = Quaternion.Euler(val);
        }
    }

    private void OnLook(InputValue value)
    {
        var dir = value.Get<Vector2>() * 5.0f;
        _look = dir;
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
