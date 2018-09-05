using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour {

	public float timerParaDestruir = 1f;
    public GameObject particlePrefab;
    private SpriteRenderer sprite;
    private TrailRenderer trail;
    private Color colorToExplode;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = colorToExplode;
        trail = GetComponent<TrailRenderer>();
        trail.startColor = colorToExplode;
    }

    public void setColor(Color color) {
        colorToExplode = color;
    }

	public void StartDestroyBlock() {
		GetComponent<BoxCollider2D>().enabled = false;
		StartCoroutine(WaitAndDestroy(timerParaDestruir));
	}

	private IEnumerator WaitAndDestroy(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject explosion = Instantiate(particlePrefab, transform.position, Quaternion.identity) as GameObject;
            ParticleSystem.MainModule ps = explosion.GetComponent<ParticleSystem>().main;
            ps.startColor = colorToExplode;
            Destroy(gameObject);
        }
    }
}
