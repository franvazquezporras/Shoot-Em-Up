using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private bool active;    
    private void Update()
    {
        if (isActiveAndEnabled && !active)
        {            
            active = true;
            StartCoroutine(ShowAndHide());
        }            
    }

    IEnumerator ShowAndHide()
    {
        yield return new WaitForSeconds(3);
        active = false;
        gameObject.SetActive(false);
    }
}
