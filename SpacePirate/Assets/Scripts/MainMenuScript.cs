using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject Options;
    public GameObject Info;
    public Slider VolumeSlider;
    public Toggle FullscreenToggle;
    public GameData Data;

    private void Awake()
    {

        if (!PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore",0);
        }
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", .3f);
        }
        if (!PlayerPrefs.HasKey("Fullscreen"))
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }

        Data.MaxScore = PlayerPrefs.GetInt("MaxScore");
        Data.Volume = PlayerPrefs.GetFloat("Volume");
        if(PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            Data.isFullscreen = true;
        }
        else
        {
            Data.isFullscreen = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Options.SetActive(false);
        Info.SetActive(false);
        MainMenu.SetActive(true);
        AudioListener.volume = Data.Volume;
        VolumeSlider.value = Data.Volume;

        FullscreenToggle.isOn = Data.isFullscreen;

        Screen.fullScreen = Data.isFullscreen;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOnClick() {

        SceneManager.LoadScene(1);
    }

    public void QuitOnClick()
    {

        Application.Quit();
    }

    public void MenuOnClick()
    {
        Options.SetActive(false);
        Info.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void OptionsOnClick()
    {  
        MainMenu.SetActive(false);
        Info.SetActive(false);
        Options.SetActive(true);
    }

    public void InfoOnClick()
    {
        MainMenu.SetActive(false);
        Info.SetActive(false);
        Info.SetActive(true);
    }

    public void VolumeSliderChange()
    {
        Data.Volume = VolumeSlider.value;
        AudioListener.volume = Data.Volume;
    }

    public void FullscreenToggleChange()
    {
        Data.isFullscreen = FullscreenToggle.isOn;

       Screen.fullScreen = Data.isFullscreen;
        
    }
}
