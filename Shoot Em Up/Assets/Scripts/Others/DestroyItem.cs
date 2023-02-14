using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    //Variables
    [SerializeField] private float time;
    [SerializeField] private AudioSource audioSource;

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Referencia el audioSource                                                                                         */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: activa el audioSource y tras unos segundos destruye el objeto                                                     */
    /*********************************************************************************************************************************/
    private void Start()
    {
        audioSource.Play();
        Destroy(gameObject, time);
    }
}
