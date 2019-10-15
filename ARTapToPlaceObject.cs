using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject cube;

    private ARRaycastManager _arRaycastManager;
    private List<ARRaycastHit> _hitList;

    // Start is called before the first frame update
    void Start()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _hitList = new List<ARRaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {
        // cast a ray from the center of the screen
        Ray screenCenterRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        bool isValidHit = _arRaycastManager.Raycast(screenCenterRay, _hitList, TrackableType.PlaneWithinPolygon);
        // if the ray hits a plane
        if (isValidHit)
        {
            ARRaycastHit hit = _hitList[0];
            // place the cube indicator at the center of the screen
            cube.SetActive(true);
            cube.transform.SetPositionAndRotation(hit.pose.position, hit.pose.rotation);
            // create a cube when user touch the screen
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Instantiate(cube, cube.transform.position, cube.transform.rotation);
                }
            }
        }
        else
        {
            cube.SetActive(false);
        }
    }
}
