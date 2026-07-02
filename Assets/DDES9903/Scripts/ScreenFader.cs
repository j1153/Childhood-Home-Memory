using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2.0f;
    public UnityEvent OnFadeComplete;

    private Image fadeImage;
    private bool isFading = false;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
            fadeImage.raycastTarget = false;
        }
    }

    public void StartFadeToBlack()
    {
        if (isFading) return;
        if (fadeImage == null) return;

        StartCoroutine(FadeToBlackRoutine());
    }

    private IEnumerator FadeToBlackRoutine()
    {
        isFading = true;
        fadeImage.raycastTarget = true;

        Color color = fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        isFading = false;

        if (OnFadeComplete != null)
        {
            OnFadeComplete.Invoke();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}