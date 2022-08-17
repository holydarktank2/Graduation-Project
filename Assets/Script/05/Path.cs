using UnityEngine;
using System.Collections;
 
public class Path : MonoBehaviour {
 
    //Display the path
    public bool showPath = true;
    public Color pathColor = Color.red;
 
    //The path is loop or not
    public bool loop = true;
 
    //The waypoint radius
    public float distance = 2.0f;
 
    //Waypoints array
    public Transform[] wayPoints;
 
    //The Reset fuction is one of Unity API.
    //MonoBehaviour.Reset()
    //http://docs.unity3d.com/ScriptReference/MonoBehaviour.Reset.html
    //This function is only called in editor mode.
    void Reset() {
 
        //Reset the wayPoint array
        wayPoints = new Transform[ GameObject.FindGameObjectsWithTag ("WayPoint").Length ];
 
        for( int cnt = 0; cnt < wayPoints.Length; cnt++ ) {
 
            wayPoints[cnt] = GameObject.Find( "WayPoint_" + (cnt + 1).ToString() ).transform;
 
        }
 
    }
 
    //Get the length of wayPoint array
    public float Length {
 
        get {
 
            return wayPoints.Length;
         
        }
     
    }
 
    //Get the position in the array with its index number
    public Vector3 GetPosition(int index) {
 
        return wayPoints[index].position;  
     
    }
 
 
    //The OnDrawGizmos function is one of Unity API
    //MonoBehaviour.OnDrawGizmos()
    //http://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDrawGizmos.html
    //This function will display gizmo in Scene and will not display in Game
    void OnDrawGizmos() {
 
        //If showPath is false, return
        if (!showPath) return;
 
        //Draw the path line
        for ( int i = 0; i < wayPoints.Length; i++ ) {
 
            if (i + 1 < wayPoints.Length) {
 
                Debug.DrawLine( wayPoints[i].position, wayPoints[i + 1].position, pathColor );
             
            }
            else {
 
                if( loop ) {
 
                    Debug.DrawLine( wayPoints[i].position, wayPoints[0].position, pathColor );
 
                }
 
            }
         
        }
     
    }
 
}