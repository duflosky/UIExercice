using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public Dialogue Dialogue;
}

[Serializable]
public class Dialogue
{
    public Sprite sprite;
    public string title;
    public string name;
    public string text;
    public List<DialogueSO> choices;
}