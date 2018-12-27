using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Billboard : MonoBehaviour
{
    List<GameObject> billBoard = new List<GameObject>();
    private int childCount;
    private void Awake()
    {
        childCount = transform.childCount;
        
        for (int i=0; i< childCount; i++)
        {
            billBoard.Add(transform.GetChild(i).transform.gameObject);
        }
           
        StartCoroutine(BillBoardUpdate());
    }
    IEnumerator BillBoardUpdate()
    {
        while (true)
        {
            for(int i=0; i< childCount; i++)
            {
                billBoard[i].transform.position = transform.GetChild(i).transform.position;
                billBoard[i].transform.LookAt(billBoard[i].transform.position + (Camera.main.transform.rotation * Vector3.forward), Camera.main.transform.rotation * Vector3.up);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(0.8f);
        }        
    }
}
