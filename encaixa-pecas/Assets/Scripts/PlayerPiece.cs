using System.Collections;
using UnityEngine;

public class PlayerPiece : MonoBehaviour {

	string tagForCollision;

	public void SetColor(Color color) {
		GetComponent<SpriteRenderer>().color = color;
	}

	public void SetTagCollider(string tag) {
		tagForCollision = tag;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == tagForCollision) {
			Destroy(col.gameObject);
			GameManager.instance.scorePoint(1);
		} else {
			GameManager.instance.GameOver();
		}
		Debug.Log(string.Format("Collision of object {0} with tag {1} on", col.name, col.tag));
	}

}
