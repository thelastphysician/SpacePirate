using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float StrafeSpeed;
    public float EdgeBuffer;
    public float Top = 20f;
    public float Bottom = 0f;
    public float Left = -20f;
    public float Right = 20f;

    public float xBorderMult = 1f;
    public float yBorderMult = 1f;

    GameObject GameController;
    public GameData Data;

    Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalAxis;
        float horizontalAxis;

        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");


        

        transform.position =  new Vector3(

            Mathf.Clamp(((horizontalAxis * StrafeSpeed * Time.deltaTime * xBorderMult) + transform.position.x),Left,Right) ,

            Mathf.Clamp(((verticalAxis * StrafeSpeed * Time.deltaTime * yBorderMult) + transform.position.y),Bottom, Top),

            0f);
        RB.velocity = new Vector3();

    }

}
