﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI PauseScore;
    public TextMeshProUGUI GameOverScore;

    int PrevHull;


    private void Awake()
    {
        Data.Hull = Data.MaxHull;
        Data.Energy = Data.MaxEnergy;
        Data.Score = 0;
        Time.timeScale = 1f;

    }
    // Start is called before the first frame update
    void Start()
    {


        Hud.SetActive(true);
        Pause.SetActive(false);
        GameOver.SetActive(false);
        
        PrevHull = Data.Hull;

    }

    // Update is called once per frame
    void Update()
    {
        if(PrevHull > Data.Hull)
        {
            Hud.GetComponent<Image>().color = new Color(1f,0,0, Hud.GetComponent<Image>().color.a + .15f);
            PrevHull = Data.Hull;
        }else if(PrevHull < Data.Hull)
        {
            Hud.GetComponent<Image>().color = new Color(1f, 0, 0, Hud.GetComponent<Image>().color.a - .15f);
            PrevHull = Data.Hull;
        }

            if (Input.GetKeyDown(PauseKey))
        {
            if (IsPlaying)
            {
                Time.timeScale = 0f;
                IsPlaying = false;
                Hud.SetActive(false);
                Pause.SetActive(true);
                PauseScore.text = "HIGH SCORE: " + Data.MaxScore + "\nTHIS SCORE: " + Data.Score;
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
            GameOverScore.text = "HIGH SCORE: " + Data.MaxScore + "\nTHIS SCORE: " + Data.Score;
        }

        //HUD elements
        string hullstr = "";
        for(int i = 0; i < Data.Hull; ++i) {
            hullstr = hullstr + "/";
        }
        HullText.text = hullstr;

        ScoreText.text = Data.Score.ToString();
        if(Data.Score > Data.MaxScore)
        {
            Data.MaxScore = Data.Score;
        }

    }

    public void ButtonResume()
    {
        Time.timeScale = 1f;
                IsPlaying = true;
                Pause.SetActive(false);
                Hud.SetActive(true);
    }
    public void ButtonRestart()
    {
        SceneManager.LoadScene(1);

    }

    public void ButtonMainMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitOnClick()
    {

        Application.Quit();
    }

}