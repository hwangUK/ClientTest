using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Anim : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartAnim()
    {
        
        gameObject.SetActive(true);        
        StartCoroutine(Anima());
        
    }

    IEnumerator Anima()
    {
        Vector3 saveLocalPos = transform.localPosition;
        for (int i=0; i< 12; i++)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+ 4.0f, transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = saveLocalPos;
        gameObject.SetActive(false);
    }
}
