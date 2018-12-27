using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Anim : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartAnim()
    {
        transform.gameObject.SetActive(true);
        StartCoroutine(Anima());
        transform.gameObject.SetActive(false);
    }

    IEnumerator Anima()
    {
        Vector3 saveLocalPos = transform.position;

        for (int i=0; i< 12; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+ 4.0f, transform.position.z);
            yield return null;
        }
        transform.position = saveLocalPos;
    }
}
