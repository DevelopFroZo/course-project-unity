using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider SFXSlider;
    public Slider MusicSlider;

    void Start()
    {
        SFXSlider.value = Parameters.GetSfxVolume();
        MusicSlider.value = Parameters.GetMusicVolume();
    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene( Parameters.GetCurrentLevel() );
    }

    public void ExitButtonClick()
    {
        Application.Quit();
        print( "Exit" );
    }

    public void ChangeSfxVolume( float volume )
    {
        Parameters.SetSfxVolume( volume );
    }

    public void ChangeMusicVolume( float volume )
    {
        Parameters.SetMusicVolume( volume );
    }
}
