using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shroom : MonoBehaviour
{

    public Animator animator;
    public int detectionRadius;
    public float speed;
    public int WanderRadius;
    public Rigidbody2D rb;
    public HealthBar healthBar;
    private PlayerController player;
    //public GameObject[] obstacles;
    public Tilemap obstacles;
    public int health = 3;
    public int damage = 1;
    public GameObject shroom;
    public AudioClip hitclip;

    private Transform target;
    private Dropper Dropper;

    private bool attacking;
    private bool charging;
    private bool attackCooldown;
    private bool idleMoving;
    private bool spawning;

    private Vector3 SpawnPos;
    private float circlingDistance;
    float targetDistance;
    private bool clockWise;
    private bool dead;
    private bool knockBack;

    Vector2 movement;

    public GameObject point;

    void Start()
    {
        //        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        //        Instantiate(point, obstacles[0].transform.position, Quaternion.identity);

        Dropper = gameObject.GetComponentInChildren<Dropper>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SpawnPos = transform.position;

        circlingDistance = Random.Range(20, 30) / 10f;

        movement.x = 0;
        movement.y = 0;

        attacking = false;
        attackCooldown = false;
        charging = false;
        idleMoving = false;
        dead = false;
        knockBack = false;
        spawning = true;

        Invoke(nameof(resetSpawn), 1);

        healthBar.SetMaxHealth(health);
        clockWise = Random.Range(0, 2) == 1;
        animator.SetTrigger("Spawn");
    }

    void Update()
    {

        /*if (obstacles.GetTile(obstacleMapTile) == null)
        {

        }*/

        var targetVector = new Vector2(-transform.position.x + target.position.x, -transform.position.y + target.position.y);
        targetDistance = Vector2.Distance(transform.position, target.position);

        if (dead || spawning)
        {
            movement.x = 0;
            movement.y = 0;
        }
        else if (targetDistance < detectionRadius && targetDistance >= circlingDistance && !attacking && !charging && !knockBack)
        {
            animator.SetFloat("spedpara", 1f);
            movement.x = targetVector.x / targetDistance;
            movement.y = targetVector.y / targetDistance;
        }
        else if (targetDistance > detectionRadius)
        {
            idle();
        }
        else if (!attacking && !charging && !knockBack)
        {
            movement.x = 0;
            movement.y = 0;
            circle(targetDistance, targetVector);
        }
        else if (charging)
        {
            movement.x = 0;
            movement.y = 0;
        }
        else if (attacking)
        {
            movement.y = targetVector.y / targetDistance;
            movement.x = targetVector.x / targetDistance;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    // Function to call if the shroom is hit
    public void hit(int damage)
    {
        var targetVector = new Vector2(-transform.position.x + target.position.x, -transform.position.y + target.position.y);
        float distance = Vector2.Distance(transform.position, target.position);
        movement.x = -targetVector.x * 2 / distance;
        movement.y = -targetVector.y * 2 / distance;
        knockBack = true;
        Invoke(nameof(resetKnockBack), 0.25f);

        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            dead = true;
            Destroy(shroom, 5);
            Dropper.drop();
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Dead");
        }
    }

    private Vector3 prevRoamPoint;
    private void idle()
    {

        // Pick random point within certain radius of spawnpoint
        float radius = Random.Range(0, WanderRadius);
        float x = Random.Range(-radius, radius);
        float y = Mathf.Sqrt(radius * radius - x * x);
        int yNeg = Random.Range(0, 2);
        if (yNeg == 1) y = -y;
        var RoamPoint = new Vector3(x + SpawnPos.x, y + SpawnPos.y, 0);
        prevRoamPoint = RoamPoint;

        // Move towards that point

        Instantiate(point, RoamPoint, Quaternion.identity);
        animator.SetFloat("spedpara", 0.8f);
        if (!idleMoving)
        {

            float distance = Vector2.Distance(RoamPoint, transform.position);
            movement.x = (RoamPoint.x - transform.position.x) / distance;
            movement.y = (RoamPoint.y - transform.position.y) / distance;
            idleMoving = true;

            Invoke(nameof(resetIdle), Random.Range(5, 10));
        }
        else if (Vector3.Distance(transform.position, prevRoamPoint) <= 0.5f)
        {
            movement.x = 0;
            movement.y = 0;
        }
    }


    private void circle(float distance, Vector2 targetVector)
    {

        if (distance <= circlingDistance)
        {
            if (clockWise)
            {
                movement.x = -targetVector.y / distance;
                movement.y = targetVector.x / distance;
            }
            else
            {
                movement.x = targetVector.y / distance;
                movement.y = -targetVector.x / distance;
            }

            if (!attacking && !attackCooldown && !charging)
            {
                attack();
            }
        }
        if (distance < circlingDistance - 0.5f)
        {

            movement.x = -targetVector.x / distance;
            movement.y = -targetVector.y / distance;
        }
    }

    private void attack()
    {
        animator.SetTrigger("Attack");
        charging = true;
        attackCooldown = true;

        Invoke(nameof(resetCharge), 0.5f);
        Invoke(nameof(attackCd), Random.Range(5.5f, 8f));
    }

    private void resetCharge()
    {
        charging = false;
        attacking = true;

        float attackTime = circlingDistance / (speed + 1);
        Invoke(nameof(resetAttack), attackTime);
    }
    private void resetAttack()
    {
        attacking = false;
        if (targetDistance <= 1)
        {
            player.hit(damage);
        }
    }
    private void attackCd()
    {
        attackCooldown = false;
    }
    private void resetIdle()
    {
        idleMoving = false;
    }
    private void resetSpawn()
    {
        spawning = false;
    }
    private void resetKnockBack()
    {
        knockBack = false;
    }
}
