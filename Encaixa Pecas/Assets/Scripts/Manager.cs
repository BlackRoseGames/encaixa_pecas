using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager instance;
    public GameObject blockPrefab;
    public Transform spawnPosition;

    [SerializeField]
    private int score = 0;

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
        instance = this;
    }

    void Update() {

    }

    public void spawnRandomColorBlock() {
        int index = Random.Range(0, 3);
        GameObject block = GameObject.Instantiate(blockPrefab, spawnPosition.position, Quaternion.identity) as GameObject;
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

}
