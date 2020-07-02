using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents; // importing ml agents
using Unity.MLAgents.Sensors;
//using System.Numerics;
using Random = UnityEngine.Random;

public class CubeAgent : Agent
{
    // public object reference
    public GameObject cAgent;
    public GameObject target;

    // reference collision detector

    colliderDetector cd;

    // parameter
    public float speed = 3f;

    // executes once at the beginning
    void Start()
    {
        cd = cAgent.GetComponent<colliderDetector>();
    }


    public override void OnEpisodeBegin()
    {
        cd.isCollided = false; // this resets the collision parameter to false

        // set agent's initial position
        cAgent.transform.localPosition = Vector3.zero;

        // set Random positon for the target
        target.transform.localPosition = new Vector3(Random.Range(-8.0f, 8.0f) * (Random.value <= 0.5 ? 1 : -1), 0, 0);

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(cAgent.transform.localPosition.x);  // we observe x position for the agent
        sensor.AddObservation(target.transform.localPosition.x);  // we 
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // we check the distance from target to object
        float finalDistance2Target = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);

        // we get a new X value for the agent
        float xNew = cAgent.transform.localPosition.x + (vectorAction[0] * speed * Time.deltaTime);

        // we assign the new x position to the agent 
        cAgent.transform.localPosition = new Vector3(xNew, 0, 0);

        // check again the new distance to our target
        float distance2target = Vector3.Distance(target.transform.localPosition, cAgent.transform.localPosition);

        // we are finally able to set the rewards
        if (distance2target < finalDistance2Target)
        {
            AddReward(0.1f);
        }
        else
        {
            AddReward(-0.1f);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (cd.isCollided == true)
        {
            AddReward(5f);  //reward +++
            EndEpisode();
        }

    }
}

