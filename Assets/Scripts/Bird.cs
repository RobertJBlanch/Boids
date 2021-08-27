using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bird : MonoBehaviour
{
    Collider2D agentCollider;
    public float maxSpeed = 3f;
    Vector2 smoothDampVelocity = Vector2.zero;
    public float smoothTime = 2f;
    public float rotationTime = 20f;
    public float rollAmount = 10f;
    public int id = 0;
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 v){

        if (v == Vector2.zero)
            v = transform.up;

        transform.position = Vector2.SmoothDamp(transform.position, (Vector2)transform.position + (v.normalized * maxSpeed), ref smoothDampVelocity, smoothTime, maxSpeed);
        // if (smoothDampVelocity != Vector2.zero) transform.forward = smoothDampVelocity;

        transform.up = v;
        // transform.position += (Vector3)v * Time.deltaTime;

    }
}
