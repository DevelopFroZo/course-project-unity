using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{
    private static MusicManager instance = null;

    public static MusicManager Instance {
             get { return instance; }
    }

    void Awake() 
    {
        AudioSource selfAudioSource = GetComponent<AudioSource>();

        if (instance != null && instance != this) 
        {
            AudioSource instanceAudioSource = instance.GetComponent<AudioSource>();

            if(instanceAudioSource.clip != selfAudioSource.clip)
            {
                instanceAudioSource.clip = selfAudioSource.clip;
                instanceAudioSource.volume = selfAudioSource.volume;
                instanceAudioSource.Play();
            }

            Destroy(this.gameObject);
            return;
        } 
        instance = this;
        selfAudioSource.Play();
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetMusicVolume( float volume ){
        GetComponent<AudioSource>().volume = volume;
    }
}