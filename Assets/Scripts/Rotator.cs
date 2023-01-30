using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationDegreesPerSecond = 0;
    public float orbitDegreesPerSecond = 0;
    public GameObject orbitAroundObject;

    private float _orbitDistance;
    private Transform _t;
    private Transform _tTarget;
    private bool _hasTarget;
    
    void Start()
    {
        _hasTarget = !orbitAroundObject.Equals(null);
        
        _t = GetComponent<Transform>();
        
        if (_hasTarget) // Object isn't the Sun
        {
            _tTarget = orbitAroundObject.GetComponent<Transform>();
            SetOrbitDistance();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        if (_hasTarget)
            UpdateOrbit();
    }

    void SetOrbitDistance()
    {
        _orbitDistance = (_t.position - _tTarget.position).magnitude;
    }

    void UpdateOrbit()
    {
        _t.position = _tTarget.position + (_t.position - _tTarget.position).normalized * _orbitDistance;
        _t.RotateAround(_tTarget.position, Vector3.up, orbitDegreesPerSecond * Time.deltaTime); // Orbit
    }

    void UpdateRotation()
    {
        _t.Rotate(new Vector3(0f, rotationDegreesPerSecond * Time.deltaTime, 0f), Space.Self); // Rotation
    }
}
