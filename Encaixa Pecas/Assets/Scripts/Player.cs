using UnityEngine;

public class Player : MonoBehaviour {


    public float switchSpeed = 10f;

    void Update() {


    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "") {
        }
        Debug.Log(col.tag);
    }
}
