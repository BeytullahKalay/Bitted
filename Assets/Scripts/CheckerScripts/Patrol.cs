using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform pathHolder;

    public float speed;
    public float waitTime;


    private Checker checkerScript;


    GameObject[] waypoints;
    Transform[] waypointsPos;

    public bool playerDetectedByBullet;

    int targetWaypointIndex;

    public Vector3 previousPos;

    bool stopMoving;



    private void Start()
    {

        targetWaypointIndex = 1;

        checkerScript = GetComponent<Checker>();


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

        if (checkerScript.playerDetected || checkerScript.playerDetectedByBullet)
        {
            //print("coroutine stopped");
            StopCoroutine(FollowPath(waypointsPos));
            stopMoving = true;
            transform.position = previousPos;
        }

    }

    IEnumerator FollowPath( Transform[] waypointsPos)
    {
        //print("coroutine started");

        if (previousPos == Vector3.zero)
        {
            transform.position = waypointsPos[0].position;
            GetComponent<Checker>()._startPos = waypointsPos[0].position;
        }
        else
        {
            SetStartPos();
        }


        Vector3 targetWaypoint = waypointsPos[targetWaypointIndex].position;

        while (!checkerScript.playerDetected && !playerDetectedByBullet)
        {
            if (!stopMoving)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                targetWaypoint.z = 0;

                transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            }

            SetStartPos();

            playerDetectedByBullet = GetComponent<Checker>().playerDetectedByBullet;


            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypointsPos[targetWaypointIndex].position;
                yield return new WaitForSeconds(waitTime);


            }
            yield return null;
        }

        SetStartPos();


    }


    public void StartFollowPathCoroutine()
    {
        //print("in Start Follow Path");
        stopMoving = false;
        StartCoroutine(FollowPath(waypointsPos));
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

    private void SetStartPos()
    {
        previousPos = transform.position;
        GetComponent<Checker>()._startPos = waypointsPos[0].position;
    }


}
