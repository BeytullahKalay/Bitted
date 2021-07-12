using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform pathHolder;

    public float speed;
    public float waitTime;


    private Checker checkerScript;

    //Vector3[] waypoints;
    GameObject[] waypoints;
    Transform[] waypointsPos;

    bool routineStopped;
    public bool playerDetectedByBullet;

    int targetWaypointIndex;

    Vector3 previousPos;

    private void Start()
    {
        playerDetectedByBullet = GetComponent<Checker>().playerDetectedByBullet;

        targetWaypointIndex = 1;

        checkerScript = GetComponent<Checker>();
        routineStopped = true;

        waypoints = new GameObject[pathHolder.childCount];

        
        waypointsPos = new Transform[pathHolder.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {

            waypoints[i] = pathHolder.GetChild(i).gameObject;
            waypointsPos[i] = waypoints[i].transform;
        }

        StartCoroutine(FollowPath(waypointsPos));
    }

    void Update()
    {
        if (!checkerScript.playerDetected && !checkerScript.playerDetectedByBullet && routineStopped)
        {
            StartCoroutine(FollowPath(waypointsPos));
        }

        //  THIS IS TEST
        print("patrol script working!!");

    }

    IEnumerator FollowPath( Transform[] waypointsPos)
    {

        if (previousPos == Vector3.zero)
        {
            transform.position = waypointsPos[0].position;
        }
        else
        {
            transform.position = previousPos;
        }


        Vector3 targetWaypoint = waypointsPos[targetWaypointIndex].position;

        while (!checkerScript.playerDetected && !playerDetectedByBullet)
        {
            routineStopped = false;

            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

            playerDetectedByBullet = GetComponent<Checker>().playerDetectedByBullet;

            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypointsPos[targetWaypointIndex].position;
                yield return new WaitForSeconds(waitTime);


            }
            yield return null;
        }

        if (playerDetectedByBullet)
        {
            print("bullet check true");
        }
        previousPos = transform.position;
        routineStopped = true;
        gameObject.GetComponent<Patrol>().enabled = false;


    }



    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

    }


}
