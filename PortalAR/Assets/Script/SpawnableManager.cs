using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hit = new List<ARRaycastHit>();
    [SerializeField]
    GameObject portal;

    public Camera arCam;
    bool isfirsttime = true;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hit))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (isfirsttime)
                    {
                        isfirsttime = false;
                        portal.SetActive(true);
                        portal.transform.position = m_Hit[0].pose.position;
                        portal.AddComponent<ARAnchor>();


                     
                        Vector3 cameraPosition = arCam.transform.position;
                       
                        cameraPosition.y = m_Hit[0].pose.position.y;
                       
                        portal.transform.LookAt(cameraPosition, portal.transform.up);
                    }
                }
            }
            //else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            //{
            //    spawnedObject.transform.position = m_Hit[0].pose.position;
            //}
            //if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //{
            //    //spawnedObject = null;
            //}
        }
    }

    
}
