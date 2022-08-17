using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    private const float MaxSpeedAngle = -140;
    private const float ZeroSpeedAngle = 0;
    private const float MaxRevsAngle = -190;
    private const float ZeroRevsAngle = 0;
    private const float MaxSteerAngle = 450;
    private const float ZeroSteerAngle = 0;

    private Transform speedneedleTransform;
    private Transform revsneedleTransform;
    private Transform steeringwheelTransform;

    private float revs;
    private float MaxSpeed;
    private float Speed;
    private float steeringAngle;

    private CarController carController;

    private void Awake()
    {
        steeringwheelTransform = GameObject.Find("playerSteeringWheel").transform;
        speedneedleTransform = GameObject.Find("speedneedle").transform;
        revsneedleTransform = GameObject.Find("revsneedle").transform;
        carController = GetComponent<CarController>();

    }

    private void Update()
    {
        steeringAngle = carController.CurrentSteerAngle;
        steeringwheelTransform.localEulerAngles = new Vector3(19.558f, 0f, -GetSteeringWheelRotation(steeringAngle, 35f));
        


        revs = carController.Revs;
        revsneedleTransform.localEulerAngles = new Vector3(10.173f, 0.039f, GetRevsRotation(revs, 1f));

        Speed = carController.CurrentSpeed;
        MaxSpeed = carController.MaxSpeed;

        if (Speed > MaxSpeed)
        {
            Speed = MaxSpeed;
        }

        speedneedleTransform.localEulerAngles = new Vector3(10.173f, 0.039f, GetSpeedRotation(Speed, MaxSpeed));
    }



    private float GetSteeringWheelRotation(float steerangle, float MaxSteer)
    {
        float totalSteerAngleSize = ZeroSpeedAngle - MaxSteerAngle;
        float steerNormalized = steerangle / 35f;
       
        return ZeroSpeedAngle - steerNormalized * totalSteerAngleSize;
    }


    private float GetRevsRotation(float revs, float MaxRevs)
    {
        float totalRevsAngleSize = ZeroRevsAngle - MaxRevsAngle;
        float revsNormalized = revs / 1f;

        return ZeroRevsAngle - revsNormalized * totalRevsAngleSize;
    }


    private float GetSpeedRotation(float Speed, float MaxSpeed)
    {
        float totalAngleSize = ZeroSpeedAngle - MaxSpeedAngle;
        float speedNormalized = Speed / MaxSpeed;

        return ZeroSpeedAngle - speedNormalized * totalAngleSize;
    }
}
