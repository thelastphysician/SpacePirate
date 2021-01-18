using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float StrafeSpeed;
    public float ForwardSpeed;
    public float StrafeVariance;
    public float ResetDistance;
    public float ResetStartPlace;
    public float ResetSpeed;
    public float FireTime;
    public float FireRate;
    public SkinnedMeshRenderer BlendKey;

    public GameObject Explosion;

    public GameObject Bomb;

    float StrafeResetTime;
    float FireResetTime;

    float Bounds = 20f;

    enum States {MoveRight,MoveLeft, Fireing, Resetting };

    GameObject GC;
    public float Speed;

    States CurrentState;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController");
        Speed = GC.GetComponent<GameController>().GlobalSpeedMult;

        StrafeVariance = StrafeVariance / Speed;

        StrafeResetTime = StrafeVariance;
        FireResetTime = FireTime;

 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z < ResetDistance)
        {
            CurrentState = States.Resetting;
        }

        switch (CurrentState)
        {
            case States.MoveRight:
                transform.position = new Vector3(transform.position.x + StrafeSpeed * Speed,transform.position.y, transform.position.z - ForwardSpeed * Speed);
                --StrafeResetTime;
                if(StrafeResetTime <= 0f)
                {
                    StrafeResetTime = StrafeVariance;
                    CurrentState = States.Fireing;
                    //Instantiate(Explosion, transform);
                    BlendKey.SetBlendShapeWeight(0, 0);
                }
            
                break;
            
            case States.MoveLeft:
                transform.position = new Vector3(transform.position.x - StrafeSpeed * Speed, transform.position.y, transform.position.z - ForwardSpeed * Speed);
                --StrafeResetTime;
                if (StrafeResetTime <= 0f)
                {
                    StrafeResetTime = StrafeVariance;
                    CurrentState = States.MoveRight;
                }

                break;

            case States.Fireing:

                if(transform.position.z > ResetStartPlace) {
                    FireResetTime = FireTime;
                    CurrentState = States.MoveLeft;
                    BlendKey.SetBlendShapeWeight(0, 100);
                }
                --FireResetTime;
                if(FireResetTime <= 0f)
                {
                    Instantiate(Bomb, transform.position, transform.rotation);
                    FireResetTime = FireTime;
                    CurrentState = States.MoveLeft;
                    BlendKey.SetBlendShapeWeight(0, 100);
                }
                break;

            case States.Resetting:
                if(transform.position.z < ResetStartPlace)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ResetSpeed * Speed);
                }
                else
                {
                    CurrentState = States.Fireing;
                }


                break;

        }

        if(transform.position.x > Bounds)
        {
            transform.position = new Vector3(Bounds, transform.position.y, transform.position.z);
        }else if (transform.position.x < -1*Bounds)
        {
            transform.position = new Vector3(-1 *Bounds, transform.position.y, transform.position.z);
        }
    }
}
