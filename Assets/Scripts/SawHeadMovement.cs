using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHeadMovement : MonoBehaviour
{
    public float speed = 2f;
    public GameObject[] waypoints;
    private int currentWPIndex = 0;
    

    private void Update() {
        if (waypoints.Length > 0){
            if (Vector2.Distance(waypoints[currentWPIndex].transform.position, transform.position) < .1f){
                currentWPIndex++;
                if (currentWPIndex >= waypoints.Length){
                    currentWPIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWPIndex].transform.position, Time.deltaTime * speed);
        }
    }

}
