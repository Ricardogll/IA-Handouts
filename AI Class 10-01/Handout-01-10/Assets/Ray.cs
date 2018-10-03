using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RayClass : System.Object {

    public Vector3 origin = Vector3.zero;
    public Vector3 direction = Vector3.zero;
    public float length = 0.0f;

    //public RaycastHit Cast(Vector3 origin, Vector3 direction, float length)
    //{
    //    RaycastHit hit;

    //    Physics.Raycast(origin, direction,out hit, length);

    //    return
    //}

}
// TODO 2: Agents must avoid any collider in their way
// 1- Create your own (serializable) class for rays and make a public array with it
// 2- Calculate a quaternion with rotation based on movement vector
// 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
// 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)