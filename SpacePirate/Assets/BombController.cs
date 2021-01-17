using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    GameObject GC;
    public float Speed;
    public GameObject ExplosionEffect;
    public float ExplosionDistance;

    public float DestroyPosition = -20f;

    bool isArmed = true;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController");
        Speed = GC.GetComponent<GameController>().GlobalSpeedMult;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (transform.position.z <= ExplosionDistance && isArmed){
            Instantiate(ExplosionEffect, transform);
            gameObject.tag = "Untagged";
            isArmed = false;
        }

        transform.position = transform.position + transform.forward * -1 * Speed;

        if (transform.position.z < DestroyPosition)
        {
            Destroy(gameObject);
        }
    }
}
