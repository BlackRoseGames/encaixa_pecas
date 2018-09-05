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
    [Tooltip("TextMeshPro para o texto de score")]
    public TextMeshProUGUI scoreText;
    [Tooltip("TextMeshPro para a maior pontuacao")]
    public TextMeshProUGUI highScoreText;
    [Tooltip("TextMeshPro para a pontuação final")]
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

    [Header("Color #3")]
    public Color thirdColor;
    string thirdColorName = "Color3";

    [Header("Block Infos")]
    [Tooltip("Tempo Inicial Para o bloco cair")]
    public float spawnTimer = 3f;
    [Tooltip("Tempo que é reduzido quando o numero X de blocos cair")]
    public float spawnReductor;
    [Tooltip("Tempo minimo para o bloco cair")]
    public float spawnMinTimer = 1f;
    [Tooltip("Rotação do bloco")]
    public float blockRotation = 10f;
    private float spawnCooldown = 0f;
    [Tooltip("O numero de blocos até aumentar a velocidade")]
    public int spawnsToSpeedUp;
    private int spawnsToSpeedUpCount = 0;

    [Header("Sounds")]
    public AudioSource pointSound;
    public AudioSource gameOverSound;
    public AudioSource musicSound;

    private bool gameOver = false;
    private bool reseting = false;
    private int highscore = 0;
    private int sameColorCount = 0;
    private int sameColorIndex = 0;

    void Start() {
        spawnCooldown = spawnTimer;
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
                spawnsToSpeedUpCount++;
                if(spawnsToSpeedUp == spawnsToSpeedUpCount) {
                    spawnsToSpeedUpCount = 0;
                    if (spawnTimer > spawnMinTimer) {
                        spawnTimer -= spawnReductor;
                    } else if ( spawnTimer < spawnMinTimer) {
                        spawnTimer = spawnMinTimer;
                    }
                }
                spawnCooldown = spawnTimer;
            }
            spawnCooldown -= Time.deltaTime;
        }
    }

    private void spawnRandomColorBlock() {
        int index = Random.Range(0, 3);
        if (sameColorIndex != index) {
            sameColorIndex = index;
            sameColorCount = 0;
        }
        if (sameColorCount >= 3) {
            while(index == sameColorIndex) {
                index = Random.Range(0, 3);
            }
            sameColorIndex = index;
            sameColorCount = 0;
        }
        sameColorCount++;
        sameColorIndex = index;
        GameObject block = Instantiate(blockPrefab, spawnPosition.position, Quaternion.identity) as GameObject;
        block.GetComponent<Rigidbody2D>().angularVelocity = blockRotation;

        BlockDestroyer blockObj = block.GetComponent<BlockDestroyer>();

        switch (index) {
            case 0:
                block.tag = firstColorName;
                blockObj.setColor(firstColor);
                break;
            case 1:
                block.tag = secondColorName;
                blockObj.setColor(secondColor);
                break;
            case 2:
                block.tag = thirdColorName;
                blockObj.setColor(thirdColor);
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
