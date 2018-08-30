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
			col.gameObject.GetComponent<BlockDestroyer>().StartDestroyBlock();
			GameManager.instance.scorePoint(1);
		} else {
			GameManager.instance.GameOver();
		}
	}

}
