using UnityEngine;
using System.Collections;

public class SteeringEvade : MonoBehaviour
{

    public float max_prediction;

    Move move;
    SteeringFlee flee;
    Move target_move;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        flee = GetComponent<SteeringFlee>();
        target_move = move.target.GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position, move.target.GetComponent<Move>().movement);
    }

    public void Steer(Vector3 target, Vector3 velocity)
    {
        //TODO Homework
        float prediction_multiplier = Vector3.Distance(move.target.transform.position, move.transform.position) / max_prediction;
        Vector3 prediction_pos = target_move.transform.position + target_move.movement * prediction_multiplier;


        flee.Steer(prediction_pos);

        /*  float prediction_multiplier = Vector3.Distance(transform.position, target_move.transform.position) / move.max_mov_velocity;
        Vector3 prediction_pos = target_move.transform.position + target_move.movement * prediction_multiplier;


        GetComponent<SteeringArrive>().Steer(prediction_pos);
      */
    }
}
 