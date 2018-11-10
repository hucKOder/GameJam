using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterHandler : MonoBehaviour {

    public float dashTimeout = 0.3f;
    public float dashMaxLength = 1f;
    public float dashSpeed = 0.4f;

    private float dashTimer;
    private Vector2 dashPosition;

    private bool isDashing = false;

	// Use this for initialization
	void Start () {
        dashTimer = 0f;
	}
	
	void FixedUpdate () {
		if (dashTimer < Time.time && Input.GetMouseButtonDown(0))
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
                isDashing = true;
            }
        }
        if (isDashing)
        {
            transform.position = Vector2.MoveTowards(transform.position, dashPosition, dashSpeed);
            if (Vector2.Distance(transform.position, dashPosition) < 0.1)
            {
                isDashing = false;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (isDashing)
            {
                var enemyHandler = other.GetComponent<EnemyHandler>();
                enemyHandler.Die();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
