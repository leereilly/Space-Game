using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public bool CanBeKilled = true;

    private Animator _anim;

    private bool isNaked;

    private bool isGangster;

    private GameObject _base;
    
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

        foreach (GameObject x in GetComponentsInChildren<GameObject>())
        {
            if (x.name == "base") {
                _base = x;
            }
        }
        isNaked = false;
    }

    public void Update()
    {

    }

    public void LateUpdate()
    {
        if (isNaked)
        {
            SpriteRenderer[] renderers = GetComponents<SpriteRenderer>();
            switchSpriteSheet("Naked", renderers);
        }

        UpdateWeaponsAndArmorSprites();
    }

    private void UpdateWeaponsAndArmorSprites()
    {
        
    }

    public void toggleNakedness()
    {
        isNaked = !isNaked;
    }

    private void switchSpriteSheet(string spriteName, SpriteRenderer[] rendererList)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Characters/Players/" + spriteName);

        foreach (SpriteRenderer renderer in rendererList)
        {
            Sprite newSprite = Array.Find(sprites, i => i.name == renderer.sprite.name);

            if (newSprite)
            {
                renderer.sprite = newSprite;
            }
        }
    }
}
