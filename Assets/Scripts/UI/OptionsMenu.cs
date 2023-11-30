using UnityEngine;
using UnityEngine.UIElements;

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
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnAudioButtonClicked(ClickEvent evt, string userargs)
    {
        advancedOptionsContainer.Remove(advancedOptionsElement);
        advancedOptionsElement = audioElement.CloneTree();
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnGeneralButtonClicked(ClickEvent evt, string userargs)
    {
        advancedOptionsContainer.Remove(advancedOptionsElement);
        advancedOptionsElement = generalElement.CloneTree();
        advancedOptionsContainer.Add(advancedOptionsElement);
    }

    private void OnGraphicsButtonClicked(ClickEvent evt, string userargs)
    {
        advancedOptionsContainer.Remove(advancedOptionsElement);
        advancedOptionsElement = graphicsElement.CloneTree();
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