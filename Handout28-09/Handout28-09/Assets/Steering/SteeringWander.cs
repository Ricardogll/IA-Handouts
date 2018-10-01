using UnityEngine;
using System.Collections;

public class SteeringWander : MonoBehaviour {

	public float min_distance = 0.1f;
	public float time_to_target = 0.25f;
    public float circle_distance = 2.0f;
    public float angle_change = 10.0f;
    private float wander_angle = 0.0f;
	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        //Vector3 diff = move.target.transform.position - transform.position;

        //if (diff.magnitude < min_distance)
        //    return;

        //diff /= time_to_target;

        //move.AccelerateMovement(diff);




        Vector3 circle_center = move.movement.normalized * circle_distance;
        Vector3 displacement = Vector3.forward * circle_distance;
        wander_angle += (Random.value * angle_change) - (angle_change * 0.5f);
        displacement = Quaternion.AngleAxis(wander_angle, Vector3.up) * displacement;
        Vector3 wander_force = circle_center + displacement;
        wander_force = Vector3.ClampMagnitude(wander_force, move.max_mov_acceleration);
        move.AccelerateMovement(wander_force);

        if(move.movement == Vector3.zero)
        {
            move.movement = Vector3.forward;

        }

	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
	}
}
