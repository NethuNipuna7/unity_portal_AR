using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    public Transform MainCam;
    public Transform objectTransform;
    bool istriggered = false;
    bool iscomefromtheDoor = false;
    public GameObject Chamber1;
    public GameObject Chamber2;
    public GameObject doormask;
    float dotproductval = 0;
    void Start()
    {
        // Assign the game object's transform
        objectTransform = transform;
    }

    void Update()
    {
        // Calculate the vector from the object to the player
        Vector3 toPlayer = MainCam.position - objectTransform.position;

        // Check if the player is behind the object using dot product
        float dotProduct = Vector3.Dot(toPlayer.normalized, objectTransform.forward);
        dotproductval = dotProduct;
        // If the dot product is negative, player is behind the object
        if (dotProduct < 0 && istriggered && !iscomefromtheDoor)
        {
            Chamber1.SetActive(false);
            Chamber2.SetActive(false);
            doormask.SetActive(true);
            iscomefromtheDoor = true;
        }
        else if (dotProduct < 0 && istriggered && iscomefromtheDoor)
        {
            Chamber1.SetActive(false);
            Chamber2.SetActive(false);
            doormask.SetActive(true);
          
        }
        else if (dotProduct > 0 && istriggered && !iscomefromtheDoor)
        {
            Chamber1.SetActive(true);
            Chamber2.SetActive(false);
            doormask.SetActive(false);

        }

        if (dotProduct < 0 && !istriggered && !iscomefromtheDoor)
        {
            Chamber1.SetActive(true);
            Chamber2.SetActive(true);
            doormask.SetActive(false);
        }
        else if (dotProduct > 0 && !istriggered && !iscomefromtheDoor)
        {
            Chamber1.SetActive(true);
            Chamber2.SetActive(false);
            doormask.SetActive(false);
        }

        Debug.Log(dotProduct);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            istriggered = true;
            Debug.Log("hiiiii");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            istriggered = false;
           

            if (dotproductval > 0)
            {
                iscomefromtheDoor = false;
            }
        }
    }
}
