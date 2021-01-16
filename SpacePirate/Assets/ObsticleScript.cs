using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleScript : MonoBehaviour
{
    public float DestroyPosition = -20f;
    public GameObject GC;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController");
        Speed = GC.GetComponent<GameController>().GlobalSpeedMult;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * -1 * Speed;

        if(transform.position.z < DestroyPosition)
        {
            Destroy(gameObject);
        }
    }
}
