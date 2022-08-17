using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrafficSimulation;

public class CheckpointAnalyzed : MonoBehaviour
{
    public enum Checkpointtype
    {
        PASS,
        LEFTTURNSIGNAL,
        RIGHTTURNSIGNAL,
        REDLIGHT,
        GROUP,
        subPASS,
        subLEFTURNSIGNAL,
        subRIGHTTURNSIGNAL,
        subREDLIGHT
    }
    public Checkpointtype checkpointtype;
    public Intersection intersection;
    public int GreenLightGroup;
    [HideInInspector] public int subcount = 0;

    public bool hi()
    {
        switch (checkpointtype)
        {
            case Checkpointtype.PASS:
                return passType();

            case Checkpointtype.LEFTTURNSIGNAL:
                return leftturnsignalType();

            case Checkpointtype.RIGHTTURNSIGNAL:
                return rightturnsignalType();

            case Checkpointtype.REDLIGHT:
                return redlightType();

            case Checkpointtype.GROUP:
                return groupType();

            case Checkpointtype.subPASS:
                return subPassType();

            case Checkpointtype.subLEFTURNSIGNAL:
                return subLeftturnsignalType();

            case Checkpointtype.subRIGHTTURNSIGNAL:
                return subRightturnsignalType();

            case Checkpointtype.subREDLIGHT:
                return subRedlightType();
               
        }
        return false;
    }
    bool passType()
    {
        return true;
    }
    bool leftturnsignalType()
    {
        if (player.leftlightOn)
        {
            return true;
        }
        return false;
    }
    bool rightturnsignalType()
    {
        if (player.rightlightOn)
        {
            return true;
        }
        return false;
    }
    bool redlightType()
    {
        if (this.intersection.currentGreenLightsGroup == this.GreenLightGroup)
            return true;
        return false;
    }
    bool groupType()
    {
        if (subcount == this.transform.childCount)
            return true;
        return false;
    }
    bool subPassType()
    {
        CheckpointAnalyzed a = this.transform.parent.GetComponent<CheckpointAnalyzed>();
        a.subcount++;
        return false;
    }
    bool subLeftturnsignalType()
    {
        if (player.leftlightOn)
        {
            CheckpointAnalyzed a = this.transform.parent.GetComponent<CheckpointAnalyzed>();
            a.subcount++;
        }
        return false;
    }
    bool subRightturnsignalType()
    {
        if (player.rightlightOn)
        {
            CheckpointAnalyzed a = this.transform.parent.GetComponent<CheckpointAnalyzed>();
            a.subcount++;
        }
        return false;
    }
    bool subRedlightType()
    {
        if (this.intersection.currentGreenLightsGroup == this.GreenLightGroup)
        {
            CheckpointAnalyzed a = this.transform.parent.GetComponent<CheckpointAnalyzed>();
            a.subcount++;
        }
        return false;
    }

}
