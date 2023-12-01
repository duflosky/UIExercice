using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private UIDocument optionsUIDocument;
    [SerializeField] private PanelSettings panelSettings;
    [SerializeField] private ThemeStyleSheet defaultThemeStyleSheet;
    [SerializeField] private ThemeStyleSheet yellowThemeStyleSheet;

    private VisualElement rootElement;
    private VisualElement audioMenuElement;
    private VisualElement generalMenuElement;
    private VisualElement graphicsMenuElement;
    private VisualElement controlsMenuElement;
    private bool canChangeLanguage;

    private void OnEnable()
    {
        rootElement = optionsUIDocument.rootVisualElement;
        rootElement.Q<Button>("AudioButton").RegisterCallback<ClickEvent, string>(OnAudioButtonClicked, "OpenAudio");
        rootElement.Q<Button>("GeneralButton")
            .RegisterCallback<ClickEvent, string>(OnGeneralButtonClicked, "OpenGeneral");
        rootElement.Q<Button>("GraphicsButton")
            .RegisterCallback<ClickEvent, string>(OnGraphicsButtonClicked, "OpenGraphics");
        rootElement.Q<Button>("ControlsButton")
            .RegisterCallback<ClickEvent, string>(OnControlsButtonClicked, "OpenControls");
        rootElement.Q<Button>("BackButton").RegisterCallback<ClickEvent, string>(OnBackButtonClicked, "CloseOptions");

        audioMenuElement = rootElement.Q<VisualElement>("AudioMenu");
        audioMenuElement.style.display = DisplayStyle.None;
        var musicSlider = audioMenuElement.Q<SliderInt>("MusicSliderInt");
        musicSlider.value = (int)(AudioListener.volume * 100f);
        musicSlider.RegisterValueChangedCallback(callback => { AudioListener.volume = callback.newValue / 100f; });

        graphicsMenuElement = rootElement.Q<VisualElement>("GraphicsMenu");
        graphicsMenuElement.style.display = DisplayStyle.None;
        var windowModeDropdown = graphicsMenuElement.Q<DropdownField>("WindowDropdownField");
        windowModeDropdown.choices = new List<string>() { "Windowed", "Fullscreen" };
        windowModeDropdown.value = Screen.fullScreenMode == FullScreenMode.Windowed
            ? "Windowed"
            : "Fullscreen";
#if UNITY_EDITOR
        UnityEditor.PlayerSettings.fullScreenMode = Screen.fullScreenMode;
#endif
        
        windowModeDropdown.RegisterValueChangedCallback(callback =>
        {
            windowModeDropdown.value = callback.newValue;
            Screen.fullScreenMode = callback.newValue == "Windowed"
                ? FullScreenMode.Windowed
                : FullScreenMode.FullScreenWindow;
#if UNITY_EDITOR
            UnityEditor.PlayerSettings.fullScreenMode = Screen.fullScreenMode;
#endif
            
        });
        var resolutionDropdown = graphicsMenuElement.Q<DropdownField>("ResolutionDropdownField");
        resolutionDropdown.choices = new List<string>() { "1920x1080", "1280x720" };
        resolutionDropdown.value = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
#if UNITY_EDITOR
        UnityEditor.PlayerSettings.defaultScreenWidth = Screen.currentResolution.width;
        UnityEditor.PlayerSettings.defaultScreenHeight = Screen.currentResolution.height;
#endif
        
        resolutionDropdown.RegisterValueChangedCallback(callback =>
        {
            resolutionDropdown.value = callback.newValue;
            switch (callback.newValue)
            {
                case "1920x1080":
                    #if UNITY_EDITOR
                    UnityEditor.PlayerSettings.defaultScreenWidth = 1920;
                    UnityEditor.PlayerSettings.defaultScreenHeight = 1080;
                    #endif
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                case "1280x720":
                    #if UNITY_EDITOR
                    UnityEditor.PlayerSettings.defaultScreenWidth = 1280;
                    UnityEditor.PlayerSettings.defaultScreenHeight = 720;
                    #endif
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
            }
        });

        controlsMenuElement = rootElement.Q<VisualElement>("ControlsMenu");
        controlsMenuElement.style.display = DisplayStyle.None;

        generalMenuElement = rootElement.Q<VisualElement>("GeneralMenu");
        generalMenuElement.style.display = DisplayStyle.Flex;
        var languageDropdown = generalMenuElement.Q<DropdownField>("LanguageDropdownField");
        languageDropdown.choices = new List<string>() { "English", "French" };
        languageDropdown.value = LocalizationSettings.SelectedLocale.name;
        languageDropdown.RegisterValueChangedCallback(callback =>
        {
            if (canChangeLanguage) return;
            switch (callback.newValue)
            {
                case "English":
                    StartCoroutine(SetLocale(0));
                    break;
                case "French":
                    StartCoroutine(SetLocale(1));
                    break;
                default:
                    return;
            }
            languageDropdown.value = callback.newValue;
        });
        var themeDropdown = generalMenuElement.Q<DropdownField>("ThemeDropdownField");
        themeDropdown.choices = new List<string>() { "Default", "Yellow" };
        themeDropdown.value = panelSettings.themeStyleSheet.name;
        themeDropdown.RegisterValueChangedCallback(callback =>
        {
            themeDropdown.value = callback.newValue;
            switch (callback.newValue)
            {
                case "Default":
                    panelSettings.themeStyleSheet = defaultThemeStyleSheet;
                    break;
                case "Yellow":
                    panelSettings.themeStyleSheet = yellowThemeStyleSheet;
                    break;
            }
        });
    }

    private IEnumerator SetLocale(int localeID)
    {
        canChangeLanguage = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        canChangeLanguage = false;
    }

    #region Event Callbacks

    private void OnAudioButtonClicked(ClickEvent evt, string userargs)
    {
        audioMenuElement.style.display = DisplayStyle.Flex;
        generalMenuElement.style.display = DisplayStyle.None;
        graphicsMenuElement.style.display = DisplayStyle.None;
        controlsMenuElement.style.display = DisplayStyle.None;
    }

    private void OnGeneralButtonClicked(ClickEvent evt, string userargs)
    {
        audioMenuElement.style.display = DisplayStyle.None;
        generalMenuElement.style.display = DisplayStyle.Flex;
        graphicsMenuElement.style.display = DisplayStyle.None;
        controlsMenuElement.style.display = DisplayStyle.None;
    }

    private void OnGraphicsButtonClicked(ClickEvent evt, string userargs)
    {
        audioMenuElement.style.display = DisplayStyle.None;
        generalMenuElement.style.display = DisplayStyle.None;
        graphicsMenuElement.style.display = DisplayStyle.Flex;
        controlsMenuElement.style.display = DisplayStyle.None;
    }

    private void OnControlsButtonClicked(ClickEvent evt, string userargs)
    {
        audioMenuElement.style.display = DisplayStyle.None;
        generalMenuElement.style.display = DisplayStyle.None;
        graphicsMenuElement.style.display = DisplayStyle.None;
        controlsMenuElement.style.display = DisplayStyle.Flex;
    }

    private void OnBackButtonClicked(ClickEvent evt, string userargs)
    {
        gameObject.SetActive(false);
    }

    #endregion
}