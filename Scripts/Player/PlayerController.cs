using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer Renderer;
    public float movementSpeed = 1f;
    public VectorValue startingPosition;
    public Vector2 movement;

    public Sprite back;
    public Sprite front;
    public Sprite right;
    public Sprite left;
    private AudioSource footstep;
    public string facing;

    public int health;
    public int numberOfHearts;

    public Image[] Hearts;
    public Sprite HeartFull;
    public Sprite HeartHalf;
    public Sprite HeartEmpty;
    public AudioClip hitsound;


    void Start()
    {

        Renderer = GetComponent<SpriteRenderer>();
        facing = "side";
        transform.position = startingPosition.initialValue;
        health = 6;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Hearts.Length * 2; i++)
        {
            if (i % 2 == 0)
            {
                if (i < health)
                {
                    Hearts[i / 2].sprite = HeartFull;
                }
                else
                {
                    Hearts[i / 2].sprite = HeartEmpty;
                }
            }
            else
            {
                if (i == health)
                {
                    Hearts[i / 2].sprite = HeartHalf;
                }
            }
        }


        //FLIP CHARARACTER
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -1;
            animator.SetFloat("Speedx", 1);
            facing = "side";
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 1;
            animator.SetFloat("Speedx", 1);
            facing = "side";
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetFloat("Speedx", 0);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            facing = "down";
            animator.SetFloat("Speedy", 1);
            animator.SetFloat("Down", 1);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            facing = "up";
            animator.SetFloat("Up", 1);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            animator.SetFloat("Speedy", 0);
            animator.SetFloat("Up", 0);
            animator.SetFloat("Down", 0);
        }
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            facing = "side";
            animator.SetFloat("Speedy", 0);
            animator.SetFloat("Up", 0);
            animator.SetFloat("Down", 0);

        }

        transform.localScale = characterScale;

        //Movement
        if (Input.GetKey("w"))
        {
            movement.y = 2 * movementSpeed;
            Renderer.sprite = back;

        }
        if (Input.GetKey("s"))
        {
            movement.y = -2 * movementSpeed;
            Renderer.sprite = front;
        }
        if (Input.GetKey("d"))
        {
            movement.x = 2 * movementSpeed;
            Renderer.sprite = right;

        }
        if (Input.GetKey("a"))
        {
            movement.x = -2 * movementSpeed;
            Renderer.sprite = left;

        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s"))
        {
            movement.y = 0;
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            movement.x = 0;
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
    private void Footstep()
    {
        FindObjectOfType<AudioManager>().Play("ran");
    }
    public void hit(int damage)
    {
        health -= damage;
        Renderer.color = new Color(255f, 0f, 0f, 255f);
        Invoke(nameof(changecolor), 0.2f);
        AudioSource.PlayClipAtPoint(hitsound, transform.position);
        if (health <= 0)
        {
            Invoke(nameof(dead), 1f);
        }
    }
    private void changecolor()
    {
        Renderer.color = new Color(1f, 1f, 1f, 1f);
    }
    private void dead()
    {
        SceneManager.LoadScene(0);
    }

}
