using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public BoxCollider2D Bc2d;
    public Rigidbody2D Rb2d;
    public int Health;
    public float Speed;

    public Animator Anim;

    public float DamageTimeMax;
    public float DeathTime;

    public AudioClip LifeLostSound;
    public AudioClip HitSound;
    public AudioClip DeathSound;


    private float startSpeed;
    private GameManager gM;
    private float damageTime;

    // Start is called before the first frame update
    void Start()
    {
        //Find the GameController object on the screen for score purposes
        gM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        //Sets the enemy's speed, depending on what enemy it is
        startSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Time events
        if (Anim.GetBool("Dead") == true) //Controls the delay between the death animation and being destroyed
        {
            Anim.SetBool("Moving", true);
            Speed = 0;
            if (DeathTime > 0)
            {
                DeathTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        } else
        {
            if (damageTime > 0) //Controls the delay between the stun animation and moving again, but only if the death animation is currently not playing
            {
                Speed = 0;
                Anim.SetBool("Moving", false);
                damageTime -= Time.deltaTime;
            }
            else
            {
                Speed = startSpeed;
                Anim.SetBool("Moving", true);
            }
        }
    }

    private void FixedUpdate()
    {
        //Movement control
        Rb2d.velocity = new Vector2(-Speed, 0);
    }


    public void OnTriggerEnter2D(Collider2D collision) //Collisions with bullets decrease health, and when health would equal 0, the enemy goes into the death animation
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Destroy(collision.gameObject);  Done in the bullet object

            if (Health > 1) //On collision with bullet, lose health (if not at 1 HP) or go through death processes (if at 1 HP)
            {
                Health -= 1;
                //Anim.SetBool("Moving", false);
                damageTime = DamageTimeMax;
                AudioSource.PlayClipAtPoint(HitSound, transform.position);
            }
            else
            {
                gM.UpdateScore(); //Gives score through the GameManager
                Anim.SetBool("Dead", true);
                AudioSource.PlayClipAtPoint(DeathSound, transform.position);

                Bc2d.enabled = false;
            }
        }

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Killzone") //Checks for collisions that would cause a life to be lost (player or back wall)
        {
            gM.UpdateLives();
            AudioSource.PlayClipAtPoint(LifeLostSound, transform.position);

            Destroy(gameObject);
        }
    }
}
