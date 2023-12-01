using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIDocument pauseUIDocument;
    [SerializeField] private OptionsMenu optionsMenu;

    private VisualElement rootElement;

    private void OnEnable()
    {
        rootElement = pauseUIDocument.rootVisualElement;
        rootElement.Q<Button>("ResumeButton").RegisterCallback<ClickEvent, string>(OnResumeButtonClicked, "ResumeGame");
        rootElement.Q<Button>("OptionsButton")
            .RegisterCallback<ClickEvent, string>(OnOptionsButtonClicked, "OpenOptions");
        rootElement.Q<Button>("MenuButton").RegisterCallback<ClickEvent, string>(OnMenuButtonClicked, "OpenMenu");
        rootElement.Q<Button>("QuitButton").RegisterCallback<ClickEvent, string>(OnQuitButtonClicked, "QuitGame");
    }

    #region Event Callbacks

    private void OnResumeButtonClicked(ClickEvent evt, string userargs)
    {
        gameObject.SetActive(false);
    }

    private void OnOptionsButtonClicked(ClickEvent evt, string userargs)
    {
        optionsMenu.gameObject.SetActive(true);
    }

    private void OnMenuButtonClicked(ClickEvent evt, string userargs)
    {
        SceneManager.LoadScene(0);
    }

    private void OnQuitButtonClicked(ClickEvent evt, string userargs)
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    #endregion
}