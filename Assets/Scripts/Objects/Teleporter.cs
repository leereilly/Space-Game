using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour, ITouchable {

    public Transform TeleportTarget;

    // Use this for initialization
    void Start () {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.transform.position = TeleportTarget.position;
    }
}
