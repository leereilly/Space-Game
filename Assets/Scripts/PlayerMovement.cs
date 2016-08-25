using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private Animator anim;
    private bool pointingLeft = false;


	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 v = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        pointingLeft = v.x < 0;

        anim.SetBool("isWalking", v != Vector2.zero);

        if (pointingLeft != anim.GetBool("pointingLeft"))
        {
            anim.SetBool("pointingLeft", pointingLeft);
            rigidBody.transform.Rotate(0, 180, 0);
        }


        if (v != Vector2.zero)
        {
            anim.SetFloat("inputX", v.x);
            anim.SetFloat("inputY", v.y);
        }

        rigidBody.MovePosition(rigidBody.position + v * Time.deltaTime);
	}
}
