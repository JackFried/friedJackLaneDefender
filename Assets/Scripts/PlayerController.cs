using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput PlayerInputInstance;
    public Rigidbody2D Rb2d;
    public BulletController BC;
    public GameObject Bullet;
    public GameObject BulletSpawn;
    public GameObject Enemy;
    public GameObject ShootEffect;
    public GameManager GM;
    public float Speed;
    public float BulletSpeed;
    public float TimerMax;
    public AudioClip ShootSound;

    private float timer;
    private InputAction moveY;
    private InputAction shoot;
    private bool movingY;
    private float moveDirectionY;
    private bool shooting;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up the input system
        PlayerInputInstance = GetComponent<PlayerInput>();
        PlayerInputInstance.currentActionMap.Enable();

        moveY = PlayerInputInstance.currentActionMap.FindAction("MoveY"); //Setting the Y-axis moving controls (WS)
        shoot = PlayerInputInstance.currentActionMap.FindAction("Shoot");

        //Setting inputs
        moveY.started += MoveY_started;
        moveY.canceled += MoveY_canceled;

        shoot.started += Shoot_started;
        shoot.canceled += Shoot_canceled;



        //Setting up timer
        timer = 0;
    }


    private void Shoot_started(InputAction.CallbackContext obj) //Determines if the player should be shooting
    {
        shooting = true;
    }
    private void Shoot_canceled(InputAction.CallbackContext obj) //Determines if the player should not be shooting
    {
        shooting = false;
    }

    private void MoveY_started(InputAction.CallbackContext obj) //Determines if the player should be moving
    {
        movingY = true;
    }
    private void MoveY_canceled(InputAction.CallbackContext obj) //Determines if the player should not be moving
    {
        movingY = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (movingY == true) //Sets the Y-axis movement variable (determines positive, negative, or zero motion)
        {
            moveDirectionY = moveY.ReadValue<float>();
        }
        else
        {
            moveDirectionY = 0;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Rb2d.velocity = new Vector2(0, moveDirectionY * Speed);

        //If shooting is true and the cooldown is finished, fire a bullet
        if (shooting == true)
        {
            if (timer <= 0)
            {
                timer = TimerMax;
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        //Creates a bullet and effect at the bullet spawn location with the applied speed
        BC.XSpeed = BulletSpeed;
        Vector2 newPos = new Vector2(BulletSpawn.transform.position.x, BulletSpawn.transform.position.y);
        Instantiate(ShootEffect, newPos, Quaternion.identity);
        Instantiate(Bullet, newPos, Quaternion.identity);

        AudioSource.PlayClipAtPoint(ShootSound, BulletSpawn.transform.position);
    }

    public void OnDestroy()
    {
        //Resetting inputs
        moveY.started -= MoveY_started;
        moveY.canceled -= MoveY_canceled;

        shoot.started -= Shoot_started;
        shoot.canceled -= Shoot_canceled;
    }
}
