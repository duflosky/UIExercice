using UnityEngine;
using UnityEngine.UIElements;

public class DialogueMenu : MonoBehaviour
{
    [SerializeField] private UIDocument dialogueUIDocument;
    [SerializeField] private DialogueSO dialogueSO;

    private Dialogue currentDialogue;
    private VisualElement rootElement;

    private void OnEnable()
    {
        currentDialogue = dialogueSO.Dialogue;
        rootElement = dialogueUIDocument.rootVisualElement;
        DisplayDialogue();
    }

    private void OnFirstButtonClicked(ClickEvent evt, string userargs)
    {
        if (currentDialogue.choices.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        currentDialogue = currentDialogue.choices[0].Dialogue;
        DisplayDialogue();
    }

    private void OnSecondButtonClicked(ClickEvent evt, string userargs)
    {
        if (currentDialogue.choices.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        currentDialogue = currentDialogue.choices[1].Dialogue;
        DisplayDialogue();
    }

    private void OnNextButtonClicked(ClickEvent evt, string userargs)
    {
        if (currentDialogue.choices.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        currentDialogue = currentDialogue.choices[0].Dialogue;
        DisplayDialogue();
    }
    
    private void DisplayDialogue()
    {
        switch (currentDialogue.choices.Count)
        {
            case 1:
                rootElement.Q<Button>("NextButton").style.display = DisplayStyle.Flex;
                rootElement.Q<Button>("FirstChoice").style.display = DisplayStyle.None;
                rootElement.Q<Button>("SecondChoice").style.display = DisplayStyle.None;
                rootElement.Q<Button>("NextButton")
                    .RegisterCallback<ClickEvent, string>(OnNextButtonClicked, "ContinueDialogue");
                break;
            case 2:
                rootElement.Q<Button>("NextButton").style.display = DisplayStyle.None;
                rootElement.Q<Button>("FirstChoice").style.display = DisplayStyle.Flex;
                rootElement.Q<Button>("SecondChoice").style.display = DisplayStyle.Flex;
                rootElement.Q<Button>("FirstChoice").text = currentDialogue.choices[0].Dialogue.title;
                rootElement.Q<Button>("SecondChoice").text = currentDialogue.choices[1].Dialogue.title;
                rootElement.Q<Button>("FirstChoice")
                    .RegisterCallback<ClickEvent, string>(OnFirstButtonClicked, "ContinueDialogue");
                rootElement.Q<Button>("SecondChoice")
                    .RegisterCallback<ClickEvent, string>(OnSecondButtonClicked, "ContinueDialogue");
                break;
        }

        rootElement.Q<VisualElement>("Picture").style.backgroundImage = new StyleBackground(currentDialogue.sprite);
        rootElement.Q<Label>("NameText").text = currentDialogue.name;
        rootElement.Q<Label>("DialogueText").text = currentDialogue.text;
    }
}