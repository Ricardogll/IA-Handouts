using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteeringSeparation : MonoBehaviour {

	public LayerMask mask;
	public float search_radius = 5.0f;
	public AnimationCurve falloff;
    public Vector3 aux = Vector3.zero;
	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 1: Agents much separate from each other:
        // 1- Find other agents in the vicinity (use a layer for all agents)
        // 2- For each of them calculate a escape vector using the AnimationCurve
        // 3- Sum up all vectors and trim down to maximum acceleration
        Collider[] hitColliders = Physics.OverlapSphere(move.transform.position, search_radius, mask);
        int i = 0;
        List<Vector3> repulsion = new List<Vector3>();
        Vector3 repulsion_vectors_sum = Vector3.zero;

        foreach (Collider col in hitColliders)
        {
            if (col.transform.root != transform)
            {
                Vector3 repulsion_vec = move.transform.position - col.transform.position;

                repulsion.Add(repulsion_vec);
                float dist = Vector3.Distance(move.transform.position, col.transform.position);

                float closeness = Mathf.Abs(dist / search_radius);

                repulsion[i].Normalize();
                repulsion[i] *= falloff.Evaluate(closeness);

                repulsion_vectors_sum += repulsion[i];
                i++;
                
            }
        }
        
        //repulsion_vectors_sum = Vector3.ClampMagnitude(repulsion_vectors_sum, move.max_mov_acceleration);
        aux = repulsion_vectors_sum;
        move.AccelerateMovement(repulsion_vectors_sum);

	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, search_radius);
	}
}
