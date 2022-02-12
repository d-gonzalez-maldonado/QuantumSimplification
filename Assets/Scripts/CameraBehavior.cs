using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float backgroundResolution = 5.35f;



    private Transform _backgroundTransform;
    // Start is called before the first frame update
    void Start()
    {
        //_backgroundTransform = GameObject.Find("Background").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 userInput = Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical");
		Vector3 new_pos = VectorFunctions.Floor(transform.position / backgroundResolution)*backgroundResolution;
        new_pos.z = 0f;
        //_backgroundTransform.position = new_pos;
        transform.Translate(userInput);
    }   
}
