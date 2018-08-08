using UnityEngine;
using Pixelplacement;

public class Player : MonoBehaviour {


    public float switchSpeedInSeconds = 0.2f;
    int position = 1;

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
        if(Input.GetKeyDown(KeyCode.LeftArrow) && position > 0) {
            position--;
            Rotate(-90);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && position < 2) {
            position++;
            Rotate(90);
        }
    }

    public void Rotate(float rotationValue) {
        Tween.Rotate(transform, new Vector3 (0, 0, rotationValue), Space.World, switchSpeedInSeconds, 0, Tween.EaseInOutStrong);
    }
}
