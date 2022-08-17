using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint05 : MonoBehaviour
{
   public Waypoint05 previousWaypoint;
   public Waypoint05 nextWaypoint;

   public float distance = 2.0f;

   [Range(0f, 5f)]
   public float width = 1f;

   public Vector3 GetPosition()
   {
       Vector3 minBound = transform.position + transform.right * width / 2f;
       Vector3 maxBound = transform.position - transform.right * width / 2f;

       //return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
       return transform.position;
   }
}
