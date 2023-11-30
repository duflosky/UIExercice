using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ModeMenu : MonoBehaviour
{
    [SerializeField] private UIDocument modeUIDocument;
    [SerializeField] private MainMenu mainMenu;
    
    private VisualElement rootElement;

    private void OnEnable()
    {
        rootElement = modeUIDocument.rootVisualElement;
        rootElement.Q<Button>("EndlessButton")
            .RegisterCallback<ClickEvent, string>(OnEndlessButtonClicked, "EndlessMode");
        rootElement.Q<Button>("SoloButton").RegisterCallback<ClickEvent, string>(OnSoloButtonClicked, "SoloMode");
        rootElement.Q<Button>("MultiplayerButton").RegisterCallback<ClickEvent, string>(OnMultiplayerButtonClicked, "MultiplayerMode");
        rootElement.Q<Button>("BackButton").RegisterCallback<ClickEvent, string>(OnBackButtonClicked, "CloseMode");
    }

    private void OnEndlessButtonClicked(ClickEvent evt, string userargs)
    {
        SceneManager.LoadScene(1);
    }

    private void OnSoloButtonClicked(ClickEvent evt, string userargs)
    {
        SceneManager.LoadScene(1);
    }

    private void OnMultiplayerButtonClicked(ClickEvent evt, string userargs)
    {
        SceneManager.LoadScene(1);
    }

    private void OnBackButtonClicked(ClickEvent evt, string userargs)
    {
        gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
