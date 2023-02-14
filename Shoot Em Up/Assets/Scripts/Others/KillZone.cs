using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    /*********************************************************************************************************************************/
    /*Funcion: OnTriggerEnter2D                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: collision (collider del objeto que entra en el trigger)                                                 */
    /*Descripción: Destruye el objeto que entra en el area de colision                                                               */
    /*********************************************************************************************************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        Destroy(collision.gameObject);
    }
}
