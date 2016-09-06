using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class CrazyPants : MonoBehaviour {
    
    public bool Active = false;

    private Color _nextColor = new Color(1, 0, 0);
    private char _reducing = 'r';
    private SpriteRenderer _renderer;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_renderer.enabled || !Active)
        {
            return;
        }

        _renderer.color = _nextColor;
        SetNextColor();
	}

    private void SetNextColor()
    {
        float r = _nextColor.r;
        float g = _nextColor.g;
        float b = _nextColor.b;

        switch (_reducing)
        {
            case 'r':
                r -= 0.01f;
                if (r < 0)
                {
                    r = 0;
                    _reducing = 'g';
                }
                g += 0.01f;
                if (g > 1)
                {
                    g = 1;
                }
                break;
            case 'g':
                g -= 0.01f;
                if (g < 0)
                {
                    g = 0;
                    _reducing = 'b';
                }
                b += 0.01f;
                if (b > 1)
                {
                    b = 1;
                }
                break;
            case 'b':
                b -= 0.01f;
                if (b < 0)
                {
                    b = 0;
                    _reducing = 'r';
                }
                r += 0.01f;
                if (r > 1)
                {
                    r = 1;
                }
                break;
        }
        _nextColor = new Color(r, g, b);
    }
}
