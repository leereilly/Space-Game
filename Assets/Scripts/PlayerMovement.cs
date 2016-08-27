using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private Animator anim;
    private bool pointingLeft = false;

    [SerializeField]
    public Transform StartPosition;


	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rigidBody.transform.position = StartPosition.position;
    }

    /// <summary>
    /// Update() will determine the vector that the controller input is indicating the
    /// player should be moving, and try to move the player accordingly
    /// 
    /// Called every frame.
    /// </summary>
    void Update () {
        
        Vector2 v = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        anim.SetBool("isWalking", v != Vector2.zero);

        if (v != Vector2.zero)
        {
            anim.SetFloat("inputX", v.x);
            anim.SetFloat("inputY", v.y);
        }

        rigidBody.MovePosition(rigidBody.position + v * Time.deltaTime);
	}
}
