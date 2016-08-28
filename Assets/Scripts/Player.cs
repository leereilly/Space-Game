using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

    public bool CanBeKilled = true;

    private Animator _anim;


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
    }

    public void Update()
    {

    }
}
