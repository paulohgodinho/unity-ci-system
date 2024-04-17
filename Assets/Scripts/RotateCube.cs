using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class RotateCube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Vector3 rotation = new Vector3(20,50,0);
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.angularVelocity = rotation;
    }
}
