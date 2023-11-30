using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    private PlayerInputs playerInputs;
    
    private void OnEnable()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Enable();
    }
    
    private void OnDisable()
    {
        playerInputs.Disable();
    }
    
    private void Start()
    {
        playerInputs.Options.Escape.performed += ctx => OnEscapeButtonClicked();
    }

    private void OnEscapeButtonClicked()
    {
        pauseMenu.SetActive(pauseMenu.activeSelf == false);
    }
}
