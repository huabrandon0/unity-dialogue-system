using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class DialogueParagraph : IDialogueSection
{
    [MultiLineProperty]
    public string _paragraph;
}
