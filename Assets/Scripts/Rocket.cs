﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    enum State {
        Playing,
        End
    };

    public Rigidbody rb;
    
    public Light assLight;
    
    public ParticleSystem engineOnParticles;
    public ParticleSystem rocketDeathParticles;
    public ParticleSystem doneParticles;
    
    public AudioSource audioSource;
    public AudioClip flySound;
    public AudioClip deathSound;
    public AudioClip doneSound;

    public AudioSource fuelAudioSource;
    public AudioClip fuelSound;

    private State state = State.Playing;
    
    public float fuel = 250;
    public float rotationSpeed = 100f;
    public float flySpeed = 100f;

    public string getStringState(){
        return state.ToString();
    }

    public float getFuel()
    {
        return fuel;
    }

    public void addFuel( float fuel_ )
    {
        fuel += fuel_;
    }

    void Restart()
    {
        SceneManager.LoadScene( Parameters.GetCurrentLevel() );
    }

    void Death()
    {
        state = State.End;
        engineOnParticles.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot( deathSound );
        assLight.enabled = false;
        rocketDeathParticles.Play();
        Invoke( "Restart", 2f );
    }

    public void PlayAddFuelSound(){
        fuelAudioSource.PlayOneShot( fuelSound );        
    }


    void NextLevel()
    {
        SceneManager.LoadScene( Parameters.GetNextLevel() );
    }

    void Done()
    {
        state = State.End;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        engineOnParticles.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot( doneSound );
        assLight.enabled = false;
        doneParticles.Play();
        Invoke( "NextLevel", 2f );
    }

    void OnCollisionEnter( Collision collision )
    {
        if( state != State.Playing ) return;

        switch( collision.gameObject.tag ){
			case "Safety":
				Debug.Log( "OK" );
				break;
			case "Done":
				Done();
				break;
			default:
                Death();
				break;
		}
    }

    void Start()
    {
        audioSource.volume = Parameters.GetSfxVolume();
        Parameters.SetScene( SceneManager.GetActiveScene().name );
    }

    void Update()
    {
        if( state == State.Playing ){
            RotationControl();
            EngineControl();
            MenuControl();
        }
    }

    void maybeDestroyFuel()
    {
        Vector3 start = gameObject.transform.position;
        Vector3 dir = transform.TransformDirection( Vector3.down ) * 10;
        RaycastHit hit;

        if( Physics.Raycast( start, dir, out hit ) && hit.collider.gameObject.tag == "Fuel" ) 
        {
            Vector3 pos = gameObject.transform.position - hit.collider.gameObject.transform.position;

            if( pos.magnitude < 5 ){
                Destroy( hit.collider.gameObject );
            }
        }

        Vector3 forward = transform.TransformDirection(Vector3.down) * 1;
    }

    void EngineControl()
	{
        if( fuel < 0 ){
            Death();

            return;
        }

		if( Input.GetKey( KeyCode.Space ) ){
			fuel -= 0.1f;
			rb.AddRelativeForce( Vector3.up * flySpeed * Time.deltaTime );
			
            if( audioSource.isPlaying == false ){
                audioSource.PlayOneShot( flySound );
            }

            assLight.enabled = true;
			engineOnParticles.Play();
            maybeDestroyFuel();
		}
		else{
			audioSource.Pause();
			engineOnParticles.Stop();
            assLight.enabled = false;
		}
		
	}

    void RotationControl()
	{
		float rotationSpeed_ = rotationSpeed * Time.deltaTime;

		rb.freezeRotation = true;

		if( Input.GetKey( KeyCode.A ) ){
            transform.Rotate( Vector3.forward * rotationSpeed_ );
		}
		else if( Input.GetKey( KeyCode.D ) ){
			transform.Rotate( -Vector3.forward * rotationSpeed_ );
		}

		rb.freezeRotation = false;
	}

    void MenuControl()
    {
        if( Input.GetKey( KeyCode.Escape ) ){
            SceneManager.LoadScene( "Menu" );
        }
    }
}
