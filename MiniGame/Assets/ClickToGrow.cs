using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToGrow : MonoBehaviour, IPointerClickHandler
{
    public float growScale = 2f; // 变大倍数
    public float duration = 0.2f; // 变大持续时间

    private Vector3 originalScale;
    private bool isGrowing = false;
    private float timer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isGrowing)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            if (progress >= 1f)
            {
                progress = 1f;
                isGrowing = false;
                timer = 0f;
            }

            // 用正弦曲线让动画更平滑
            float scale = originalScale.x * (1f + (growScale - 1f) * Mathf.Sin(progress * Mathf.PI / 2f));
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 点击时触发变大动画
        isGrowing = true;
        timer = 0f;

        Debug.Log($"点到了：{gameObject.name}");
    }
}