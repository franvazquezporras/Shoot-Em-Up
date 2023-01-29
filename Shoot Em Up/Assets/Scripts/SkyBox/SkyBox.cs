using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    
    [Header("Background Textures")]
    [SerializeField]private Texture2D [] backgroundList;

    [SerializeField] ScoreControl scorePlayer;
    private float rotateSpeed = 1.2f;
    float exposureBackground =1;
    float fadeTiming = 1f;
    bool active;
    private int levelGoal = 1000;

 
    private void Awake()
    {
        RenderSettings.skybox.mainTexture = backgroundList[0];
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);       
        if ( scorePlayer.GetPlayerScore()>=levelGoal && !active)
        {
            levelGoal += 1000;
            StartCoroutine(ExposureDown());            
        }
            
        
    }

    private void ChangeTexture()
    {
        RenderSettings.skybox.mainTexture =  backgroundList[Random.Range(0,backgroundList.Length-1)];
        StartCoroutine(ExposureUp());
    }

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
