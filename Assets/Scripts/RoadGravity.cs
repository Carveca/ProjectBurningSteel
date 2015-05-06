﻿using UnityEngine;
using System.Collections.Generic;

public class RoadGravity : MonoBehaviour 
{
    // Objects that have entered this road piece's trigger. (Guess you could call it a "box of influence")
    private Dictionary<int, Transform> enteredObjects = new Dictionary<int,Transform>(); // Instance ID, Transform

    public float gravitationalAcceleration = 9.81f;
    public float gravityMultiplier = 5.0f;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ignore")
        {
            enteredObjects.Add(other.GetInstanceID(), other.transform);
            other.GetComponent<CheckGravity>().Plus();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Ignore")
        {
            other.GetComponent<CheckGravity>().Minus();
            enteredObjects.Remove(other.GetInstanceID());            
        }
    }

    void FixedUpdate()
    {
        GravityEffect();
    }

    void GravityEffect()
    {
        if (enteredObjects.Count > 0)
        {
            foreach (KeyValuePair<int, Transform> keyValuePair in enteredObjects)
            {
                // Calculate the acceleration / force
                Vector3 accel = -this.transform.up * gravitationalAcceleration * gravityMultiplier;

                // Apply the acceleration to the Transform objects rigidbody velocity
                keyValuePair.Value.GetComponent<VehicleMovement>().Gravity(accel * keyValuePair.Value.GetComponent<Rigidbody>().mass);
            }
        }
    }
}
