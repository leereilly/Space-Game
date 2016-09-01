using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private Transform _t;
    public int SpawnRadius;
    public GameObject[] WhatToSpawn;
    public int MaxToSpawn = 10;

    public int TimeBetweenSpawns = 3;

    private List<GameObject> _whatToSpawn;

    private DateTime nextSpawn = DateTime.Now;

	// Use this for initialization
	void Start () {
        _t = GetComponent<Transform>();
        _whatToSpawn = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (DateTime.Now > nextSpawn && _whatToSpawn.Count < MaxToSpawn)
        {
            float x = (float)UnityEngine.Random.Range(-SpawnRadius*1000, SpawnRadius*1000)/1000 ;
            float y = (float)UnityEngine.Random.Range(-SpawnRadius * 1000, SpawnRadius * 1000) / 1000;
            _whatToSpawn.Add(Instantiate(WhatToSpawn[0], _t.position + new Vector3(x, y, 0), Quaternion.Euler(0,0,0)) as GameObject);

            nextSpawn = DateTime.Now.AddSeconds(TimeBetweenSpawns);
        }
	}
}
