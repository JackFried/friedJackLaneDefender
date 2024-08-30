using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public int Health;
    public float Speed;

    public Animator Anim;

    public float DamageTimeMax;
    public float DeathTime;


    private float startSpeed;
    private GameManager gM;
    private float damageTime;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        startSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Time events
        if (Anim.GetBool("Dead") == true)
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
            if (damageTime > 0)
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
        Rb2d.velocity = new Vector2(-Speed, 0);
    }


    public void OnTriggerEnter2D(Collider2D collision) //Collisions with bullets decrease health, and when health would equal 0, the enemy goes into death anim
    {
        if (collision.gameObject.tag == "Bullet") //Enemy is only affected by the bullet if they are not in i-frames
        {
            //Destroy(collision.gameObject);  Done in the bullet object

            if (Health > 1)
            {
                Health -= 1;
                //Anim.SetBool("Moving", false);
                damageTime = DamageTimeMax;
            }
            else
            {
                gM.UpdateScore(); //Gives score through the GameManager
                Anim.SetBool("Dead", true);
            }
        }

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Killzone")
        {
            gM.UpdateLives();
            Destroy(gameObject);
        }
    }
}
