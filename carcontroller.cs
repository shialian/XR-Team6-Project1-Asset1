using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HTC.UnityPlugin.Vive;
using UnityEngine.SceneManagement;
public class carcontroller : MonoBehaviour
{
    public bool carAccident;
    public bool getGoal;
    public HealthBarDrop fatherHealth;
    public AudioClip accident;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private Vector3 movingDirection;
    private float horizontalInput;
    private float verticalInput;
    private float leftInput;
    private float stop;
    private float currentsteerAngle;
    private float currentbreakforce;
    private bool isBreaking;
    private AudioSource accidentSource;
    [SerializeField] private float motorforce;
    [SerializeField] private float breakforce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private Transform RIGHT;
    [SerializeField] private Transform HEAD;
    [SerializeField] private WheelCollider FLwheelcollider;
    [SerializeField] private WheelCollider FRwheelcollider;
    [SerializeField] private WheelCollider RRwheelcollider;
    [SerializeField] private WheelCollider RLwheelcollider;
    public float rotateSpeedFactor = 10.0f;
    [SerializeField] private Transform FLwheeltransform;
    [SerializeField] private Transform FRwheeltransform;
    [SerializeField] private Transform RRwheeltransform;
    [SerializeField] private Transform RLwheeltransform;
    private Transform mainCamera;

    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag=="CarPrefab")
        {
            accidentSource.PlayOneShot(accident);
            carAccident = true;
        }
    }

    private void Awake()
    {
        accidentSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void Update()
    {

        GetInput();

    }

    private void FixedUpdate()
    {

        HandleMotor();
        HandleSteering();
        UpdateWheels();
        ApplyBreaking();
    }
    private void HandleMotor()
    {
        if (GetComponent<Rigidbody>().velocity.x * GetComponent<Rigidbody>().velocity.x + GetComponent<Rigidbody>().velocity.y * GetComponent<Rigidbody>().velocity.y + GetComponent<Rigidbody>().velocity.z * GetComponent<Rigidbody>().velocity.z > 4)
         verticalInput = 0;
        else {
            FLwheelcollider.motorTorque = verticalInput * motorforce;
            FRwheelcollider.motorTorque = verticalInput * motorforce;
        }
        currentbreakforce = breakforce;


        ApplyBreaking();

        //RRwheelcollider.motorTorque = verticalInput * motorforce;
        //RLwheelcollider.motorTorque = verticalInput * motorforce;
    }
    private void ApplyBreaking()
    {

        FLwheelcollider.brakeTorque = leftInput * currentbreakforce + stop* currentbreakforce;
        FRwheelcollider.brakeTorque = leftInput * currentbreakforce + stop * currentbreakforce;
        RLwheelcollider.brakeTorque = leftInput * currentbreakforce + stop * currentbreakforce;
        RRwheelcollider.brakeTorque = leftInput * currentbreakforce + stop * currentbreakforce;
    }
    private void HandleSteering()
    {
        currentsteerAngle = maxSteerAngle * horizontalInput;
        FLwheelcollider.steerAngle = currentsteerAngle;
        FRwheelcollider.steerAngle = currentsteerAngle;
    }
    private void GetInput()
    {
      
        horizontalInput = -(RIGHT.position.y - HEAD.position.y+0.25f )*1.2f;

        verticalInput = ViveInput.GetTriggerValue(HandRole.RightHand);
        //if (verticalInput == 0&&)
           // stop = 0.001f;
        leftInput = ViveInput.GetTriggerValue(HandRole.LeftHand);


    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(FLwheelcollider, FLwheeltransform);
        UpdateSingleWheel(FRwheelcollider, FRwheeltransform);
        UpdateSingleWheel(RLwheelcollider, RLwheeltransform);
        UpdateSingleWheel(RRwheelcollider, RRwheeltransform);
    }
    private void SetDirectionAndRotation()
    {
        if (verticalInput != 0 || horizontalInput != 0)
        {
            float playerYaw = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            float cameraYaw = mainCamera.eulerAngles.y;
            Vector3 playerRotation = new Vector3(0.0f, playerYaw + cameraYaw, 0.0f);
            Quaternion quaPlayerRotation = Quaternion.Euler(playerRotation);
            movingDirection = quaPlayerRotation * Vector3.forward;
            movingDirection = RoundVector(movingDirection, -5);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaPlayerRotation, rotateSpeedFactor * Time.fixedDeltaTime);
        }
    }
    private Vector3 RoundVector(Vector3 vector, int digit)
    {
        float shift = Mathf.Pow(10, -digit);
        vector.x = Mathf.Round(vector.x * shift) / shift;
        vector.y = Mathf.Round(vector.y * shift) / shift;
        vector.z = Mathf.Round(vector.z * shift) / shift;
        return vector;
    }
    private void UpdateSingleWheel(WheelCollider wheelcollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelcollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }
}

