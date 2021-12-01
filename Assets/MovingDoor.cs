using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
    public bool isOpen = false;

    public Transform door;
    public Transform startingPoint;
    public Transform endingPoint;

   private void Start()
    {
        door.position = startingPoint.position;
    }

    private void Update()
    {
        if (isOpen == true)
        {

            door.position = Vector3.MoveTowards(door.position, endingPoint.position, Time.deltaTime);
            Debug.Log(door.position);
        }
        
    }
}
