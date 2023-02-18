using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
public class MainMenu : MonoBehaviour
{

    //Variables    
    [Header("Menu References")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionMenu;

    [Header("Resolution")]
    [SerializeField] private Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [SerializeField] private Toggle fullscreenCheck;

    [Header("Quality")]
    [SerializeField] private Dropdown qualityDropdown;

    [Header("Language")]
    [SerializeField] private Dropdown languageDropdown;

    [Header("Brightness")]
    [SerializeField] private Image brightnessPanel;
    [SerializeField] private Slider brightnessSlider;

    [Header("Sounds")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource click;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider soundVolume;

    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    private bool sound;
    private bool music;
    private bool firstLoad = true;

    private bool update = false;
    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene las resoluciones y carga los parametros guardados de los playerpref                                       */
    /*********************************************************************************************************************************/
    private void Start()
    {        
        TakeAllResolutions();        
        LoadOptionsUI();        
        LoadSetting();
        firstLoad = false;
    }

    /*********************************************************************************************************************************/
    /*Funcion: TakeAllResolutions                                                                                                    */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene las resoluciones posibles de la pantalla para generarlas en el dropbox de resoluciones sin que se repitan */
    /*********************************************************************************************************************************/
    private void TakeAllResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (i >= 2 && (resolutions[i].width != resolutions[i - 1].width || resolutions[i].height != resolutions[i - 1].height))
                options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    /*********************************************************************************************************************************/
    /*Funcion: SetResolution                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: resolutionIndex (resolucion seleccionada del dropbox)                                                   */
    /*Descripción: Modifica la resolucion del juego                                                                                  */
    /*********************************************************************************************************************************/
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        if (!firstLoad)
            update = true;
    }
    /*********************************************************************************************************************************/
    /*Funcion: SetLanguage                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: language (idioma seleccionada del dropbox)                                                              */
    /*Descripción: Modifica el idioma del juego                                                                                      */
    /*********************************************************************************************************************************/
    public void SetLanguage(int language)
    {
        SystemLanguage languageS = language switch
        {
            0 => SystemLanguage.Spanish,
            1 => SystemLanguage.English,
            _ => SystemLanguage.Spanish,
        };        
        GetComponent<LocalizationManager>().SetLanguage(languageS);
        if(!firstLoad)
            update = true;
        
    }
   
    /*********************************************************************************************************************************/
    /*Funcion: SetMasterVolume                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripción: Modifica el volumen master del juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        if (!firstLoad)
            update = true;
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetMusicVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripción: Modifica el volumen musica del juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        if (musicButton != null)
        {
            if (volume <= -0.001)
            {
                music = true;
                musicButton.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                music = false;
                musicButton.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        if (!firstLoad)
            update = true;
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetSoundVolume                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: volume (volumen nuevo)                                                                                  */
    /*Descripción: Modifica el volumen de sonidos juego                                                                              */
    /*********************************************************************************************************************************/
    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("soundVolume", Mathf.Log10(volume) * 20);
        if (soundButton != null)
        {
            if (volume <= -0.001)
            {
                sound = true;
                soundButton.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                sound = false;
                soundButton.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        if (!firstLoad)
            update = true;
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetQuality                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: qualityIndex (index del dropbox)                                                                        */
    /*Descripción: Modifica la calidad de graficos con el valor recibido                                                             */
    /*********************************************************************************************************************************/
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        if (!firstLoad)
            update = true;
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetFullScreen                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: isFullscreen (booleana para controlar si esta en fullscreen o no el juego)                              */
    /*Descripción: activa o desactiva la pantalla completa                                                                           */
    /*********************************************************************************************************************************/
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (!firstLoad)
            update = true;
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetBrightness                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: value (valor del slider para aumentar o reducir el brillo)                                              */
    /*Descripción: activa o desactiva la pantalla completa                                                                           */
    /*********************************************************************************************************************************/
    public void SetBrightness(float value)
    {
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, value);
        if (!firstLoad)
            update = true;
    }

    /*********************************************************************************************************************************/
    /*Funcion: PlaySoundClick                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Reproduce el sonido de click                                                                                      */
    /*********************************************************************************************************************************/
    public void PlaySoundClick()
    {
        if (!firstLoad)
            click.Play();
    }
    /*********************************************************************************************************************************/
    /*Funcion: ShowPanel                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: showImage (panel a mostrar)                                                                             */
    /*Descripción: Activa el objeto que recibe como parametro                                                                        */
    /*********************************************************************************************************************************/
    public void ShowPanel(GameObject showImage)
    {
        StartCoroutine(Show(showImage));
    }
    /*********************************************************************************************************************************/
    /*Funcion: Show                                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: showImage (panel a mostrar)                                                                             */
    /*Descripción: Activa el objeto que recibe como parametro tras un segundo                                                        */
    /*********************************************************************************************************************************/
    private IEnumerator Show(GameObject showImage)
    {
        yield return new WaitForSecondsRealtime(1);
        showImage.gameObject.SetActive(true);
    }
    /*********************************************************************************************************************************/
    /*Funcion: Hide                                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: hideImage (panel a ocultar)                                                                             */
    /*Descripción: Desactiva el objeto que recibe como parametro                                                                     */
    /*********************************************************************************************************************************/
    public void Hide(GameObject hideImage)
    {
        Animator anim = hideImage.GetComponent<Animator>();
        anim.SetBool("Hide", true);
        StartCoroutine(HideObject(hideImage));
    }

    /*********************************************************************************************************************************/
    /*Funcion: QuitGame                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: quitPanel (panel a mostrar)                                                                             */
    /*Descripción: Activa el panel de confirmar si se desea salir del juego                                                          */
    /*********************************************************************************************************************************/
    public void QuitGame(GameObject quitPanel)
    {
        ShowPanel(quitPanel);
    }

    /*********************************************************************************************************************************/
    /*Funcion: CloseGame                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Cierra la aplicacion por completo                                                                                 */
    /*********************************************************************************************************************************/
    public void CloseGame()
    {
        Application.Quit();
    }

    /*********************************************************************************************************************************/
    /*Funcion: LoadSetting                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Carga los parametros guardados y asigna los valores a los componentes de la UI                                    */
    /*********************************************************************************************************************************/
    private void LoadSetting()
    {
        SetLanguage(languageDropdown.value);
        SetFullScreen(fullscreenCheck.isOn);
        SetQuality(qualityDropdown.value);
        SetResolution(resolutionDropdown.value);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, brightnessSlider.value);
        SetMasterVolume(masterVolume.value);
        SetMusicVolume(musicVolume.value);
        SetSoundVolume(soundVolume.value);
    }
    /*********************************************************************************************************************************/
    /*Funcion: LoadOptionsUI                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Carga los parametros guardados en playerpref                                                                      */
    /*********************************************************************************************************************************/
    public void LoadOptionsUI()
    {
        if (PlayerPrefs.GetInt("fullScreen") == 1)
            fullscreenCheck.isOn = true;
        else
            fullscreenCheck.isOn = false;
        languageDropdown.value = PlayerPrefs.GetInt("language",1);
        qualityDropdown.value = PlayerPrefs.GetInt("quality", 0);
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", 0);
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 0f);
        masterVolume.value = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        soundVolume.value = PlayerPrefs.GetFloat("soundVolume", 0.5f);        
    }

    /*********************************************************************************************************************************/
    /*Funcion: Deny                                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Cancela los cambios realizados en el menu de opciones si no se han guardado previamente                           */
    /*********************************************************************************************************************************/
    public void Deny()
    {
        LoadSetting();
        LoadOptionsUI();
        update = false;        
    }

    /*********************************************************************************************************************************/
    /*Funcion: Accept                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Acepta los cambios realizados en el menu de opciones y los envia a guardar en los playerpref                      */
    /*********************************************************************************************************************************/
    public void Accept()
    {
        SaveSetting();
        update = false;
    }

    /*********************************************************************************************************************************/
    /*Funcion: SaveSetting                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Guarda los valores de las opciones en los distintos playerpref                                                    */
    /*********************************************************************************************************************************/
    private void SaveSetting()
    {
        if (fullscreenCheck.isOn)
            PlayerPrefs.SetInt("fullScreen", 1);
        else
            PlayerPrefs.SetInt("fullScreen", 0);
        PlayerPrefs.SetInt("language", languageDropdown.value);
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.SetFloat("masterVolume", masterVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("soundVolume", soundVolume.value);
    }

    /*********************************************************************************************************************************/
    /*Funcion: Resume                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: hideImage (panel de pausa)                                                                              */
    /*Descripción: Activa el tiempo de juego y oculta el panel de pausa                                                              */
    /*********************************************************************************************************************************/
    public void Resume(GameObject hideImage)
    {
        Time.timeScale = 1;
        Hide(hideImage);
    }
    /*********************************************************************************************************************************/
    /*Funcion: Resume                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: showImage (panel de pausa)                                                                              */
    /*Descripción: Desactiva del tiempo de juego y acitiva el panel de pausa                                                         */
    /*********************************************************************************************************************************/
    public void Pause(GameObject showImage)
    {
        Time.timeScale = 0; 
        ShowPanel(showImage);        
       
    }

    /*********************************************************************************************************************************/
    /*Funcion: MuteUnmute                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: type (comprueba si es el boton de musica o sonido)                                                      */
    /*Descripción: mutea o desmutea el sonido o la musica del juego                                                                  */
    /*********************************************************************************************************************************/
    public void MuteUnmute(int type)
    {
        if (type == 1)
        {
            if (music)
            {
                if (PlayerPrefs.GetFloat("musicVolume") <= -0.001f)
                    PlayerPrefs.SetFloat("musicVolume", 0.5f);
                SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 0.5f));
                LoadOptionsUI();
            }
            else
                SetMusicVolume(-0.001f);
        }
        else
        {
            if (sound)
            {
                if (PlayerPrefs.GetFloat("soundVolume") <= -0.001f)
                    PlayerPrefs.SetFloat("soundVolume", 0.5f);
                SetSoundVolume(PlayerPrefs.GetFloat("soundVolume", 0.5f));
                LoadOptionsUI();
            }
            else
                SetSoundVolume(-0.001f);
        }

        EventSystem.current.SetSelectedGameObject(null);
    }
    /*********************************************************************************************************************************/
    /*Funcion: Resume                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: scene (escena a cargar)                                                                                 */
    /*Descripción: Carga la escena que recibe como parametro                                                                         */
    /*********************************************************************************************************************************/
    public void LoadScene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    /*********************************************************************************************************************************/
    /*Funcion: ShowUpdatePanel                                                                                                       */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: panel (panel a mostrar)                                                                                 */
    /*Descripción: Muestra la pantalla de alerta del menu de opciones al cancelar si se ha realizado modificacion sin guardar        */
    /*********************************************************************************************************************************/
    public void ShowUpdatePanel(GameObject panel)
    {
        if (update)
            ShowPanel(panel);
        else
        {
            Hide(panel.transform.parent.gameObject);
            ShowPanel(mainMenu);
        }
            
    }
    /*********************************************************************************************************************************/
    /*Funcion: HideObject                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: hideImage (objeto a desactivar)                                                                         */
    /*Descripción: desactiva el objeto que recibe pasado un segundo                                                                  */
    /*********************************************************************************************************************************/
    public IEnumerator HideObject(GameObject hideImage)
    {
        yield return new WaitForSecondsRealtime(1);
        hideImage.SetActive(false);
    }
}
