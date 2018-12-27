using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager_Scene1 : MonoBehaviour
{
    Camera mainCam;
    Vector3 mouseStart;
    Vector3 mouseMove;
    public float camSpeed;
    // Start is called before the first frame update
    void Awake()
    { 
    }
    private void Start()
    {
        StartCoroutine(Co_CamUpdate());
    }

    
    IEnumerator Co_CamUpdate()
    {
        //RaycastHit rHit;
        while (true){
           
            if (Input.GetMouseButtonDown(0))
            {
                //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rHit, 1000,layerMask);
                ////Debug.Log(rHit.collider.tag);
                //Debug.Log(rHit.collider.gameObject.name);
                //Debug.Log("IN");
                mouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
                mouseStart = Camera.main.ScreenPointToRay(mouseStart).direction;
            }
            else if (Input.GetMouseButton(0))
            {
                mouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
                mouseMove = Camera.main.ScreenPointToRay(mouseMove).direction;
                
                var moveAmount = (mouseStart - mouseMove).normalized;
                moveAmount.x *= camSpeed;
                moveAmount.y = 0.0f;
                moveAmount.z *= camSpeed;
                Camera.main.transform.position = (Camera.main.transform.position + moveAmount);
            }
            yield return null;
        }
    }    
}
