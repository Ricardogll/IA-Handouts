using UnityEngine;
using System.Collections;

public class SteeringVelocityMatching : MonoBehaviour {

	public float time_to_target = 0.25f;
    //public GameObject follow_this_object;
    Move move;
	Move target_move;
    
	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		target_move = move.target.GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target_move)
		{
            // TODO 5: First come up with your ideal velocity
            // then accelerate to it.
            Vector3 aux = target_move.movement - move.movement;
            
            //move.SetMovementVelocity(Vector3.forward);
            move.rotation = target_move.rotation;
            move.movement = target_move.movement;
		}
	}
}
