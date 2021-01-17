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

    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController");
        Speed = GC.GetComponent<GameController>().GlobalSpeedMult;
        random = Random.Range(0f,1f);
        HoldTimeCountdown = HoldTime;
        HoldDistance += Random.Range(-5, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(transform.position.z > HoldDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed * -1);
        }
        else if (HoldTime /4 >= HoldTimeCountdown && HoldTimeCountdown > 0f)
        {
            --HoldTimeCountdown;
            BlendKey.SetBlendShapeWeight(0, 1 / (HoldTimeCountdown) * 100);
        }
        else if (HoldTimeCountdown <= 0f)
        {
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
