using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Animator))]
public class PlayerSprite : MonoBehaviour
{
    
    /// <summary>
    /// Name of the sprite. Must be found in Resources/Characters
    /// </summary>
    public string SpriteName;

    private Animator _animator;
    private SpriteRenderer _renderer;

    public bool Enabled
    {
        get
        {
            return !string.IsNullOrEmpty(SpriteName);
        }
    }

	// Use this for initialization
	void Start ()
    {
        InitPlayerSprite();
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (Enabled)
        {
            switchSpriteSheet();
        }
    }

    void Update()
    {
        _animator.enabled = Enabled;
        _renderer.enabled = Enabled;
    }

    #region public functions

    public Animator GetAnimator()
    {
        return _animator;
    }

    public SpriteRenderer GetRenderer()
    {
        return _renderer;
    }

    #endregion

    #region private functions

    private void InitPlayerSprite()
    {
        Animator a = GetMyAnimator();
        SpriteRenderer r = GetMyRenderer();

        if (a != null)
        {
            _animator = a;
        }

        if (r != null)
        {
            _renderer = r;
        }
    }

    private void switchSpriteSheet()
    {
        if (!Enabled)
        {
            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Characters/" + SpriteName);
        
        Sprite newSprite = Array.Find(sprites, i => i.name == _renderer.sprite.name);

        if (newSprite)
        {
            _renderer.sprite = newSprite;
        }
        else
        {
            _renderer.enabled = false;
        }
    }

    private Animator GetMyAnimator()
    {
        foreach (Animator item in GetComponents<Animator>())
        {
            if (item.name == name)
            {
                return item;
            }
        }

        return null;
    }

    private SpriteRenderer GetMyRenderer()
    {
        foreach (SpriteRenderer item in GetComponents<SpriteRenderer>())
        {
            if (item.name == name)
            {
                return item;
            }
        }

        return null;
    }
    #endregion
}
