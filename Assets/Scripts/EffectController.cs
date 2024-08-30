using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public float Timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() //Once the timer is up, delete the effect
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
