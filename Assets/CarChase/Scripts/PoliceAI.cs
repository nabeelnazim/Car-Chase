using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceAI : MonoBehaviour
{
   
    private GameObject player;
    public Vector3 playerPos;
    public WheelCollider frontRight;
    public WheelCollider frontLeft;
    private NavMeshAgent nav;
    [SerializeField]
    private float speed = 30, rotationspeed = 5;
    private Rigidbody myBody;
    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
        player = GameObject.Find("RallyCar");
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        FollowPlayer();
        Drive();
    }
    private void Drive()
    {
        frontLeft.motorTorque = 1000f;
        frontRight.motorTorque = 1000f;
    }
    private void FollowPlayer()
    {
        Vector3 pointTarget = transform.position - player.transform.position;
        pointTarget.Normalize();

        float value = Vector3.Cross(pointTarget, transform.forward).y;

        myBody.angularVelocity = rotationspeed * value * new Vector3(0, 1, 0);
        //myBody.velocity = transform.forward * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*Destroy(collision.gameObject);*/
            collision.gameObject.SetActive(false);
        }
    }

}



