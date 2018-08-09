using UnityEngine.SceneManagement;
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
    public TextMeshProUGUI scoreText;
    public GameObject blockPrefab;
    public Transform spawnPosition;

    public GameObject GameOverCanvas;
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

    private bool gameOver = false;
    [Header("Block Infos")]

    public float spawnTimer = 3f;
    private float spawnCooldown = 0f;
    public float blockRotation = 10f;

    [Header("Sounds")]
    public GameObject pointSound;
    public GameObject gameOverSound;


    void Start() {
        GameOverCanvas.SetActive(false);
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
        Instantiate(gameOverSound, transform.position, Quaternion.identity);
        gameOver = true;
        CameraShake.instance.Shake(1f, 1f, 0.2f);
        finalScoreText.SetText(string.Format(finalScoreText.text, score));
        GameOverCanvas.SetActive(true);
        GameOverCanvas.GetComponent<Animator>().Play("gameOver");
    }

    public void scorePoint(int point) {
        if (gameOver) {
            return;
        }
        Instantiate(pointSound, transform.position, Quaternion.identity);
        Debug.Log("Scored "+point+" point(s).");
        score += point;
        scoreText.SetText(score.ToString());
    }

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
