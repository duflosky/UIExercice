using UnityEngine;
using UnityEngine.UIElements;

public class DialogueMenu : MonoBehaviour
{
    [SerializeField] private UIDocument dialogueUIDocument;
    
    private VisualElement rootElement;
    
    private void OnEnable()
    {
        rootElement = dialogueUIDocument.rootVisualElement;
    }
}
