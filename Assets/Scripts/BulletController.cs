using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float XSpeed;
    public Rigidbody2D Rb2d;
    public GameObject HitEffect;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the speed
        Rb2d.velocity = new Vector2(XSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter2D(Collider2D collision) //If a bullet collides with a collider object or enemy, the bullet is destroyed
    {
        if (collision.gameObject.tag == "Collider")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
            Instantiate(HitEffect, newPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
