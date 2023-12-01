using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public Dialogue Dialogue;

    public void OnValidate()
    {
        if (Dialogue.choices.Count > 2)
        {
            Debug.LogError("DialogueSO: can only have 2 choices.");
            Dialogue.choices.RemoveRange(2, Dialogue.choices.Count - 2);
        }
    }
}

[Serializable]
public class Dialogue
{
    public Sprite sprite;
    public string titleFrench;
    public string titleEnglish;
    public string name;
    [FormerlySerializedAs("text")] public string textFrench;
    public string textEnglish;
    public List<DialogueSO> choices;
}