using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float fuelCapacity = 200;

    public Transform tr;
    public Rocket rocket;

    void OnTriggerEnter( Collider collider ){
        if( rocket.getStringState() != "Playing" ) return;

        GameObject obj = collider.gameObject.transform.parent.gameObject;
        
        if( obj.tag == "Rocket" ){
            this.GetComponent<CapsuleCollider>().enabled = false;
            Destroy( this.gameObject );
            ( (Rocket) obj.GetComponent( typeof( Rocket ) ) ).addFuel( fuelCapacity );
        }
	}

    void Update()
    {
        tr.Rotate( new Vector3( 1, 1, 1 ) );
    }
}
