using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    public enum ControlType {SteeringWheel, KeyBoard}
    public ControlType controlType ;
    //= ControlType.SteeringWheel
    public float BestLapTime{ get; private set;} = Mathf.Infinity;
    public float LastLapTime{ get; private set;} = 0;
    public float CurrentLapTime{ get; private set;} = 0;
    public int CurrentLap{ get; private set;} = 0;
    public navigationbar nav;

    private float lapTimerTimestamp;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private CarController carController;
    private AudioSource audioSource;
    

    public static List<int> leftovercheckpoint = new List<int>();
    public static List<int> passedcheckpoint = new List<int>();
    private int num = 0 ;
    private int sum = 0 ;

    public static int right=0;
    public static int square=0;
    public static int middle=0;
    public static int wrongway=0;
    public static int aicar = 0;

    private Renderer l_Renderer;
    private Renderer r_Renderer;
    private Renderer lA_Renderer;
    private Renderer rA_Renderer;
    public static bool leftlightOn=false;
    public static bool rightlightOn =false;
    bool leftTriggered=false;
    bool rightTriggered=false;
    bool AutoLightTrigger=false;

    private Transform VisionTransform;

    public Transform Middle_mirror;
    public Transform Right_mirror;
    public Transform Left_mirror;

    public static int a = -1;
  
    

    LogitechGSDK.LogiControllerPropertiesData properties;

    

    public float xAxes,GasInput,BreakInput,ClutchInput;

    public float handbrake=0;

    private void Start()
    {
        if (PlayerPrefs.GetInt("controlType") == 0)
        {
            controlType = ControlType.SteeringWheel;
            Debug.Log(LogitechGSDK.LogiSteeringInitialize(false));
        }
        else if (PlayerPrefs.GetInt("controlType") == 1)
        {
            controlType = ControlType.KeyBoard;
        }
        /*if (controlType == ControlType.SteeringWheel)
        {
            Debug.Log(LogitechGSDK.LogiSteeringInitialize(false));            
        }*/
            //leftovercheckpoint.Clear();

    }

    void Awake()
    {
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        carController = GetComponent<CarController>();
        audioSource =GetComponent<AudioSource>();
        //Debug.Log(checkpointCount);
    }
    
    void Update()
    {

        switch (controlType)
        {   
            case ControlType.KeyBoard:
                //print("KeyBoard");
                if(controlType == ControlType.KeyBoard)
                {
                    float h = CrossPlatformInputManager.GetAxis("Horizontal");
                    float v = CrossPlatformInputManager.GetAxis("Vertical");
                    float handbrake = CrossPlatformInputManager.GetAxis("Jump");
                    carController.Move(h, v, v, handbrake);
                }
                break;

            case ControlType.SteeringWheel:
                //print("Logitech G29");
                //break;
                LogitechGSDK.LogiPlaySpringForce(0, 0, 33, 28);
        
                CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;

                if (LogitechGSDK.LogiUpdate()&&LogitechGSDK.LogiIsConnected(0)) //方向盤訊號輸入設定
                {
                    LogitechGSDK.DIJOYSTATE2ENGINES rec;
                    rec = LogitechGSDK.LogiGetStateUnity(0);
                    
                    xAxes = rec.lX / 32768f; //-1 0 1

                    if(rec.lY > 0)
                    {
                        GasInput = 0;
                    }
                    else if (rec.lY < 0)
                    {
                        GasInput = rec.lY / -32768f;
                    }
                    
                    if(rec.lRz > 0)
                    {
                        BreakInput = 0;
                    }
                    else if (rec.lRz <0)
                    {
                        BreakInput = rec.lRz / -32768f;
                    }

                    if(rec.rglSlider[0] > 0)
                    {
                        ClutchInput = 0;
                    }
                    else if (rec.rglSlider[0] <0)
                    {
                        ClutchInput = rec.rglSlider[0] / -32768f;
                    } 
                    
                    
                    uint Direction = rec.rgdwPOV[0];
                    VisionTransform = GameObject.Find("Main Camera").transform;
                    switch (Direction)
                    {
                        case 0:
                            Vector3 middlerelativePos = Middle_mirror.position - VisionTransform.position;
                            VisionTransform.rotation = Quaternion.LookRotation(middlerelativePos);
                            break;
                        
                        case 9000:
                            Vector3 leftrelativePos = Right_mirror.position - VisionTransform.position;
                            VisionTransform.rotation = Quaternion.LookRotation(leftrelativePos);
                            break;

                        case 18000:
                            VisionTransform.localEulerAngles = new Vector3(0,0,0);
                            break;

                        case 27000:
                            Vector3 rightrelativePos = Left_mirror.position - VisionTransform.position;
                            VisionTransform.rotation = Quaternion.LookRotation(rightrelativePos);
                            break;

                        
                        default:
                            VisionTransform.localEulerAngles = new Vector3(0,0,0);
                            break;
                    }

                }
                else
                {
                    Debug.Log("No Steering Wheel Connected");
                }

                if(LogitechGSDK.LogiButtonIsPressed(0, 2)==true){//手剎車
                    handbrake = 1f;
                }
                else{
                    handbrake = 0f;
                }

                if(controlType == ControlType.SteeringWheel)//訊號傳給車輛
                {
                    carController.Move(xAxes, GasInput, -BreakInput, handbrake);
                }
                
                LogitechGSDK.LogiPlayLeds(0,carController.Revs, 0.01f, 1f);//方向盤超轉燈
                
                GameObject LeftLight = GameObject.Find("TurnSignalLeft");
                l_Renderer = LeftLight.GetComponent<Renderer>();

                GameObject LeftLightArrow = GameObject.Find("TurnSignalLeftArrow");
                lA_Renderer = LeftLightArrow.GetComponent<Renderer>();

                GameObject RightLight = GameObject.Find("TurnSignalRight");
                r_Renderer = RightLight.GetComponent<Renderer>();

                GameObject RightLightArrow = GameObject.Find("TurnSignalRightArrow");
                rA_Renderer = RightLightArrow.GetComponent<Renderer>();


                if (xAxes<=-0.2&&leftlightOn==true)
                {
                    AutoLightTrigger=true;
                }
                else if(xAxes>=0.2&&rightlightOn==true)
                {
                    AutoLightTrigger=true;
                }

                if(LogitechGSDK.LogiButtonTriggered(0, 5)==true&&leftlightOn==false)//左方向燈音效開啟
                {
                    leftTriggered=true;
                    audioSource.mute = !audioSource.mute;
                }
                else if(AutoLightTrigger==true&&xAxes>=-0.03&&leftlightOn==true)//左方向燈自動熄滅
                {
                    leftTriggered=false;
                    audioSource.mute = !audioSource.mute;
                    AutoLightTrigger=false;
                }
                else if(LogitechGSDK.LogiButtonTriggered(0, 5)==true&&leftlightOn==true)//左方向燈音效關閉
                {
                    leftTriggered=false;
                    audioSource.mute = !audioSource.mute;
                }

                if(LogitechGSDK.LogiButtonTriggered(0, 4)==true&&rightlightOn==false)//右方向燈音效開啟
                {
                    rightTriggered=true;
                    audioSource.mute = !audioSource.mute;
                }
                else if(AutoLightTrigger==true&&xAxes<=0.03&&rightlightOn==true)//右方向燈自動熄滅
                {
                    rightTriggered=false;
                    audioSource.mute = !audioSource.mute;
                    AutoLightTrigger=false;
                }
                else if(LogitechGSDK.LogiButtonTriggered(0, 4)==true&&rightlightOn==true)//右方向燈關閉
                {
                    rightTriggered=false;
                    audioSource.mute = !audioSource.mute;
                }


                if(leftTriggered==true)//左方向燈燈亮
                {
                    r_Renderer.material.DisableKeyword("_EMISSION");
                    l_Renderer.material.EnableKeyword("_EMISSION");
                    rA_Renderer.material.DisableKeyword("_EMISSION");
                    lA_Renderer.material.EnableKeyword("_EMISSION");
                    leftlightOn =true;
                    rightlightOn=false;       
                }
                else if(rightTriggered==true)//右方向燈燈亮
                {
                    l_Renderer.material.DisableKeyword("_EMISSION");
                    r_Renderer.material.EnableKeyword("_EMISSION");
                    lA_Renderer.material.DisableKeyword("_EMISSION");
                    rA_Renderer.material.EnableKeyword("_EMISSION");
                    leftlightOn =false;
                    rightlightOn=true;       
                }
                else//方向燈燈滅
                {
                    r_Renderer.material.DisableKeyword("_EMISSION");
                    l_Renderer.material.DisableKeyword("_EMISSION");
                    rA_Renderer.material.DisableKeyword("_EMISSION");
                    lA_Renderer.material.DisableKeyword("_EMISSION");
                    leftlightOn =false;
                    rightlightOn=false;     
                }
                
                if(leftlightOn==true)//左方向燈閃爍
                {
                    float floor = 0f;
                    float ceiling = 1f;
                    float emission = floor + Mathf.PingPong(Time.time*2f, ceiling - floor);
                    l_Renderer.material.SetColor("_EmissionColor",new Color(2f,2f,2f)*emission);
                    lA_Renderer.material.SetColor("_EmissionColor", new Color(2f, 2f, 2f) * emission);

                }
                if(rightlightOn==true)//右方向燈閃爍
                {
                    float floor = 0f;
                    float ceiling = 1f;
                    float emission = floor + Mathf.PingPong(Time.time*2f, ceiling - floor);
                    r_Renderer.material.SetColor("_EmissionColor",new Color(2f,2f,2f)*emission);
                    rA_Renderer.material.SetColor("_EmissionColor", new Color(2f, 2f, 2f) * emission);

                }
                break;                    
        }

    }
    
    void StartLap()//偵測到起點
    {
        //Debug.Log("StartLap!");
        leftovercheckpoint = new List<int>();
        //lastCheckpointPassed = 1;
        lapTimerTimestamp=Time.time;
    }

    void EndLap()//偵測到終點
    {
        LastLapTime = Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);  
        //Debug.Log("EndLap - Laptime was"+LastLapTime+"seconds"); 
        
        if(passedcheckpoint.Count != checkpointCount)
        {
            for(int i=1;i<=checkpointCount;i++)
            {   
                 num=0;
                 for(int j=0;j<passedcheckpoint.Count;j++)
                 {
                    if(i==passedcheckpoint[j])
                    {
                        num++;
                    }
                 }
                 if(num==0){
                     leftovercheckpoint.Add(i);
                     sum++;
                 }
                 
            }
        }
        //Debug.Log(passedcheckpoint.Count);
        if(leftovercheckpoint.Count==0){
            leftovercheckpoint.Clear();
        }
        passedcheckpoint.Clear();
        //passedcheckpoint = new List<int>();


    }

    void OnTriggerEnter(Collider collider)//辨識車輛碰到甚麼物件
    {

        if (collider.CompareTag("Touchable")&& collider.gameObject.layer == checkpointLayer)        //撞到遊戲關卡通關檢查點
        {
            if (collider.gameObject.name == "Start")
            {
                StartLap();
            }
            else if (collider.gameObject.name == "End")
            {
                EndLap();
            }
            else
            {
                CheckpointAnalyzed checka = collider.GetComponent<CheckpointAnalyzed>();
                if(checka.hi())
                {
                    a = int.Parse(collider.gameObject.name);
                    passedcheckpoint.Add(a);
                    nav.showNav(a);
                }             
            }
        }

        else if (collider.CompareTag("AutonomousVehicle")) //撞到車輛
        {
            float Speed = carController.CurrentSpeed;
            if (controlType == ControlType.SteeringWheel)
            {
                LogitechGSDK.LogiPlayFrontalCollisionForce(0,(int)(Speed*2));
                LogitechGSDK.LogiPlaySideCollisionForce(0,(int)(Speed*2));
            }
            

            right = 0;
            square = 0;
            middle = 0;
            wrongway = 0;
            aicar = 1;
        }

        else if (collider.CompareTag("Untouchable")) //撞到路肩
        {
            
            
            // Debug.Log(collider.name);
            if (collider.name == "CheckPoint Right")
            {
                float Speed = carController.CurrentSpeed;
                if (controlType == ControlType.SteeringWheel)
                {
                    LogitechGSDK.LogiPlayFrontalCollisionForce(0,(int)(Speed*2));
                }
                    
                right = 1;
                square = 0;
                middle = 0;
                wrongway = 0;
                aicar = 0;
            }
            else if (collider.name == "Square")
            {
                float Speed = carController.CurrentSpeed;
                if (controlType == ControlType.SteeringWheel)
                {
                    LogitechGSDK.LogiPlayFrontalCollisionForce(0,(int)(Speed*2));
                }
                
                right = 0;
                square = 1;
                middle = 0;
                wrongway = 0;
                aicar = 0;
            }
            else if (collider.name == "CheckPoint Middle")
            {
                float Speed = carController.CurrentSpeed;
                if (controlType == ControlType.SteeringWheel)
                {
                    LogitechGSDK.LogiPlayFrontalCollisionForce(0,(int)(Speed*2));
                }
               
                right = 0;
                square = 0;
                middle = 1;
                wrongway = 0;
                aicar = 0;
            }
            else if (collider.name == "wrongway")
            {
                right = 0;
                square = 0;
                middle = 0;
                wrongway = 1;
                aicar = 0;
            }
            
            // Debug.Log(right);
            // Debug.Log(left);
            // Debug.Log(middle);
        }
    }

    
}
