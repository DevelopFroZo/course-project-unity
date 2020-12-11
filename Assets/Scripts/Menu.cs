using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = Parameters.GetVolume();
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

    public void ChangeVolume( float volume )
    {
        Parameters.SetVolume( volume );
    }
}
