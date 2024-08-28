using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    public int Health;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Rb2d.velocity = new Vector2(-Speed, 0);
    }


    public void OnTriggerEnter2D(Collider2D collision) //Collisions with bullets decrease health, and when health would equal 0, the enemy is destroyed
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);

            if (Health > 1)
            {
                Health -= 1;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
