using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private UIDocument optionsUIDocument;
    [SerializeField] private VisualTreeAsset audioElement;
    [SerializeField] private VisualTreeAsset generalElement;
    [SerializeField] private VisualTreeAsset graphicsElement;
    [SerializeField] private VisualTreeAsset controlsElement;

    private VisualElement rootElement;
    private VisualElement advancedOptionsContainer;
    private VisualElement advancedOptionsElement;

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
        advancedOptionsContainer = rootElement.Q<VisualElement>("AdvancedOptionsContainer");
        advancedOptionsElement = generalElement.CloneTree();
        var languageDropdown = advancedOptionsElement.Q<DropdownField>("LanguageDropdownField");
        languageDropdown.choices = new List<string>() {"English", "Français"};
        languageDropdown.value = "English";
        languageDropdown.RegisterValueChangedCallback(callback =>
        {
            languageDropdown.value = callback.newValue;
        });
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnAudioButtonClicked(ClickEvent evt, string userargs)
    {
        advancedOptionsContainer.Remove(advancedOptionsElement);
        advancedOptionsElement = audioElement.CloneTree();
        var musicSlider = advancedOptionsElement.Q<SliderInt>("MusicSliderInt");
        musicSlider.value = (int) (AudioListener.volume * 100f);
        musicSlider.RegisterValueChangedCallback(callback =>
        {
            AudioListener.volume = callback.newValue / 100f;
        });
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnGeneralButtonClicked(ClickEvent evt, string userargs)
    {
        advancedOptionsContainer.Remove(advancedOptionsElement);
        advancedOptionsElement = generalElement.CloneTree();
        var languageDropdown = advancedOptionsElement.Q<DropdownField>("LanguageDropdownField");
        languageDropdown.choices = new List<string>() {"English", "Français"};
        languageDropdown.value = "English";
        languageDropdown.RegisterValueChangedCallback(callback =>
        {
            languageDropdown.value = callback.newValue;
        });
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnGraphicsButtonClicked(ClickEvent evt, string userargs)
    {
        advancedOptionsContainer.Remove(advancedOptionsElement);
        advancedOptionsElement = graphicsElement.CloneTree();
        var windowModeDropdown = advancedOptionsElement.Q<DropdownField>("WindowDropdownField");
        windowModeDropdown.choices = new List<string>() {"Windowed", "Fullscreen"};
        windowModeDropdown.value = Screen.fullScreenMode == FullScreenMode.Windowed
            ? "Windowed"
            : "Fullscreen";
        UnityEditor.PlayerSettings.fullScreenMode = Screen.fullScreenMode;
        windowModeDropdown.RegisterValueChangedCallback(callback =>
        {
            windowModeDropdown.value = callback.newValue;
            Screen.fullScreenMode = callback.newValue == "Windowed"
                ? FullScreenMode.Windowed
                : FullScreenMode.FullScreenWindow;
            UnityEditor.PlayerSettings.fullScreenMode = Screen.fullScreenMode;
        });
        var resolutionDropdown = advancedOptionsElement.Q<DropdownField>("ResolutionDropdownField");
        resolutionDropdown.choices = new List<string>() {"1920x1080", "1280x720"};
        resolutionDropdown.value = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
        UnityEditor.PlayerSettings.defaultScreenWidth = Screen.currentResolution.width;
        UnityEditor.PlayerSettings.defaultScreenHeight = Screen.currentResolution.height;
        resolutionDropdown.RegisterValueChangedCallback(callback =>
        {
            resolutionDropdown.value = callback.newValue;
            string[] resolution = callback.newValue.Split('x');
            int width = int.Parse(resolution[0]);
            int height = int.Parse(resolution[1]);
            Screen.SetResolution(width, height, Screen.fullScreen);
            UnityEditor.PlayerSettings.defaultScreenWidth = width;
            UnityEditor.PlayerSettings.defaultScreenHeight = height;
        });
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnControlsButtonClicked(ClickEvent evt, string userargs)
    {
        advancedOptionsContainer.Remove(advancedOptionsElement);
        advancedOptionsElement = controlsElement.CloneTree();
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnBackButtonClicked(ClickEvent evt, string userargs)
    {
        gameObject.SetActive(false);
    }
}