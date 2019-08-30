using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBack : MonoBehaviour
{
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Quaternion initialRotation;
    [SerializeField] private Transform initialParent;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.localRotation;
        initialParent = transform.parent;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TooFarAway())
            RestorePosition();
    }

    private bool TooFarAway()
    {
        return Vector3.Distance(transform.position, initialPosition) > 0.5;
    }

    private void RestorePosition()
    {
        Debug.Log("Resetting position");
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        transform.parent = initialParent;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.Sleep();
    }
}