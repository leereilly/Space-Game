using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    private Camera cam;

	// Use this for initialization
	void Start () {

        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        cam.orthographicSize = (Screen.height / 100f) / 4f;

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.25f) + new Vector3(0, 0, -10);
        }
	
	}
}
