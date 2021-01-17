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
    public float ShotThickness = .5f;
    public GameObject Explosion;
    public GameObject CollisionExplosion;
    public GameObject GaussEffect;

    public float xBorderMult = 1f;
    public float yBorderMult = 1f;


    GameObject GameController;
    public GameData Data;

    LineRenderer Gauss;

    public float GaussVisualTime = 20f;
    public float GaussVisualTimeCountdown;

    // Start is called before the first frame update
    void Start()
    {
        Gauss = GetComponent<LineRenderer>();
        GaussVisualTimeCountdown = GaussVisualTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Movement
        float verticalAxis;
        float horizontalAxis;

        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");


        

        transform.position =  new Vector3(

            Mathf.Clamp(((horizontalAxis * StrafeSpeed * Time.deltaTime * xBorderMult) + transform.position.x),Left,Right) ,

            Mathf.Clamp(((verticalAxis * StrafeSpeed * Time.deltaTime * yBorderMult) + transform.position.y),Bottom, Top),

            0f);

        //End Movement

        //Shoot
        Gauss.SetPosition(0, transform.position);
        Gauss.SetPosition(1, transform.position + transform.forward * Data.Range);

        if (GaussVisualTimeCountdown < GaussVisualTime)
        {
            Gauss.startWidth = 9;
            Gauss.endWidth = 6;
            ++ GaussVisualTimeCountdown;
        }
        else
        {
            Gauss.startWidth = 2;
            Gauss.endWidth = 1;
        }
        //draw the laser

        if (GaussVisualTimeCountdown >= GaussVisualTime && Input.GetButtonDown("Fire1"))
        {


            GaussVisualTimeCountdown = 0f;

            GameObject GaussEffectInstance = Instantiate(GaussEffect, transform);
            GaussEffectInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            GaussEffectInstance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
        }
            RaycastHit hit;

        if (Physics.SphereCast(transform.position,ShotThickness, transform.forward, out hit, Data.Range))
        {

            

            if (GaussVisualTimeCountdown >= GaussVisualTime && Input.GetButtonDown("Fire1"))
            {


     

                if(hit.collider.gameObject.tag == "Obsticle")
                {
                    Instantiate(Explosion,hit.collider.gameObject.transform.position, Random.rotation);
                    Destroy(hit.collider.gameObject);
                    ++Data.Score;
                }

                if (hit.collider.gameObject.tag == "Enemy")
                {
                    Instantiate(Explosion, hit.collider.gameObject.transform.position, Random.rotation);
                    Destroy(hit.collider.gameObject);
                    Data.Score +=5;
                }
            }

        }
        
        

        //End Shoot

    }

    private void OnTriggerEnter(Collider collision)
    {

        if(collision.gameObject.tag == "Obsticle")
        {
            --Data.Hull;
            Instantiate(CollisionExplosion, transform);
     
        }
        else if(collision.gameObject.tag == "Boost")
        {
            ++Data.Hull;
        }
    }

}
