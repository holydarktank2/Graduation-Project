using UnityEngine;
using System.Collections;
 
public class PathFollowing : MonoBehaviour {
     
    public Path path;//The path
 
    public float speed = 20.0f;//following speed
    public float mass = 5.0f;//this is for object mass for simulating the real car
 
    public bool isLooping = true;//the car will loop or not
     
    private float curSpeed;//Actual speed of the car
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPosition;
 
    private Vector3 curVelocity; 
 
    void Start () {
 
        pathLength = path.Length;
 
        curPathIndex = 0;
 
        //get the current velocity of the vehicle
        curVelocity = transform.forward;
     
    } 
     
    void Update () {
 
        //Unify the speed
        curSpeed = speed * Time.deltaTime;
 
        targetPosition = path.GetPosition( curPathIndex );
 
        //If reach the radius within the path then move to next point in the path
        if ( Vector3.Distance(transform.position, targetPosition) < path.distance ) {
 
            //Don't move the vehicle if path is finished
            if ( curPathIndex < pathLength - 1 )
                curPathIndex++;
            else if ( isLooping )
                curPathIndex = 0;
            else
                return;
         
        }
 
        //Calculate the acceleration towards the path
        curVelocity += Accelerate( targetPosition );
 
        //Move the car according to the velocity
        transform.position += curVelocity;
        //Rotate the car towards the desired Velocity
        transform.rotation = Quaternion.LookRotation( curVelocity );
     
    }
 
    //Steering algorithm to steer the vector towards the target
    public Vector3 Accelerate( Vector3 target ) {
 
        //Calculate the directional vector from the current position towards the target point
        Vector3 desiredVelocity = target - transform.position;
 
        //Normalise the desired Velocity
        desiredVelocity.Normalize();
 
        desiredVelocity *= curSpeed;
 
        //Calculate the force Vector
        Vector3 steeringForce = desiredVelocity - curVelocity;
        Vector3 acceleration = steeringForce / mass;
 
        return acceleration;
     
    } 
 
}