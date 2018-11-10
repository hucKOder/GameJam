using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterHandler : MonoBehaviour
{
    public enum PlayerState
    {
        Idle = 0,
        Walk = 1,
        Dash = 2,
        AfterDash = 3,
        Hit = 4,
        Dead = 5,
    }

    public PlayerState state;
    public float dashTimeout = 0.3f; // inability to dash right after
    public float dashRecoveryTime = 0.15f; // invincible frame after dash
    public float dashMaxLength = 1f;
    public float dashSpeed = 0.4f;

    public float movementSpeed = 0.02f;

    private float dashTimer;
    private float dashRecoveryTimer;
    private Vector2 dashPosition;

    private bool invincible = false;

    // Use this for initialization
    void Start()
    {
        dashTimer = 0f;
    }

    void FixedUpdate()
    {
        if (state == PlayerState.Dead)
        {
            return;
        }

        if (state != PlayerState.Hit
            && state != PlayerState.Dash
            && dashTimer < Time.time
            && Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                transform.position = Vector2.MoveTowards(transform.position, hitPoint, movementSpeed);
                state = PlayerState.Walk;
            }
        }
        if (state != PlayerState.Hit
            && state != PlayerState.Dash
            && dashTimer < Time.time
            && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                if (Vector2.Distance(hitPoint, transform.position) > dashMaxLength)
                {
                    Vector3 direction = hitPoint - transform.position;
                    direction.Normalize();
                    dashPosition = transform.position + direction * dashMaxLength;
                }
                else
                {
                    dashPosition = hitPoint;
                }
                dashTimer = Time.time + dashTimeout;
                state = PlayerState.Dash;
                invincible = true;
            }
        }
        if (state == PlayerState.Dash)
        {
            transform.position = Vector2.MoveTowards(transform.position, dashPosition, dashSpeed);
            if (Vector2.Distance(transform.position, dashPosition) < 0.1)
            {
                dashRecoveryTimer = Time.time + dashRecoveryTime;
                invincible = true;
                state = PlayerState.AfterDash;
            }
        }
        // disable invincibility from dash
        if (dashRecoveryTimer < Time.time && invincible)
        {
            invincible = false;
        }
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (state == PlayerState.Dash || invincible)
            {
                var enemyHandler = other.GetComponent<EnemyHandler>();
                enemyHandler.Hit();
            }
            else
            {
                state = PlayerState.Dead;
                //GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
