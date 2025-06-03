using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FadeOutManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 5.0f;
    [SerializeField] private bool fadeIn = false;

    private void Start()
    {
        if (fadeIn)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeIn()
    {
       yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }
    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration));
    }
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}
