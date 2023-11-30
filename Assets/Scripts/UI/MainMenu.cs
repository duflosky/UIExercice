using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument menuUIDocument;
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private ModeMenu modeMenu;

    private VisualElement rootElement;
    
    private void OnEnable()
    {
        rootElement = menuUIDocument.rootVisualElement;
        rootElement.Q<Button>("PlayButton").RegisterCallback<ClickEvent, string>(OnPlayButtonClicked, "StartGame");
        rootElement.Q<Button>("OptionsButton").RegisterCallback<ClickEvent, string>(OnOptionsButtonClicked, "OpenOptions");
        rootElement.Q<Button>("QuitButton").RegisterCallback<ClickEvent, string>(OnQuitButtonClicked, "QuitGame");
    }

    private void OnPlayButtonClicked(ClickEvent evt, string userargs)
    {
        gameObject.SetActive(false);
        modeMenu.gameObject.SetActive(true);
    }
    
    private void OnOptionsButtonClicked(ClickEvent evt, string userargs)
    {
        optionsMenu.gameObject.SetActive(true);
    }
    
    private void OnQuitButtonClicked(ClickEvent evt, string userargs)
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
