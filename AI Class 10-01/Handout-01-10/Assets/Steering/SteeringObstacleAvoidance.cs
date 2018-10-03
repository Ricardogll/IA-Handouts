using UnityEngine;
using System.Collections;

public class SteeringObstacleAvoidance : MonoBehaviour {

	public LayerMask mask;
	public float avoid_distance = 5.0f;
    //public RayClass[] rays;
    public RayClass rays;
    public AnimationCurve falloff;
    public float speed_multiplier = 1.0f;
    Move move;
	SteeringSeek seek;
    public Vector3 aux;
	// Use this for initialization
	void Start () {
		move = GetComponent<Move>(); 
		seek = GetComponent<SteeringSeek>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Agents must avoid any collider in their way
        // 1- Create your own (serializable) class for rays and make a public array with it
        // 2- Calculate a quaternion with rotation based on movement vector
        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
        // 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)

        Quaternion rot = Quaternion.AngleAxis(move.rotation, move.movement);
        RaycastHit hit;


        //rays[0].direction = move.movement;
        //rays[0].origin = move.transform.position;
        //rays[0].length = avoid_distance;

        //if (Physics.Raycast(rays[0].origin, rays[0].direction, out hit, rays[0].length))
        //{

        //    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        //    {
        //        aux++;
        //        Debug.DrawLine(rays[0].origin, hit.transform.position, Color.red);
        //    }
        //}


        //***************************************
        rays.direction = move.movement;
        rays.origin = move.transform.position;
        rays.length = avoid_distance;

        if (Physics.Raycast(rays.origin, rays.direction, out hit, rays.length))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Buildings")) //Como hacer esto con la variable mask?
            {
                float dist = Vector3.Distance(rays.origin, hit.point);

                float closeness = Mathf.Abs(dist / avoid_distance);
                Debug.DrawLine(rays.origin, hit.transform.position, Color.red);
                Vector3 go_to;
                go_to = hit.point + hit.normal * move.movement.magnitude;
                go_to.Normalize();
                go_to *= falloff.Evaluate(closeness);
                seek.Steer(go_to*speed_multiplier);

                Debug.DrawLine(hit.point, go_to, Color.cyan);

                //move.AccelerateMovement(go_to);
                aux = hit.point;
            }
        }




        //Debug.DrawLine(move.transform.position, Vector3.zero, Color.red);
    }

	void OnDrawGizmosSelected() 
	{
		if(move && this.isActiveAndEnabled)
		{
			Gizmos.color = Color.red;
			float angle = Mathf.Atan2(move.movement.x, move.movement.z);
			Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

			// TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)

		}
	}
}
