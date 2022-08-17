using UnityEngine;
using System.Collections;
 
public class path2 : MonoBehaviour {
     
    public Waypoint05 path;//The path
 
    public float speed = 20.0f;//following speed
    public float mass = 5.0f;//this is for object mass for simulating the real car
 
    public Vector3 carposition;
    public bool ismooving = true;
    //public bool isLooping = true;//the car will loop or not
     
    private float curSpeed;//Actual speed of the car

    public Vector3 targetPosition;
 
    private Vector3 curVelocity; 
 
    void Start () {


        curVelocity = transform.forward;
     
    } 
     
    void Update () {
 
        //Unify the speed

        carposition = GameObject.Find("car").transform.position;

        curSpeed = speed * Time.deltaTime;
 
        targetPosition = path.GetPosition();
 
        //If reach the radius within the path then move to next point in the path
        if ( Vector3.Distance(transform.position, targetPosition) < path.distance ) {
 
            //Don't move the vehicle if path is finished
            path.previousWaypoint = path;
            path = path.nextWaypoint;
         
        }

        if(ismooving)
        {
            //Calculate the acceleration towards the path
            curVelocity += Accelerate( targetPosition );
 
            //Move the car according to the velocity
            transform.position += curVelocity;
            //Rotate the car towards the desired Velocity
            transform.rotation = Quaternion.LookRotation( curVelocity );
        }
        else
        {
            if(Vector3.Distance(transform.position, carposition) > 5)
            {
                ismooving = true;
                speed = 20;
            }
        }
     
    }
 
    //Steering algorithm to steer the vector towards the target
    public Vector3 Accelerate( Vector3 target ) {
 
        //Calculate the directional vector from the current position towards the target point
        Vector3 desiredVelocity = target - transform.position;
 
        //Normalise the desired Velocity
        desiredVelocity.Normalize();

        if(Vector3.Distance(transform.position, carposition) < 5)
        {
            speed -= 10;
            if(speed <= 0)
            {
                ismooving = false;
            }
        }
 
        desiredVelocity = desiredVelocity * curSpeed;
 
        //Calculate the force Vector
        Vector3 steeringForce = desiredVelocity - curVelocity;
        Vector3 acceleration = steeringForce / mass;

        
        return acceleration;
     
    } 
 
}