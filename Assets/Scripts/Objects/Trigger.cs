using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BoxCollider2D))]

public class Trigger : MonoBehaviour, ITouchable {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D collider)
    {
        
    }
}
