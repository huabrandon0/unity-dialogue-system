using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New DialogueChapter", menuName = "Dialogue/DialogueChapter")]
public class DialogueChapter : SerializedScriptableObject
{
    public IDialogueSection[] _sections;
}
