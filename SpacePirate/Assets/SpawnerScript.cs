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
    // Start is called before the first frame update
    void Start()
    {
        countdown = frequency + Random.Range(frequency-varience, frequency + varience);
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown <= 0f)
        {
            SpawnObjects();
            countdown = frequency;
        }

        --countdown;
    }

    void SpawnObjects()
    {
        GameObject newObs = Instantiate(Obsicles[Random.Range(0,Obsicles.Length-1)],transform);

        newObs.transform.position = new Vector3( Random.Range(transform.position.x - 15, transform.position.x + 15), transform.position.y, transform.position.z);
    }


}
