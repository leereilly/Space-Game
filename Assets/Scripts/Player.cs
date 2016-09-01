using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

    public bool CanBeKilled = true;

    private Animator _anim;

    private bool isNaked;

    private bool isGangster;
    
    public bool Kill()
    {
        if (!CanBeKilled)
        {
            return false;
        }
        if (_anim == null)
        {
            Start();
        }
        _anim.SetBool("isWalking", false);
        _anim.SetBool("isDead", true);
        return true;
    }

    public void Start()
    {
        _anim = GetComponent<PlayerMovement>().Animator;

        isNaked = false;
    }

    public void Update()
    {

    }

    public void LateUpdate()
    {
        if (isNaked)
        {
            switchSpriteSheet("Naked");
        }
    }

    public void toggleNakedness()
    {
        isNaked = !isNaked;
    }

    private void switchSpriteSheet(string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Characters/Players/" + spriteName);

        foreach (SpriteRenderer renderer in GetComponents<SpriteRenderer>())
        {
            Sprite newSprite = Array.Find(sprites, i => i.name == renderer.sprite.name);

            if (newSprite)
            {
                renderer.sprite = newSprite;
            }
        }
    }
}
