using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] WaypointSystem waypoints;
    [SerializeField] float move__speed = 5f;
    [SerializeField] float dis_threshold = 0.1f;
    public bool is__patrolling = true;
    Transform current__waypoint;
    // Start is called before the first frame update
    void Start()
    {
        if (waypoints == null)
        { return; }
        else
        {
            current__waypoint = waypoints.get__next__waypoint(current__waypoint);
            transform.position = current__waypoint.position;

            //set next waypoint
            current__waypoint = waypoints.get__next__waypoint(current__waypoint);
            transform.LookAt(current__waypoint);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (is__patrolling && waypoints != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, current__waypoint.position, move__speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, current__waypoint.position) < dis_threshold)
            {
                current__waypoint = waypoints.get__next__waypoint(current__waypoint);
                transform.LookAt(current__waypoint);

            }
        }


    }
}
