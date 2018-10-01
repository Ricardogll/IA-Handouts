using UnityEngine;
using System.Collections;

public class SteeringAlign : MonoBehaviour {

	public float min_angle = 0.01f;
	public float slow_angle = 0.1f;
	public float time_to_target = 0.1f;
  
    
	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 4: As with arrive, we first construct our ideal rotation
        // then accelerate to it. Use Mathf.DeltaAngle() to wrap around PI
        // Is the same as arrive but with angular velocities


        float target_degrees = Mathf.Atan2(move.movement.x, move.movement.z) * Mathf.Rad2Deg;
        float current_degrees = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float delta = CalcShortestRot(current_degrees, target_degrees);
        
        
        float ideal_angle = delta;//= Mathf.MoveTowardsAngle(current_degrees, target_degrees, move.max_rot_acceleration);

       // angle = Mathf.LerpAngle(target_degrees, current_degrees, Time.time);

        if (Mathf.Abs(delta) < slow_angle && Mathf.Abs(delta) > min_angle)
        {
            ideal_angle *= current_degrees/target_degrees;
        }
        else if (Mathf.Abs(delta) < min_angle)
        {
            ideal_angle =0;
            move.rotation = 0;
        }

        if (Mathf.Abs(ideal_angle) > move.max_rot_acceleration)
            Mathf.Clamp(ideal_angle, -move.max_rot_acceleration, move.max_rot_acceleration);


        move.AccelerateRotation(ideal_angle);

        /*float target_degrees = Mathf.Atan2(move.movement.x, move.movement.z) * Mathf.Rad2Deg;
        float current_degrees = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float delta = Mathf.DeltaAngle(target_degrees, current_degrees);

        if(Mathf.Abs(delta) < min_angle)
            move.SetRotationVelocity(0.0f);
        else
            move.SetRotationVelocity(-delta);*/
    }

    // If the return value is positive, then rotate to the left. Else,
    // rotate to the right.
    float CalcShortestRot(float from, float to)
    {
        // If from or to is a negative, we have to recalculate them.
        // For an example, if from = -45 then from(-45) + 360 = 315.
        if (from < 0)
        {
            from += 360;
        }

        if (to < 0)
        {
            to += 360;
        }

        // Do not rotate if from == to.
        if (from == to ||
           from == 0 && to == 360 ||
           from == 360 && to == 0)
        {
            return 0;
        }

        // Pre-calculate left and right.
        float left = (360 - from) + to;
        float right = from - to;
        // If from < to, re-calculate left and right.
        if (from < to)
        {
            if (to > 0)
            {
                left = to - from;
                right = (360 - to) + from;
            }
            else
            {
                left = (360 - to) + from;
                right = to - from;
            }
        }

        // Determine the shortest direction.
        return ((left <= right) ? left : (right * -1));
    }
}
