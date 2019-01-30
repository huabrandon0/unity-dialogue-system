using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
    public TMP_Text _text;
    public float _animationSpeed = 50f;
    Coroutine _textRevealCoroutine;
    bool _isRevealingText = false;

    DialogueChapter _dialogueChapter;
    int _index;

    public void SetDialogueChapter(DialogueChapter dialogueChapter)
    {
        _dialogueChapter = dialogueChapter;
    }

    public bool StartDisplay(DialogueChapter dialogueChapter)
    {
        _dialogueChapter = dialogueChapter;

        _index = 0;
        if (!DisplaySection(_index))
            return false;

        return true;
    }

    public bool Next()
    {
        if (_isRevealingText)
        {
            StopCoroutine(_textRevealCoroutine);
            _textRevealCoroutine = null;
            RevealCharacters(_text);
            _isRevealingText = false;
            return true;
        }
        else if (IsLastIndex(_index))
        {
            _text.SetText("");
            return false;
        }
        else
        {
            _index++;
            if (!DisplaySection(_index))
                return false;

            return true;
        }
    }
    
    public bool HasNext
    {
        get { return _isRevealingText || IsValidIndex(_index + 1) || IsLastIndex(_index); }    
    }

    bool IsValidIndex(int index)
    {
        return _dialogueChapter && _dialogueChapter._sections != null && index >= 0 && index < _dialogueChapter._sections.Length;
    }

    bool IsLastIndex(int index)
    {
        return _dialogueChapter && _dialogueChapter._sections != null && index == _dialogueChapter._sections.Length - 1;
    }

    bool DisplaySection(int index)
    {
        if (!IsValidIndex(index))
            return false;

        IDialogueSection dialogueSection = _dialogueChapter._sections[index];
        switch (dialogueSection)
        {
            case DialogueParagraph dp:
                AnimateText(dp._paragraph);
                break;
            case DialoguePrompt dp:
                string toDisplay = dp._paragraph;
                toDisplay += "; Choices:";
                foreach(string choice in dp._choices)
                    toDisplay += " " + choice;
                AnimateText(toDisplay);
                break;
        }
        return true;
    }

    void AnimateText(string text)
    {
        _text.SetText(text);

        if (_isRevealingText && _textRevealCoroutine != null)
            StopCoroutine(_textRevealCoroutine);

        _textRevealCoroutine = StartCoroutine(AsyncRevealCharacters(_text));
    }
    
    IEnumerator AsyncRevealCharacters(TMP_Text textComponent)
    {
        _isRevealingText = true;

        textComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = textComponent.textInfo;

        int totalVisibleCharacters = textInfo.characterCount;
        int visibleCount = 0;

        while (visibleCount <= totalVisibleCharacters)
        {
            textComponent.maxVisibleCharacters = visibleCount;
            visibleCount++;
            yield return new WaitForSeconds(1f / _animationSpeed);
        }

        _isRevealingText = false;
    }

    void RevealCharacters(TMP_Text textComponent)
    {
        textComponent.ForceMeshUpdate();
        textComponent.maxVisibleCharacters = textComponent.textInfo.characterCount;
    }
}
