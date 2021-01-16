using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public KeyCode PauseKey;

    public float GlobalSpeedMult = 1f;

    public GameData Data;



    bool IsPlaying = true;

    public GameObject Hud;
    public GameObject Pause;
    public GameObject GameOver;

    public TextMeshProUGUI HullText;

    private void Awake()
    {
        Data.Hull = Data.MaxHull;
        Data.Energy = Data.MaxEnergy;
    }
    // Start is called before the first frame update
    void Start()
    {


        Hud.SetActive(true);
        Pause.SetActive(false);
        GameOver.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseKey))
        {
            if (IsPlaying)
            {
                Time.timeScale = 0f;
                IsPlaying = false;
                Hud.SetActive(false);
                Pause.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                IsPlaying = true;
                Pause.SetActive(false);
                Hud.SetActive(true);
            }
        }


        if(Data.Hull <= 0)
        {
            Time.timeScale = 0f;
            IsPlaying = false;
            Hud.SetActive(false);
            Pause.SetActive(false);
            GameOver.SetActive(true);
        }

        string hullstr = "";
        for(int i = 0; i < Data.Hull; ++i) {
            hullstr = hullstr + "/";
        }
        HullText.text = hullstr;
    }
}
