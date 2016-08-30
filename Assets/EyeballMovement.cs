using System;
using UnityEngine;

public enum WalkingState
{
    None,
    Down,
    Up,
    Left,
    Right
}

public class EyeballMovement : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private Animator anim;

    private DateTime _nextMovement = new DateTime();

    private Vector2 _walkingDirection;

    private int _framesToWalk = 0;
    public int FramesPerWalk = 10;

    //private WalkingState _walkingState;

    /// <summary>
    /// Speed that this mofo moves
    /// </summary>
    public float MovementSpeed = 3;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        rigidBody.freezeRotation = true;
        anim = GetComponent<Animator>();
        _nextMovement = DateTime.Now.AddSeconds(-1);
    }

    private Vector2 getRandomVector2()
    {
        System.Random r = new System.Random(DateTime.Now.Millisecond);
        int rnd = r.Next(0, 100);
        int direction = rnd % 4;

        switch (direction)
        {
            case 0:
                Debug.Log("Moving Left");
                return new Vector2(-1, 0);
            case 1:
                Debug.Log("Moving Right");
                return new Vector2(1, 0);
            case 2:
                Debug.Log("Moving Down");
                return new Vector2(0, -1);
            case 3:
                Debug.Log("Moving Up");
                return new Vector2(0, 1);
        }
        return new Vector2(0, 0);
    }

    private WalkingState getMyWalkingState()
    {
        if (_framesToWalk > 0)
        {
            return getWalkingState();
        }

        return WalkingState.None;
    }

    private WalkingState getWalkingState()
    {
        return getWalkingState(_walkingDirection);
    }

    private WalkingState getWalkingState(Vector2 state)
    {
        WalkingState s = WalkingState.None;

        if (state == new Vector2(-1, 0))
        {
            s = WalkingState.Left;
        }
        else if (state == new Vector2(1, 0))
        {
            s = WalkingState.Right;
        }
        else if(state == new Vector2(0, -1))
        {
            s = WalkingState.Down;
        }
        else if(state == new Vector2(0, 1))
        {
            s = WalkingState.Up;
        }

        return s;
    }

    private Vector2 walkingStateToVector(WalkingState state)
    {
        switch (state)
        {
            case WalkingState.Down:
                return new Vector2(0, -1);
            case WalkingState.Up:
                return new Vector2(0, 1);
            case WalkingState.Left:
                return new Vector2(-1, 0);
            case WalkingState.Right:
                return new Vector2(1, 0);
        }

        return Vector2.zero;
    }

    // yo dawg
    private void flipMyState()
    {
        WalkingState myState = getMyWalkingState();
        if (myState == WalkingState.None) return;

        WalkingState newState = WalkingState.None;
        switch (myState)
        {
            case WalkingState.Down:
                newState = WalkingState.Up;
                break;
            case WalkingState.Up:
                newState = WalkingState.Down;
                break;
            case WalkingState.Left:
                newState = WalkingState.Right;
                break;
            case WalkingState.Right:
                newState = WalkingState.Left;
                break;
        }

        if (newState != WalkingState.None)
        {
            Debug.Log("Flipping to " + newState.ToString());
            changeMyWalkingState(newState);
        }
    }

    private void changeMyWalkingState(WalkingState state)
    {
        _walkingDirection = walkingStateToVector(state);
        ResetFramesToWalk();
    }
	
	// Update is called once per frame
	void Update ()
    {
        WalkingState currentState = getMyWalkingState();
        Vector2 v = walkingStateToVector(currentState);

        if (currentState == WalkingState.None)
        {
            // don't want to walk too often..
            if (DateTime.Now < _nextMovement)
            {
                anim.SetBool("iswalking", false);
                return;
            }

            v = getRandomVector2();
            currentState = getWalkingState(v);
            changeMyWalkingState(currentState);
        }

        if (v != Vector2.zero)
        {

            anim.SetBool("iswalking", true);
            anim.SetFloat("input_x", v.x);
            anim.SetFloat("input_y", v.y);
        } else
        {
            anim.SetBool("iswalking", false);
        }

        rigidBody.MovePosition(rigidBody.position + v * Time.deltaTime * MovementSpeed);
        
        _framesToWalk--;

        if (_framesToWalk < 1)
        {
            System.Random rnd = new System.Random(DateTime.Now.Millisecond);
            int msToAdd = rnd.Next(2000, 5000);
            Debug.LogFormat("Waiting {0} milliseconds", msToAdd);
            _nextMovement = DateTime.Now.AddMilliseconds(msToAdd);
        }
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        //_framesToWalk++; //
        flipMyState();
    }

    private void ResetFramesToWalk()
    {
        _framesToWalk = FramesPerWalk;
    }
}
