using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
[RequireComponent(typeof(TextMeshPro))]
public class TextFX : MonoBehaviour
{
    Sequence clearText;
    Sequence newText;
    
    [SerializeField]
    TextMeshPro textMesh;

    DOTweenTMPAnimator textAnim;

    private void Reset()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Awake()
    {        
        textAnim = new DOTweenTMPAnimator(textMesh);
    }

    public void AnimateNewText(string newString)
    {        
        if (string.IsNullOrWhiteSpace(newString))
            return;
        if (clearText.IsActive() || newText.IsActive())
        {
            Debug.Log("Sequence Still Active!");
            return;
        }

        ClearStringAnimation(newString);

    }

    public void ClearStringAnimation(string newString)
    {
        clearText = DOTween.Sequence();

        float outSpeed = 0.5f;
        for (int i = 0; i < textAnim.textInfo.characterCount; i++)
        {
            if (!textAnim.textInfo.characterInfo[i].isVisible) continue;
            Vector3 r = new Vector3((Random.value > 0.5 ? 1 : -1) * Random.Range(1.0f, 3.0f),
                                    (Random.value > 0.5 ? 1 : -1) * Random.Range(0.0f, 1.0f),
                                    0);
            clearText.Join(textAnim.DOFadeChar(i, 0, outSpeed).From(1));
            clearText.Join(textAnim.DOOffsetChar(i, r, outSpeed));
        }

        clearText.AppendInterval(0.5f);
        clearText.AppendCallback(() =>
        {
            textMesh.text = newString;
            textAnim.Refresh();
            InputStringAnimation();
        });
    }

    public void InputStringAnimation()
    {
        newText = DOTween.Sequence();

        float inSpeed =1;
        for (int i = 0; i < textAnim.textInfo.characterCount; i++)
        {
            if (!textAnim.textInfo.characterInfo[i].isVisible) continue;
            Vector3 r = new Vector3((Random.value > 0.5 ? 1 : -1) * Random.Range(1.0f, 5.0f),
                                    (Random.value > 0.5 ? 1 : -1) * Random.Range(0.0f, 2.0f),
                                    0);
            newText.Join(textAnim.DOFadeChar(i, 1, inSpeed).From(0));
            newText.Join(textAnim.DOOffsetChar(i, r, inSpeed).From());
        }
    }
}
