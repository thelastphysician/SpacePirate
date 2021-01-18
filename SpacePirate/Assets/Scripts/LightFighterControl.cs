using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFighterControl : MonoBehaviour
{

    public float HoldDistance = 20f;
    public float StrafeDistance = 20f;
    public float StradeSpeed = 4f;
    public float BoostMult = 3f;
    public float ShootTime = 100f;
    public float HoldTime = 100f;
    public float HoldTimeCountdown;
    public SkinnedMeshRenderer BlendKey;
    GameObject GC;
    float Speed;
    float random;
    TrailRenderer Trail;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController");
        Speed = GC.GetComponent<GameController>().GlobalSpeedMult;
        Trail = GetComponent<TrailRenderer>();
        random = Random.Range(0f,1f);
        HoldTimeCountdown = HoldTime;
        HoldDistance += Random.Range(-5, 10);
        Trail.time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(transform.position.z > HoldDistance && ! (HoldTimeCountdown < HoldTime))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed * -1);
        }
        else if (HoldTime /4 >= HoldTimeCountdown && HoldTimeCountdown > 0f)
        {
            --HoldTimeCountdown;
            Trail.time = 1f;
            BlendKey.SetBlendShapeWeight(0, Mathf.Min((1 / (HoldTimeCountdown) * 100),100));
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed/6);
        }
        else if (HoldTimeCountdown <= 0f)
        {
            transform.localScale = new Vector3(1, 1, 2);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed * -1 * BoostMult);
        }
        else
        {
            --HoldTimeCountdown;
            transform.position = new Vector3(transform.position.x + Mathf.Sin((Time.time + random) * StradeSpeed) * StrafeDistance, transform.position.y, transform.position.z);

        }

        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }


    }
}
