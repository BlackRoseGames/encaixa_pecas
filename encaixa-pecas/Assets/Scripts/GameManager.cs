using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;
    
    void Awake() {
        instance = this;
    }
    #endregion

    [Header("Objects references")]
    public GameObject blockPrefab;
    public Transform spawnPosition;
    public GameObject GameOverCanvas;
    public Animator TransitionAnimator;

    [Header("Text References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScoreText;


    [Header("Game info")]
    [SerializeField]
    int score;

    [Header("Color #1")]
    public Color firstColor;
    string firstColorName = "Color1";

    [Header("Color #2")]
    public Color secondColor;
    string secondColorName = "Color2";

    [Header("Color #2")]
    public Color thirdColor;
    string thirdColorName = "Color3";

    [Header("Block Infos")]
    public float spawnTimer = 3f;
    private float spawnCooldown = 0f;
    public float blockRotation = 10f;

    [Header("Sounds")]
    public AudioSource pointSound;
    public AudioSource gameOverSound;

    private bool gameOver = false;
    private bool reseting = false;
    private int highscore = 0;

    void Start() {
        GameOverCanvas.SetActive(false);
        highscore = PlayerPrefs.GetInt("highscore");
    }

    public Color[] getListColors() {
        return new Color[]{firstColor, secondColor, thirdColor};
    }

    public string[] getListTags() {
        return new string[]{firstColorName, secondColorName, thirdColorName};
    }

    void Update() {
        if (!gameOver) {
            if (spawnCooldown <= 0) {
                spawnRandomColorBlock();
                spawnCooldown = spawnTimer;
            }
            spawnCooldown -= Time.deltaTime;
        }
    }

    private void spawnRandomColorBlock() {
        int index = Random.Range(0, 3);
        GameObject block = Instantiate(blockPrefab, spawnPosition.position, Quaternion.identity) as GameObject;
        block.GetComponent<Rigidbody2D>().angularVelocity = blockRotation;

        SpriteRenderer blockRenderer = block.GetComponent<SpriteRenderer>();
        TrailRenderer blockTrail = block.GetComponent<TrailRenderer>();
        switch (index)
        {
            case 0:
                block.tag = firstColorName;
                blockRenderer.color = firstColor;
                blockTrail.startColor = firstColor;
                break;
            case 1:
                block.tag = secondColorName;
                blockRenderer.color = secondColor;
                blockTrail.startColor = secondColor;
                break;
            case 2:
                block.tag = thirdColorName;
                blockRenderer.color = thirdColor;
                blockTrail.startColor = thirdColor;
                break;
        }
    }

    public void GameOver() {
        if (gameOver) {
            return;
        }
        gameOverSound.Play();
        gameOver = true;
        CameraShake.instance.Shake(1f, 1f, 0.2f);
        if (score > highscore) {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
        finalScoreText.SetText(string.Format(finalScoreText.text, score));
        highScoreText.SetText(string.Format(highScoreText.text, highscore));
        GameOverCanvas.SetActive(true);
        GameOverCanvas.GetComponent<Animator>().Play("gameOver");
    }

    public void scorePoint(int point) {
        if (gameOver) {
            return;
        }
        pointSound.Play();
        Debug.Log("Scored "+point+" point(s).");
        score += point;
        scoreText.SetText(score.ToString());
    }

    public void RestartScene() {
        if (!reseting) {
            TransitionAnimator.Play("transitionIn");
            reseting = true;
            StartCoroutine(RestartSceneTimer(1f));
        }
    }

     private IEnumerator RestartSceneTimer(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }
}
