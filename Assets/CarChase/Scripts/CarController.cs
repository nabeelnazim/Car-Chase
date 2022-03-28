using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private float horizontalinput;
    private float verticalinput;
    private bool isbreaking;
    private float currentbreakForce;
    private float currentsteerAngle;
    public float speed = 0.0f;
    public GameObject policePrefeb;
    private float points;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider backRightCollider;
    [SerializeField] private WheelCollider backLeftCollider;

    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform backRightTransform;
    [SerializeField] private Transform backLeftTransform;
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleStaring();
        Updatewheel();
    }

    private void Updatewheel()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(backRightCollider, backRightTransform);
        UpdateSingleWheel(backLeftCollider, backLeftTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void HandleStaring()
    {
        currentsteerAngle = maxSteerAngle * horizontalinput;
        frontLeftCollider.steerAngle = currentsteerAngle;
        frontRightCollider.steerAngle = currentsteerAngle;
    }

    private void HandleMotor()
    {
        frontLeftCollider.motorTorque = verticalinput * motorForce;
        frontRightCollider.motorTorque = verticalinput * motorForce;
        currentbreakForce = isbreaking ? breakForce : 0f;
        if (isbreaking)
        {
            ApplyBreaking();
        }
    }
    private void ApplyBreaking()
    {
        frontLeftCollider.brakeTorque = currentbreakForce;
        frontRightCollider.brakeTorque = currentbreakForce;
        backLeftCollider.brakeTorque = currentbreakForce;
        backRightCollider.brakeTorque = currentbreakForce;
    }

    private void GetInput()
    {
        horizontalinput = Input.GetAxis(Horizontal);
        verticalinput = Input.GetAxis(Vertical);
        isbreaking = Input.GetKey(KeyCode.Space);
    }
    public Vector3 spawnposition()
    {
        float spawnX = transform.position.x + 10;
        float spawnZ = transform.position.z + 10;
        Vector3 spawnPos = new Vector3(spawnX, transform.position.y, spawnZ);
        return spawnPos;
    }
    private void StartSpawning()
    {
        Instantiate(policePrefeb, spawnposition(), policePrefeb.transform.rotation);  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
           StartSpawning();
           points += 200;
           Destroy(collision.gameObject);
        }
    }
}
