using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// Names must match up exactly with part group names under Player prefab
/// </summary>
public enum PlayerPart
{
    Base,
    Weapon,
    SecondaryWeapon,
    Head, // shoulders, knees, and toes.
    Chest,
    Legs, 
    Feet
}

[RequireComponent(typeof (PlayerMovement))]
public class Player : MonoBehaviour {

    private Dictionary<PlayerPart, PlayerSprite> _parts = new Dictionary<PlayerPart, PlayerSprite>();

    public void Start()
    {
        SetupPlayerParts();
    }

    private void SetupPlayerParts()
    {
        foreach (PlayerPart p in Enum.GetValues(typeof(PlayerPart)))
        {
            _parts.Add(p, FindPlayerSpriteByName(p.ToString()));
        }
    }

    public void Update()
    {

    }

    public void LateUpdate()
    {
    }

    public void SetPartSprite(PlayerPart part, string spriteName)
    {
        if (_parts.ContainsKey(part))
        {
            _parts[part].SpriteName = spriteName;
        }
        
    }

    public void SetAnimatorFloat(string name, float value)
    {
        foreach (PlayerSprite sprite in _parts.Values)
        {
            sprite.GetAnimator().SetFloat(name, value);
        }
    }

    public void SetAnimatorBool(string name, bool value)
    {
        foreach (PlayerSprite sprite in _parts.Values)
        {
            sprite.GetAnimator().SetBool(name, value);
        }
    }

    public PlayerSprite GetPlayerSprite(PlayerPart part)
    {
        if (_parts.ContainsKey(part))
        {
            return _parts[part];
        }
        return null;
    }

    public Animator GetAnimatorByPart(PlayerPart part)
    {
        PlayerSprite ps = GetPlayerSprite(part);

        if (ps != null)
        {
            return ps.GetAnimator();
        }

        return null;
    }

    public SpriteRenderer GetRendererByPart(PlayerPart part)
    {
        PlayerSprite ps = GetPlayerSprite(part);

        if (ps != null)
        {
            return ps.GetRenderer();
        }

        return null;
    }
    
    private PlayerSprite FindPlayerSpriteByName(string findName)
    {
        foreach (PlayerSprite item in GetComponentsInChildren<PlayerSprite>())
        {
            if (item.name == findName)
            {
                return item;
            }
        }

        return null;
    }
}
