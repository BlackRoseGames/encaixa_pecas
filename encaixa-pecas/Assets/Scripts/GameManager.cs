using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject blockPrefab;
    public Transform spawnPosition;

    [Header("Color #1")]
    public Color firstColor;
    public string firstColorName;

    [Header("Color #2")]
    public Color secondColor;
    public string secondColorName;

    [Header("Color #2")]
    public Color thirdColor;
    public string thirdColorName;

    void Awake() {
        instance = self;
    }

    void Update() {

    }

    public void spawnRandomColorBlock() {
        int index = Random.Range(0, 3);
        SpriteRenderer blockRenderer = Instantiate(blockPrefab, spawnPosition.position, Quaternion.identity) as SpriteRenderer;
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

}
