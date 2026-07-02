using UnityEngine;
using System.Collections;

public class AudioFader : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("淡入配置")]
    [Tooltip("音量变化的序列 (例如: 0, 0.1, 0.3)")]
    public float[] volumeSteps = { 0.0f, 0.1f, 0.3f };
    [Tooltip("每两个值之间间隔的秒数")]
    public float fadeInInterval = 3.0f;

    [Header("淡出配置")]
    [Tooltip("淡出持续的总时长")]
    public float fadeOutDuration = 2.0f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // 自动开始淡入
        StartCoroutine(FadeInRoutine());
    }

    // 淡入协程
    IEnumerator FadeInRoutine()
    {
        audioSource.volume = volumeSteps[0];
        audioSource.Play();

        for (int i = 0; i < volumeSteps.Length - 1; i++)
        {
            float startVol = volumeSteps[i];
            float endVol = volumeSteps[i + 1];
            float elapsed = 0f;

            while (elapsed < fadeInInterval)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVol, endVol, elapsed / fadeInInterval);
                yield return null;
            }
            audioSource.volume = endVol;
        }
    }

    // 外部调用的淡出函数
    public void StartFadeOut()
    {
        StopAllCoroutines(); // 停止当前的淡入过程
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        float startVol = audioSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVol, 0f, elapsed / fadeOutDuration);
            yield return null;
        }
        
        audioSource.volume = 0f;
        audioSource.Stop();
    }
}