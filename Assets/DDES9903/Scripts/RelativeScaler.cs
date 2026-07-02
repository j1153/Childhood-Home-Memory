using UnityEngine;
using System.Collections;

public class RelativeScaler : MonoBehaviour
{
    [Header("缩放倍数配置")]
    [Tooltip("放大倍数（例如：2 表示在当前基础上乘 2）")]
    public float scaleUpMultiplier = 2.0f;
    [Tooltip("缩小倍数（例如：0.5 表示在当前基础上乘 0.5）")]
    public float scaleDownMultiplier = 0.5f;

    private float duration = 1.0f; // 固定时长 1s

    public void ScaleUp()
    {
        StopAllCoroutines();
        // 基于当前比例计算目标值
        Vector3 targetScale = transform.localScale * scaleUpMultiplier;
        StartCoroutine(ScaleRoutine(targetScale));
    }

    public void ScaleDown()
    {
        StopAllCoroutines();
        // 基于当前比例计算目标值
        Vector3 targetScale = transform.localScale * scaleDownMultiplier;
        StartCoroutine(ScaleRoutine(targetScale));
    }

    private IEnumerator ScaleRoutine(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}