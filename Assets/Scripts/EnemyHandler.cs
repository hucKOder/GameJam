using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    public enum State {IDLE, WALK, CHARGE, DASH, AFTER_DASH, DEATH};

    public float movementSpeed = 0.01f;
    public GameObject onDeathEffectPrefab;
    public float chargeDistance = 1.5f;
    public float chargeStartDistance = 1f;
    public float chargeCastTime = 1f;
    public float chargeSpeed = 0.4f;
    public float timeToSetDistance = 0.5f;
    public float afterDashTime = 0.4f;

    public State currentState = State.WALK;

    private GameObject fighter;
    private bool isCharging = false;
    private bool isPositionSet = false;
    private bool isInDash = false;
    private bool isInAfterDash = false;
    private bool isDeath = false;
    private Vector2 chargePosition;
    private float chargeTimer;
    private float afterDashTimer;

	// Use this for initialization
	void Start () {
        fighter = GameObject.FindGameObjectWithTag("Fighter");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isDeath)
        {
            if (!isCharging && !isInDash && !isInAfterDash && fighter != null)
            {
                if (Vector2.Distance(transform.position, fighter.transform.position) < chargeStartDistance)
                {
                    isCharging = true;
                    isPositionSet = false;
                    chargeTimer = Time.time + chargeCastTime;
                    currentState = State.CHARGE;
                }
                transform.position = Vector2.MoveTowards(transform.position, fighter.transform.position, movementSpeed);
            }
            else if ((isCharging || isInDash || isInAfterDash) && fighter != null)
            {
                if (!isPositionSet && (chargeTimer - Time.time < timeToSetDistance))
                {
                    isPositionSet = true;
                    var direction = fighter.transform.position - transform.position;
                    direction.Normalize();
                    chargePosition = transform.position + direction * chargeDistance;
                }
                if (!isInDash && !isInAfterDash && chargeTimer < Time.time)
                {
                    isInDash = true;
                    isCharging = false;
                    currentState = State.DASH;
                }
                if (isInDash)
                {
                    transform.position = Vector2.MoveTowards(transform.position, chargePosition, chargeSpeed);
                    if (Vector2.Distance(transform.position, chargePosition) < 0.1)
                    {
                        isInDash = false;
                        isInAfterDash = true;
                        afterDashTimer = Time.time + afterDashTime;
                        currentState = State.AFTER_DASH;
                    }
                }
                if (isInAfterDash && afterDashTimer < Time.time)
                {
                    isInAfterDash = false;
                    currentState = State.WALK;
                }
            }
            else if (fighter == null)
            {
                currentState = State.IDLE;
            }
        }
	}

    public void Die()
    {
        Instantiate(onDeathEffectPrefab, transform.position, Quaternion.identity);
        isDeath = true;
        currentState = State.DEATH;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
    }
}
