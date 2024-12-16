
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    public int WithinArea = 0;
    public int RobotLimit = 400;
    public Vector3 StartJumpTo = Vector3.zero;
    private S2024D14Extend Handler;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<GameObject> allObjects = new List<GameObject>();
        GameObject.FindGameObjectsWithTag("MapHandler", allObjects);
        Handler = allObjects.First().GetComponent<S2024D14Extend>();

        transform.position = StartJumpTo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        WithinArea++;

        if (RobotLimit < WithinArea)
        {
            Handler.OnDetectAreaExceeded();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        WithinArea--;
    }
}
