using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private Transform _t;
    public int SpawnRadius;
    public GameObject[] WhatToSpawn;

    public Transform SpawnLocation;

    private List<GameObject> _whatToSpawn;

    private DateTime nextSpawn = DateTime.Now;

	// Use this for initialization
	void Start () {
        _t = GetComponent<Transform>();
        _whatToSpawn = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (DateTime.Now > nextSpawn && _whatToSpawn.Count < 10)
        {
            _whatToSpawn.Add(Instantiate(WhatToSpawn[0], SpawnLocation.position, Quaternion.Euler(0,0,0)) as GameObject);

            nextSpawn = DateTime.Now.AddSeconds(10);
        }
	}
}
