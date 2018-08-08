using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject blockPrefab;
    public Transform spawnPosition;

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
    
    void Awake() {
        instance = this;
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
        switch (index)
        {
            case 0:
                block.tag = firstColorName;
                blockRenderer.color = firstColor;
                break;
            case 1:
                block.tag = secondColorName;
                blockRenderer.color = secondColor;
                break;
            case 2:
                block.tag = thirdColorName;
                blockRenderer.color = thirdColor;
                break;
        }
    }

    public void GameOver() {
        Debug.Log("GAME OVER");
    }

    public void scorePoint(int point) {
        Debug.Log("Scored "+point+" point(s).");
        score += point;
    }

}
