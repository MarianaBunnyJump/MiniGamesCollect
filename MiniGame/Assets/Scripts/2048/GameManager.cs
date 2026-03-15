using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TileBoard board;
    [SerializeField] private CanvasGroup gameOverCg;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI historyText;
    [SerializeField] private AudioSource audioSource;

    public int score = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Debug.Log($"=== 游戏启动 ===");
        Debug.Log($"读取到的历史最高分：{LoadHistory()}");
        NewGame();
    }

    public void NewGame()
    {
        //重置分数
        SetScore(0);
        historyText.text = LoadHistory().ToString();

        //隐藏游戏结束画面
        gameOverCg.alpha = 0f;
        gameOverCg.interactable = false;

        //更新board状态
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOverCg.interactable = true;

        StartCoroutine(Fade(gameOverCg, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHistory();
    }

    private void SaveHistory()
    {
        int historyScore = LoadHistory();

        if (score > historyScore)
        {
            PlayerPrefs.SetInt("historyText", score);
            PlayerPrefs.Save();
        }
    }

    private int LoadHistory()
    {
        return PlayerPrefs.GetInt("historyText", 0);
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }
}