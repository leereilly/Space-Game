using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(SpriteRenderer))]
public class ClothHood : MonoBehaviour {

    private BoxCollider2D _collider;


	// Use this for initialization
	void Start () {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.gameObject.GetComponent<Player>();

        if (p != null)
        {
            p.SetPartSprite(PlayerPart.Head, "Items/Head/cloth_hood_male");
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
