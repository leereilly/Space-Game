using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private Player _player;

    [SerializeField]
    public Transform StartPosition;

	// Use this for initialization
	void Start () {
        _player = GetComponent<Player>();

        SetupRigidBody();
    }

    private void SetupRigidBody()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        rigidBody.freezeRotation = true;
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

        _player.SetAnimatorBool("isWalking", v != Vector2.zero);

        if (v != Vector2.zero)
        {
            _player.SetAnimatorFloat("inputX", v.x);
            _player.SetAnimatorFloat("inputY", v.y);
        }

        rigidBody.MovePosition(rigidBody.position + v * Time.deltaTime);
	}
    
}
