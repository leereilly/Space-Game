using UnityEngine;
using System.Collections;


public class EyeballMovement : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        rigidBody.freezeRotation = true;
        anim = GetComponent<Animator>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 v = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (v != Vector2.zero)
        {
            anim.SetBool("iswalking", true);
        } else
        {
            anim.SetBool("iswalking", false);
        }

        rigidBody.MovePosition(rigidBody.position + v * Time.deltaTime);
	}
}
