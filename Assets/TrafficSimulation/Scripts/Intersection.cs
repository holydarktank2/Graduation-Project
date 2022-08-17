// Traffic Simulation
// https://github.com/mchrbn/unity-traffic-simulation

using System.Collections.Generic;
using UnityEngine;

namespace TrafficSimulation{
    public enum IntersectionType{
        STOP,
        TRAFFIC_LIGHT
    }

    public class Intersection : MonoBehaviour
    {   
        public IntersectionType intersectionType;
        public int id;  

        //For stop only
        public List<Segment> prioritySegments;

        //For traffic lights only
        public float lightsDuration = 10;
        public float yellowLightDuration = 10;
        public int groupCount = 4;
        public List<Segment> lightsNbr1;
        public List<Segment> lightsNbr2;
        public List<Segment> lightsNbr3;
        public List<Segment> lightsNbr4;

        private List<GameObject> vehiclesQueue;
        private List<GameObject> PriorityLeftQueue;
        private List<GameObject> PriorityRightQueue;
        private List<GameObject> PriorityQueue;
        private List<GameObject> PriorityStraightQueue;
        private List<GameObject> TurningCar;
        private List<GameObject> vehiclesInIntersection;
        private List<GameObject> nonvehiclesInIntersection;
        private TrafficSystem trafficSystem;

        //public static bool FullIntersection = false;

        [HideInInspector] public int currentGreenLightsGroup = 1;

        void Start(){
            vehiclesQueue = new List<GameObject>();
            TurningCar = new List<GameObject>();
            PriorityLeftQueue = new List<GameObject>();
            PriorityRightQueue = new List<GameObject>();
            PriorityQueue = new List<GameObject>();
            PriorityStraightQueue = new List<GameObject>();
            vehiclesInIntersection = new List<GameObject>();
            nonvehiclesInIntersection = new List<GameObject>();
            if (intersectionType == IntersectionType.TRAFFIC_LIGHT)
                InvokeRepeating("SwitchLights", lightsDuration, lightsDuration);
        }

        void SwitchLights(){
            currentGreenLightsGroup = currentGreenLightsGroup % groupCount + 1;
            
            //Wait few seconds after light transition before making the other car move (= orange light)
            Invoke("MoveVehiclesQueue", yellowLightDuration);
        }

        void OnTriggerEnter(Collider _other) {
            //Check if vehicle is already in the list if yes abort
            //Also abort if we just started the scene (if vehicles inside colliders at start)
            if (IsAlreadyInIntersection(_other.gameObject) || Time.timeSinceLevelLoad < .5f) return;

            if (_other.tag == "AutonomousVehicle" && intersectionType == IntersectionType.STOP)
                TriggerStop(_other.gameObject);
            else if (_other.tag == "AutonomousVehicle" && intersectionType == IntersectionType.TRAFFIC_LIGHT) 
                TriggerLight(_other.gameObject);
        }

        void OnTriggerStay(Collider _other)
        {
            if (IsAlreadyInIntersection(_other.gameObject) == true && vehiclesInIntersection.Count >= 1)
            {
                StayStop(_other.gameObject);
                
            }
        }

        /*void Update()//GameObject _vehicle
        {
           
            Debug.Log("left"+PriorityLeftQueue.Count);
            Debug.Log("right"+PriorityRightQueue.Count);
            Debug.Log("straight"+PriorityStraightQueue.Count);
            Debug.Log("turing" + TurningCar.Count);
            Debug.Log("intersectiom " + vehiclesInIntersection.Count);

            Debug.Log("nonintersectiom " + nonvehiclesInIntersection.Count);
            //Debug.Log(vehicleSegment);
        }*/

        void OnTriggerExit(Collider _other) {
            if (_other.tag == "AutonomousVehicle" && intersectionType == IntersectionType.STOP)
                ExitStop(_other.gameObject);
            else if(_other.tag == "AutonomousVehicle" && intersectionType == IntersectionType.TRAFFIC_LIGHT)
                ExitLight(_other.gameObject);
        }

        void TriggerStop(GameObject _vehicle){
            VehicleAI vehicleAI = _vehicle.GetComponent<VehicleAI>();

            //Depending on the waypoint threshold, the car can be either on the target segment or on the past segment
            int vehicleSegment = vehicleAI.GetSegmentVehicleIsIn();

            if(!IsPrioritySegment(vehicleSegment)){
                if(vehiclesQueue.Count > 0 || vehiclesInIntersection.Count > 0){
                    vehicleAI.vehicleStatus = Status.STOP;
                    vehiclesQueue.Add(_vehicle);
                }
                else{
                    vehiclesInIntersection.Add(_vehicle);
                    nonvehiclesInIntersection.Add(_vehicle);
                    TurningCar.Add(_vehicle);                   
                    vehicleAI.vehicleStatus = Status.SLOW_DOWN;

                }
            }
            else{
                if (!PriorityQueue.Contains(_vehicle))
                {
                        PriorityQueue.Add(_vehicle);
                }

                if (TurningCar.Count > 0 || nonvehiclesInIntersection.Count > 0)//&& PriorityvehiclesQueue.Count>=0 && PriorityvehiclesQueue.Count <= 2 FullIntersection == true && vehiclesInIntersection.Count > 0 &&
                {
                    //Debug.Log("ONaaa");
                    vehicleAI.vehicleStatus = Status.STOP;
                }
                else if(TurningCar.Count == 0 && nonvehiclesInIntersection.Count == 0)
                {
                    vehicleAI.vehicleStatus = Status.SLOW_DOWN;
                    vehiclesInIntersection.Add(_vehicle);
                 }
                            
            }
        }

        void StayStop(GameObject _vehicle)
        {
            
            VehicleAI vehicleAI = _vehicle.GetComponent<VehicleAI>();
            int vehicleSegment = vehicleAI.GetSegmentVehicleIsIn();

            if (IsPrioritySegment(vehicleSegment))
            {
                
                if (vehicleAI.AILightLeft == false && vehicleAI.AILightRight == false && TurningCar.Count == 0 && nonvehiclesInIntersection.Count == 0)//直走先行
                {
                    vehicleAI.vehicleStatus = Status.SLOW_DOWN;
                    if (!PriorityStraightQueue.Contains(_vehicle))
                    {
                        PriorityStraightQueue.Add(_vehicle);
                    }

                    PriorityLeftQueue.Remove(_vehicle);
                    PriorityRightQueue.Remove(_vehicle);

                   // Debug.Log(vehicleAI + " turn1");
                }
                else if (vehicleAI.AILightRight == true && PriorityLeftQueue.Count > 0)//在路口如果自己要右轉，要讓左轉車先走
                {
                    vehicleAI.vehicleStatus = Status.STOP;
                    PriorityLeftQueue[0].GetComponent<VehicleAI>().vehicleStatus = Status.GO;
                    if (!PriorityRightQueue.Contains(_vehicle))
                    {
                        PriorityRightQueue.Add(_vehicle);
                    }

                   // Debug.Log(vehicleAI + " turn2");
                }

                else if (vehicleAI.AILightLeft == true && PriorityLeftQueue.Count == 2)
                {
                    PriorityLeftQueue[0].GetComponent<VehicleAI>().vehicleStatus = Status.GO;
                    vehicleAI.vehicleStatus = Status.GO;

                    //Debug.Log(vehicleAI + " turn3");
                }

                else if (vehicleAI.AILightLeft == true && PriorityStraightQueue.Count > 1 && PriorityRightQueue.Count == 0)//在路口如果有車子要左轉，要讓直行車先走&& vehiclesInIntersection.Count < 3
                {
                    vehicleAI.vehicleStatus = Status.STOP;
                    if (!PriorityLeftQueue.Contains(_vehicle))
                    {
                        PriorityLeftQueue.Add(_vehicle);
                    }

                    //Debug.Log(vehicleAI + " turn6");
                }

                else if (vehicleAI.AILightLeft == true)//記錄左轉
                {
                    //vehicleAI.vehicleStatus = Status.STOP;

                    PriorityStraightQueue.Remove(_vehicle);

                    if (!PriorityLeftQueue.Contains(_vehicle))
                    {
                        PriorityLeftQueue.Add(_vehicle);
                    }
                    if (!TurningCar.Contains(_vehicle))
                    {
                        TurningCar.Add(_vehicle);
                    }


                    //Debug.Log(vehicleAI + " turn5");
                }

                else if (vehicleAI.AILightRight == true)//記錄右轉
                {
                    //vehicleAI.vehicleStatus = Status.STOP;

                    PriorityStraightQueue.Remove(_vehicle);

                    if (!PriorityRightQueue.Contains(_vehicle))
                    {
                        PriorityRightQueue.Add(_vehicle);
                    }
                    if (!TurningCar.Contains(_vehicle))
                    {
                        TurningCar.Add(_vehicle);
                    }

                    //Debug.Log(vehicleAI + " turn4");
                }

            }
        }

        void ExitStop(GameObject _vehicle){
            _vehicle.GetComponent<VehicleAI>().vehicleStatus = Status.GO;
            vehiclesInIntersection.Remove(_vehicle);
            nonvehiclesInIntersection.Remove(_vehicle);
            //FullIntersection = false;
            
            vehiclesQueue.Remove(_vehicle);
            PriorityLeftQueue.Remove(_vehicle);
            PriorityRightQueue.Remove(_vehicle);
            PriorityQueue.Remove(_vehicle);
            PriorityStraightQueue.Remove(_vehicle);
            TurningCar.Remove(_vehicle);

            if (vehiclesQueue.Count > 0 && vehiclesInIntersection.Count == 0 && PriorityQueue.Count == 0){
                vehiclesQueue[0].GetComponent<VehicleAI>().vehicleStatus = Status.GO;
                nonvehiclesInIntersection.Add(vehiclesQueue[0]);
                //FullIntersection = true;
            }
            else if (PriorityQueue.Count != 0)
            {
                for(int i=0; i<PriorityQueue.Count; i++)
                {
                    PriorityQueue[i].GetComponent<VehicleAI>().vehicleStatus = Status.GO;
                }
            }
            /*else if (PriorityvehiclesQueue.Count != 0)
            {
                for (int i = 0; i < PriorityvehiclesQueue.Count; i++)
                {
                    PriorityvehiclesQueue[i].GetComponent<VehicleAI>().vehicleStatus = Status.GO;
                }
            }*/

        }

        void TriggerLight(GameObject _vehicle){
            VehicleAI vehicleAI = _vehicle.GetComponent<VehicleAI>();
            int vehicleSegment = vehicleAI.GetSegmentVehicleIsIn();

            if(isGreenLightSegment(vehicleSegment))
            {
                vehicleAI.vehicleStatus = Status.GO;
            }
            else{
                vehicleAI.vehicleStatus = Status.STOP;
                vehiclesQueue.Add(_vehicle);
            }
        }

        void ExitLight(GameObject _vehicle){
            _vehicle.GetComponent<VehicleAI>().vehicleStatus = Status.GO;
        }

        bool isGreenLightSegment(int _vehicleSegment)
        {
            if (currentGreenLightsGroup == 1)
            {
                foreach (Segment segment in lightsNbr1)
                {
                    if (segment.id == _vehicleSegment)
                        return true;
                }
            }
            else if (currentGreenLightsGroup == 2)
            {
                foreach (Segment segment in lightsNbr2)
                {
                    if (segment.id == _vehicleSegment)
                        return true;
                }
            }
            else if (currentGreenLightsGroup == 3)
            {
                foreach (Segment segment in lightsNbr3)
                {
                    if (segment.id == _vehicleSegment)
                        return true;
                }
            }
            else if (currentGreenLightsGroup == 4)
            {
                foreach (Segment segment in lightsNbr4)
                {
                    if (segment.id == _vehicleSegment)
                        return true;
                }
            }
            else
                return false;
            return false;
        }

        void MoveVehiclesQueue(){
            //Move all vehicles in queue
            List<GameObject> nVehiclesQueue = new List<GameObject>(vehiclesQueue);
            foreach(GameObject vehicle in vehiclesQueue){
                int vehicleSegment = vehicle.GetComponent<VehicleAI>().GetSegmentVehicleIsIn();
                if(isGreenLightSegment(vehicleSegment)){
                    vehicle.GetComponent<VehicleAI>().vehicleStatus = Status.GO;
                    nVehiclesQueue.Remove(vehicle);
                }
            }
            vehiclesQueue = nVehiclesQueue;
        }

        bool IsPrioritySegment(int _vehicleSegment){
            foreach(Segment s in prioritySegments){
                if(_vehicleSegment == s.id)
                    return true;
            }
            return false;
        }

        bool IsAlreadyInIntersection(GameObject _target){
            foreach(GameObject vehicle in vehiclesInIntersection){
                if(vehicle.GetInstanceID() == _target.GetInstanceID()) return true;
            }
            foreach(GameObject vehicle in vehiclesQueue){
                if(vehicle.GetInstanceID() == _target.GetInstanceID()) return true;
            }

            return false;
        } 


        private List<GameObject> memVehiclesQueue = new List<GameObject>();
        private List<GameObject> memVehiclesInIntersection = new List<GameObject>();

        public void SaveIntersectionStatus(){
            memVehiclesQueue = vehiclesQueue;
            memVehiclesInIntersection = vehiclesInIntersection;
        }

        public void ResumeIntersectionStatus(){
            foreach(GameObject v in vehiclesInIntersection){
                foreach(GameObject v2 in memVehiclesInIntersection){
                    if(v.GetInstanceID() == v2.GetInstanceID()){
                        v.GetComponent<VehicleAI>().vehicleStatus = v2.GetComponent<VehicleAI>().vehicleStatus;
                        break;
                    }
                }
            }
            foreach(GameObject v in vehiclesQueue){
                foreach(GameObject v2 in memVehiclesQueue){
                    if(v.GetInstanceID() == v2.GetInstanceID()){
                        v.GetComponent<VehicleAI>().vehicleStatus = v2.GetComponent<VehicleAI>().vehicleStatus;
                        break;
                    }
                }
            }
        }
    }
}
