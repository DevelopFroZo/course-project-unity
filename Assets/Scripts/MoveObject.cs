using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Vector3 startPosition;

    public Vector3 moveTo;
    public float speed;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float progress = Mathf.PingPong( Time.time * speed, 1 );
		Vector3 offset =  moveTo * progress;

		transform.position = startPosition + offset;
    }
}
