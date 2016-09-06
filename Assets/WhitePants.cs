using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(SpriteRenderer))]
public class WhitePants : MonoBehaviour
{

    private BoxCollider2D _collider;


    // Use this for initialization
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!GetComponent<SpriteRenderer>().enabled)
        {
            return;
        }

        Player p = other.gameObject.GetComponent<Player>();

        if (p != null)
        {
            p.SetPartSprite(PlayerPart.Legs, "Players/legs/pants/male/white_pants_male");
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
