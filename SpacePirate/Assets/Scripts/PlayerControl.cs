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
    public GameObject ObsticleExplosion;
    public GameObject EnemyExplosion;
    public GameObject HealthExplosion;
    public GameObject CollisionExplosion;
    public GameObject GaussEffect;
    public GameObject Shield;

    public AudioClip[] SoundsArray;
    //0-Gauss, 1-Player Dmg, 2-Regular Health, 3-Overshield

    AudioSource PlayerAudio;

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
        Shield.SetActive(false);
        PlayerAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Movement
        float verticalAxis;
        float horizontalAxis;

        //verticalAxis = Input.GetAxis("Vertical");
        verticalAxis = 0f;
        horizontalAxis = Input.GetAxis("Horizontal");


        

        transform.position =  new Vector3(

            Mathf.Clamp(((horizontalAxis * StrafeSpeed * Time.deltaTime * xBorderMult) + transform.position.x),Left,Right) ,

            Mathf.Clamp(((verticalAxis * StrafeSpeed * Time.deltaTime * yBorderMult) + transform.position.y),Bottom, Top),

            0f);

        //End Movement

        //draw the laser
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





        //Shoot

        if (Data.Energy < Data.MaxEnergy)
        {
            Data.Energy += Data.RechargeRate;
        }
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1") && Data.Energy >= Data.MaxEnergy - 10f)
        {
            GaussVisualTimeCountdown = 0f;
            PlayerAudio.clip = SoundsArray[0];
            PlayerAudio.Play();
            Data.Energy = 0f;

            GameObject GaussEffectInstance = Instantiate(GaussEffect, transform);
            GaussEffectInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            GaussEffectInstance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);


            if (Physics.SphereCast(transform.position, ShotThickness, transform.forward, out hit, Data.Range))
            {


     

                if(hit.collider.gameObject.tag == "Obsticle")
                {
                    
                    GameObject CreatedExpl = Instantiate(ObsticleExplosion,hit.collider.gameObject.transform.position, transform.rotation);
                    CreatedExpl.transform.rotation = Quaternion.Euler(180, 0, 0);
                    Destroy(hit.collider.gameObject);
                    ++Data.Score;
                }

                else if(hit.collider.gameObject.tag == "Enemy")
                {
                    GameObject CreatedExpl = Instantiate(EnemyExplosion, hit.collider.gameObject.transform.position, transform.rotation);
                    CreatedExpl.transform.rotation = Quaternion.Euler(180, 0, 0);
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
            if(Data.Hull == Data.MaxHull)
            {
                Shield.SetActive(false);
            }
            Instantiate(CollisionExplosion, transform);
            Destroy(collision.gameObject);

            PlayerAudio.clip = SoundsArray[1];
            PlayerAudio.Play();

        }
       else if (collision.gameObject.tag == "Enemy")
        {
            --Data.Hull;
            if (Data.Hull == Data.MaxHull)
            {
                Shield.SetActive(false);
            }
            Instantiate(CollisionExplosion, transform);

            PlayerAudio.clip = SoundsArray[1];
            PlayerAudio.Play();


        }
        else if(collision.gameObject.tag == "Boost")
        {
            if(Data.Hull < Data.MaxHull)
            {
                ++Data.Hull;
                Instantiate(HealthExplosion, transform);
                Destroy(collision.gameObject);
                PlayerAudio.clip = SoundsArray[2];
                PlayerAudio.Play();

            }
            else if(Data.Hull  == Data.MaxHull)
            {
                ++Data.Hull;
                Shield.SetActive(true);
                PlayerAudio.clip = SoundsArray[3];
                PlayerAudio.Play();
            }
           
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("MaxScore",Data.MaxScore);
        PlayerPrefs.SetFloat("Volume",Data.Volume);
        if (Data.isFullscreen)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        
    }

}
