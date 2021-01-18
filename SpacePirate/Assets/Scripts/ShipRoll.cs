using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRoll : MonoBehaviour
{
    public float RollMult = 1;
    public float PitchMult = -50;

    Vector3 PrevPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.rotation = Quaternion.Euler(0f, 0f,( transform.position.x - PrevPos.x) * RollMult);
        PrevPos = transform.position;
    }
}
