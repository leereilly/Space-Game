using UnityEngine;
using System.Collections;

public class RandomGem : MonoBehaviour {

    public Sprite[] Sprites;

	// Use this for initialization
	void Start () {
        if (Sprites.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
