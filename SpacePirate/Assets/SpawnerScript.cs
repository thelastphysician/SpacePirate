using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] Obsicles;

    public float frequency = 1f;
    public float varience = .5f;

    public float Top = 20f;
    public float Bottom = 0f;
    public float Left = -20f;
    public float Right = 20f;

    private float countdown;

    GameObject GC;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        countdown = frequency + Random.Range(frequency-varience, frequency + varience);
        GC = GameObject.FindGameObjectWithTag("GameController");
        Speed = GC.GetComponent<GameController>().GlobalSpeedMult;

        frequency = frequency / Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(countdown <= 0f)
        {
            SpawnObjects();
            countdown = frequency + Random.Range(frequency - varience, frequency + varience);
        }

        --countdown;
    }

    void SpawnObjects()
    {
        GameObject newObs = Instantiate(Obsicles[Random.Range(0,Obsicles.Length)],transform);

        newObs.transform.position = new Vector3( Random.Range(transform.position.x - 15, transform.position.x + 15), transform.position.y, transform.position.z);
    }


}
