using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{

    public Animator Animator;

    public enum State
    {
        IDLE = 0,
        WALK = 1,
        CHARGE = 2,
        DASH = 3,
        AFTER_DASH = 4,
        HIT = 5,
        DEATH = 6
    };

    public int Health = 1;
    public float movementSpeed = 0.01f;
    public GameObject onDeathEffectPrefab;
    public float chargeDistance = 1.5f;
    public float chargeStartDistance = 1f;
    public float chargeTime = 1f;
    public float chargeSpeed = 0.4f;
    public float timeToSetDistance = 0.5f;
    public float dashRecoveryTime = 0.4f;
    public float hitRecoveryTime = 0.2f;

    public State currentState = State.WALK;

    private GameObject fighter;
    private bool isPositionSet = false;
    private Vector2 chargePosition;
    private float chargeTimer;
    private float afterDashTimer;
    private float hitRecoveryTimer;

    // Use this for initialization
    void Start()
    {
        fighter = GameObject.FindGameObjectWithTag("Fighter");
    }

    public void SetDireciton(Vector3 target)
    {
        var renderHandler = GetComponent<RenderHandler>();
        if (renderHandler != null)
        {
            var diff = target - transform.position;

            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            {
                if (diff.x > 0)
                {
                    renderHandler.direction = RenderHandler.Direction.E;
                }
                else
                {
                    renderHandler.direction = RenderHandler.Direction.W;
                }
            }
            else
            {
                if (diff.y > 0)
                {
                    renderHandler.direction = RenderHandler.Direction.N;
                }
                else
                {
                    renderHandler.direction = RenderHandler.Direction.S;
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState != State.DEATH)
        {
            if ((currentState == State.IDLE || currentState == State.WALK) && fighter != null)
            {
                // start charging
                if (Vector2.Distance(transform.position, fighter.transform.position) < chargeStartDistance)
                {
                    currentState = State.CHARGE;
                    isPositionSet = false;
                    chargeTimer = Time.time + chargeTime;
                }
                // follow
                transform.position = Vector2.MoveTowards(transform.position, fighter.transform.position, movementSpeed);
                SetDireciton(chargePosition);
            }
            else if ((currentState == State.CHARGE) && fighter != null)
            {
                // set position
                if (!isPositionSet && (chargeTimer - Time.time < timeToSetDistance))
                {
                    isPositionSet = true;
                    var direction = fighter.transform.position - transform.position;
                    direction.Normalize();
                    chargePosition = transform.position + direction * chargeDistance;
                    SetDireciton(chargePosition);
                }
                // charge time over -> dash
                if (chargeTimer < Time.time)
                {
                    currentState = State.DASH;
                }
            }
            // dash
            else if (currentState == State.DASH)
            {
                transform.position = Vector2.MoveTowards(transform.position, chargePosition, chargeSpeed);
                // wait a little
                if (Vector2.Distance(transform.position, chargePosition) < 0.1)
                {
                    afterDashTimer = Time.time + dashRecoveryTime;
                    currentState = State.AFTER_DASH;
                }
            }
            // end dash
            else if (currentState == State.AFTER_DASH && afterDashTimer < Time.time)
            {
                currentState = State.IDLE;
            }
            // stop follow
            else if (fighter == null)
            {
                currentState = State.IDLE;
            }
            else if (currentState == State.HIT && hitRecoveryTimer < Time.time)
            {
                if (Health <= 0)
                {
                    currentState = State.DEATH;
                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<BoxCollider2D>());
                }
                else
                {
                    currentState = State.IDLE;
                }
            }
        }
        Animator.SetInteger("State", (int)currentState);
    }

    public void Hit()
    {
        Instantiate(onDeathEffectPrefab, transform.position, Quaternion.identity);
        hitRecoveryTimer = Time.time + hitRecoveryTime;
        currentState = State.HIT;
        Health--;

    }
}
