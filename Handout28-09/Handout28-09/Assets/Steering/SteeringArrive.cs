using UnityEngine;
using System.Collections;

public class SteeringArrive : MonoBehaviour {

	public float min_distance = 0.1f;
	public float slow_distance = 5.0f;
	public float time_to_target = 0.1f;
    
    
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
		if(!move)
			move = GetComponent<Move>();

        // TODO 3: Create a vector to calculate our ideal velocity
        // then calculate the acceleration needed to match that velocity
        // before sending it to move.AccelerateMovement() clamp it to 
        // move.max_mov_acceleration

        Vector3 ideal_velocity = Vector3.Normalize(move.target.transform.position-transform.position);
        ideal_velocity *= move.max_mov_velocity;

        float dist = Vector3.Distance(transform.position, move.target.transform.position);

        if (dist <= slow_distance )
        {
            ideal_velocity = Vector3.Normalize(ideal_velocity) * move.max_mov_velocity * (dist / slow_distance);

        }

        Vector3 steering = ideal_velocity - move.movement;

        if (dist < min_distance)
            steering = -move.movement;

        move.movement = Vector3.ClampMagnitude(move.movement + steering, move.max_mov_velocity);



        
    }


    //float GetRequiredVelocityChange(float aFinalSpeed, float aDrag)
    //{
    //    float m = Mathf.Clamp01(aDrag * Time.fixedDeltaTime);
    //    return aFinalSpeed * m / (1 - m);
    //}

    void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, slow_distance);
	}
}
