using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneTextController : MonoBehaviour
{
    [SerializeField] TMP_Text tmp_Text;
    [SerializeField, Multiline(4)] string[] paragraphs;
    [SerializeField] float timeBetweenLetter = 0.2f;
    [SerializeField] float timeBetweenParagraphs = 0.5f;
    int paragraphIndex;
    WaitForSeconds WaitTime;
    private void Awake()
    {
        WaitTime = new WaitForSeconds(timeBetweenLetter);
        paragraphIndex = 0;
    }
    public void ReadText()
    {
        StartCoroutine(ReadParagraph());
    }
    public void ClearText()
    {
        tmp_Text.text = string.Empty;
    }
    IEnumerator ReadParagraph()
    {

        for (int i = 0; i < paragraphs[paragraphIndex].Length; i++)
        {
            tmp_Text.text += paragraphs[paragraphIndex][i].ToString();
            yield return WaitTime;
        }
        paragraphIndex++;
        /*for (paragraphIndex = 0; paragraphIndex < paragraphs.Length; paragraphIndex++)
        {
            WaitForSeconds WaitTime2 = new WaitForSeconds(timeBetweenParagraphs);
            yield return WaitTime2;
            tmp_Text.text = string.Empty;
        }*/
        
    }
}
