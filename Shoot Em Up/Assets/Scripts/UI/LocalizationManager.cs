using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    public void SetLanguage(SystemLanguage systemLanguage)
    {
        string language = systemLanguage switch
        {
            SystemLanguage.English => "en",
            SystemLanguage.Spanish => "es",
            _ => "es",
        };        
        LoadLocale(language);
    }
    private void LoadLocale(string languageIdentifier)
    {
        LocalizationSettings.SelectedLocale.Identifier = languageIdentifier;
        
    }
}
