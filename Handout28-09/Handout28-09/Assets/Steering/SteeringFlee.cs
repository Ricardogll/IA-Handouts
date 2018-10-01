﻿using UnityEngine;
using System.Collections;

public class SteeringFlee : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
        // TODO 2: Same as Steering seek but opposite direction
        Vector3 diff = move.target.transform.position - transform.position;
        diff.Normalize();
        move.AccelerateMovement(diff * (-move.max_mov_acceleration));
    }
}
