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
        Die = 5,
        Dead = 6,
    }

    public enum Weapon
    {
        Sword,
        FireBlade,
        FrostAxe
    }

    public Animator animator;

    public GameObject onDeathPrefab;

    public PlayerState state;

    public Weapon weapon;

    public GameObject weaponSlot;

    public Sprite[] weaponSkins;

    public float dashTimeout = 0.3f; // inability to dash right after
    public float dashRecoveryTime = 0.15f; // invincible frame after dash
    public float dashMaxLength = 1f;
    public float dashSpeed = 0.4f;
    public SceneController sceneController;
    public AudioClip[] att; 


    public float movementSpeed = 0.02f;

    private float dashTimer;
    private float dashRecoveryTimer;
    private Vector2 dashPosition;

    private VisualHandler visualHandler;

    private bool invincible = false;

    // Use this for initialization
    void Start()
    {
        dashTimer = 0f;
        visualHandler = GetComponent<VisualHandler>();
        if (weaponSlot)
        {
            var wpn = weaponSlot.GetComponent<SpriteRenderer>();
            wpn.sprite = weaponSkins[(int)weapon];
        }
    }

    void FixedUpdate()
    {
        animator.SetInteger("State", (int)state);

        //DEAD
        if (state == PlayerState.Dead)
        {
            return;
        }

        //BEH
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
                visualHandler.SetDireciton(hitPoint);
            }
        }
        //PRIPRAVA NA DASH
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
                visualHandler.SetDireciton(dashPosition);
                dashTimer = Time.time + dashTimeout;
                state = PlayerState.Dash;
                invincible = true;
            }
        }
        //DASH
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
                var rand = Random.Range(0, att.Length);
                var ass = GetComponent<AudioSource>();
                ass.clip = att[rand];
                ass.Play();
            }
            else
            {
                Instantiate(onDeathPrefab, transform.position, Quaternion.identity);
                state = PlayerState.Dead;
                //GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(transition());
            }
        }
    }

    IEnumerator transition()
    {
        yield return new WaitForSeconds(2f);
        sceneController.LoadNextScene();
    }
}


