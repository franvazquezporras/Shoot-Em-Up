using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    //Variables
    private bool active;

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Activa el cartel de nivel superado y llama a la corrutina que luego lo ocultara                                   */
    /*********************************************************************************************************************************/
    private void Update()
    {
        if (isActiveAndEnabled && !active)
        {            
            active = true;
            StartCoroutine(ShowAndHide());
        }            
    }


    /*********************************************************************************************************************************/
    /*Funcion: ShowAndHide                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Tras unos segundos oculta el cartel de nivel superado                                                             */
    /*********************************************************************************************************************************/
    IEnumerator ShowAndHide()
    {
        yield return new WaitForSeconds(3);
        active = false;
        gameObject.SetActive(false);
    }
}
