using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    //Variables
    [Header("Background Textures")]
    [SerializeField]private Texture2D [] backgroundList;

    [SerializeField] ScoreControl scorePlayer;
    private float rotateSpeed = 0.2f;
    float exposureBackground =1;
    float fadeTiming = 1f;
    bool active;
    private int levelGoal = 1000;



    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Asigna el primer valor del array al skybox texture                                                                */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        RenderSettings.skybox.mainTexture = backgroundList[0];
    }


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Va rotando la textura del skybox para hacer efecto de movimiento, tambien cambia el fondo del mismo cada 1000     */
    /*              puntos                                                                                                           */
    /*********************************************************************************************************************************/
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);       
        if ( scorePlayer.GetPlayerScore()>=levelGoal && !active)
        {
            levelGoal += 1000;
            StartCoroutine(ExposureDown());            
        }
    }

    /*********************************************************************************************************************************/
    /*Funcion: ChangeTexture                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Cambia la textura del skybox por una de las texturas de la lista                                                  */    
    /*********************************************************************************************************************************/

    private void ChangeTexture()
    {
        RenderSettings.skybox.mainTexture =  backgroundList[Random.Range(0,backgroundList.Length-1)];
        StartCoroutine(ExposureUp());
    }


    /*********************************************************************************************************************************/
    /*Funcion: ExposureDown                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Reduce en el tiempo a 0 el exposure del skybox hasta y manda a cambiar el fondo del mismo                         */
    /*********************************************************************************************************************************/
    IEnumerator ExposureDown()
    {
        active = true;
        float elapsedTime = 0.0f;
        while(elapsedTime < fadeTiming)
        {
            elapsedTime += Time.deltaTime*0.2f;            
            RenderSettings.skybox.SetFloat("_Exposure", exposureBackground - elapsedTime);
            yield return new WaitForEndOfFrame();
        }
        ChangeTexture();
    }



    /*********************************************************************************************************************************/
    /*Funcion: ExposureUp                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Aumenta el parametro exposure del skybox hasta que sea visible                                                    */
    /*********************************************************************************************************************************/
    IEnumerator ExposureUp()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeTiming)
        {

            elapsedTime += Time.deltaTime * 0.2f;
            RenderSettings.skybox.SetFloat("_Exposure", 0 + elapsedTime);
            yield return new WaitForEndOfFrame();
        }
        active = false;
    }
}
