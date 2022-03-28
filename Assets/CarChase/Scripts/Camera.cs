using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    public bool lookAtPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleTranslation();
    }
    public void HandleTranslation()
    {
        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        if (lookAtPlayer)
        {
            transform.LookAt(player);
        }
    }
}
