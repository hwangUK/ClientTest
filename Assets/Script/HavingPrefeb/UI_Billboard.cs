using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Billboard : MonoBehaviour
{
    Transform billBoard;
    private void Awake()
    {
        billBoard = transform.GetChild(0).transform;
        StartCoroutine(BillBoardUpdate());
    }
    void LateUpdate()
    {
        //빌보드 업데이트
        //billBoard.transform.LookAt(transform.position + (Camera.main.transform.rotation * Vector3.forward), Camera.main.transform.rotation * Vector3.up);
        //transform.LookAt(Vector3.zero);
    }

    IEnumerator BillBoardUpdate()
    {
        while (true)
        {
            billBoard.LookAt(transform.position + (Camera.main.transform.rotation * Vector3.forward), Camera.main.transform.rotation * Vector3.up);
            yield return new WaitForSeconds(0.5f);
        }        
    }
}
