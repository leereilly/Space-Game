using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShipMovement : MonoBehaviour {

    public float speed = 10;

    public float turnforce = 4;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per physics tick
	void FixedUpdate () {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (Input.GetButton("SpaceshipAccelerate"))
        {
            rb.AddForce(transform.up * speed);
        }

        if (Input.GetButton("SpaceshipBrake"))
        {
            rb.AddForce(transform.up * -speed);
        }

        float turn = -Input.GetAxis("Horizontal"); //todo make SpaceshipTurn

        rb.AddTorque(turn * turnforce);
        
	}
}
