using System.Collections;
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
    public float fuel = 250;

    private State state = State.Playing;
    public float rotationSpeed = 100f;
    public float flySpeed = 100f;

    public string getStringState(){
        return state.ToString();
    }

    public float getFuel(){
        return fuel;
    }

    public void addFuel( float fuel_ ){
        fuel += fuel_;
    }

    void Restart(){
        SceneManager.LoadScene( "Level 1" );
    }

    void Death(){
        state = State.End;
        engineOnParticles.Stop();
        assLight.enabled = false;
        rocketDeathParticles.Play();
        Invoke( "Restart", 2f );
    }

    void Done(){
        state = State.End;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        engineOnParticles.Stop();
        assLight.enabled = false;
        doneParticles.Play();
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

    void Update()
    {
        if( state == State.Playing ){
            RotationControl();
            EngineControl();
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
			
            // if( audioSource.isPlaying == false ){
            //     audioSource.PlayOneShot( flySound );
            // }

            assLight.enabled = true;
			engineOnParticles.Play();
            maybeDestroyFuel();
		}
		else{
		// 	audioSource.Pause();
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
}
