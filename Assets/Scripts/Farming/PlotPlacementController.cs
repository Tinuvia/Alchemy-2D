using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from Jason Wieman tutorial placing turrets "Unity3D Building and Object Placement and rotation"

public class PlotPlacementController : MonoBehaviour
{
    [SerializeField] private GameObject placeableObjectPrefab;

    [SerializeField] private KeyCode newObjectHotKey = KeyCode.R;

    private GameObject currentPlaceableObject;
    private float mouseWheelRotation;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            // TODO: disable collision while placing (moves Mucklan around)
            MoveCurrentPlaceableObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void ReleaseIfClicked()
    {
        // we no longer want to place an object
        if (Input.GetMouseButtonDown(1))
        {
            currentPlaceableObject = null;
        }
    }

    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.forward, mouseWheelRotation); // mouseWheelRotation*10f increases the speed the object rotates when placing
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        // TODO: check if placing on ground
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); 

        if (hitInfo)
        {
            currentPlaceableObject.transform.position = hitInfo.point;
        }

    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotKey))
        {
            if (currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
                currentPlaceableObject.transform.SetParent(transform);
            }
            else // if we press twice, destroy object
            {
                Destroy(currentPlaceableObject);
            }
        }
    }

    //  REFACTORING 
    // Instantiate --> SpawnManager
}
