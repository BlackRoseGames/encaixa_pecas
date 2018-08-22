using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Pixelplacement;

public class Player : MonoBehaviour {


    public float switchSpeedInSeconds = 0.2f;
    private bool isRotating = false;

    void Start() {
        PlayerPiece[] pieces = GetComponentsInChildren<PlayerPiece>();
        Color[] colors = GameManager.instance.getListColors();
        string[] tags = GameManager.instance.getListTags();

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].SetColor(colors[i]);
            pieces[i].SetTagCollider(tags[i]);
        }
    }
    void Update() {
        if (!isRotating) {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                Rotate(120);
            }else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                Rotate(-120);
            }
        }
    }

    public void Rotate(float rotationValue) {
        isRotating = true;
        Tween.Rotate(transform, new Vector3 (0, 0, rotationValue), Space.World, switchSpeedInSeconds, 0, Tween.EaseInOutStrong, Tween.LoopType.None, null, () => isRotating = false);
    }

}
