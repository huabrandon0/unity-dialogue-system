using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    DialogueChapter _chapterToDisplay;

    [SerializeField]
    DialogueDisplay _dialogueDisplay;

    void Start()
    {
        _dialogueDisplay.StartDisplay(_chapterToDisplay);
        StartCoroutine(AsyncClickForNextSection());
    }

    IEnumerator AsyncClickForNextSection()
    {
        while (_dialogueDisplay.HasNext)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                _dialogueDisplay.Next();

            yield return null;
        }
    }
}
