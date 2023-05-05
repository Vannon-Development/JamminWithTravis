using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
    public float walkSpeed;

    private Vector3 _walk;
    private Vector3 _look;
    private Vector3 _frameLook;
    private Rigidbody _body;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _body.angularVelocity = Vector3.zero;
        var pos = transform.rotation * _walk;
        pos.y = 0;
        _body.velocity = pos;
        
        if (_look.magnitude >= 0.0001 || _frameLook.magnitude >= 0.0001)
        {
            var look = _look + _frameLook;
            var val = transform.rotation.eulerAngles;
            if (val.x > 180) val.x -= 360;
            val.x = Mathf.Clamp(val.x - look.y, -30, 30);
            val.y += look.x;
            _body.MoveRotation(Quaternion.Euler(val));
        }

        _frameLook = Vector3.zero;
    }

    private void OnLook(InputValue value)
    {
        var dir = value.Get<Vector2>() * 5.0f;
        _look = dir;
    }

    private void OnMouseLook(InputValue value)
    {
        var dir = value.Get<Vector2>() * 5.0f;
        _frameLook = dir;
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
