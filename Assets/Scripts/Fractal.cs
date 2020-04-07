using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {
	public int step { get; private set; }
	public int iterations;
	public float sizeFactor;
	public GameObject fractalModel;

	// Start is called before the first frame update
	void Start() {
		StartCoroutine(creation());
	}

	private IEnumerator creation() {
		List<Transform> generadores = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).CompareTag("Generador")) {
				generadores.Add(transform.GetChild(i));
			}
		}

		if (step < iterations) {
			foreach (var generador in generadores) {

				yield return new WaitForSeconds(2.0f + Random.Range(1.0f, 2.0f));

				var newfractalModel = Instantiate(fractalModel, generador);
				newfractalModel.transform.localScale = new Vector3(sizeFactor, sizeFactor, sizeFactor);
				newfractalModel.AddComponent<Fractal>();
				newfractalModel.GetComponent<Fractal>().step = step + 1;
				newfractalModel.GetComponent<Fractal>().iterations = iterations;
				newfractalModel.GetComponent<Fractal>().sizeFactor = sizeFactor;
				newfractalModel.GetComponent<Fractal>().fractalModel = fractalModel;
				newfractalModel.GetComponent<Fractal>().changeColor();
			}
		}
	}

	public void changeColor() {
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).GetComponent<Renderer>()) {
				transform.GetChild(i).GetComponent<Renderer>().material.color = Random.ColorHSV();
			}
		}
	}

	// Update is called once per frame
	void Update() {
		Quaternion quat = transform.localRotation * Quaternion.AngleAxis(10.0f * Time.deltaTime, Vector3.up);
		transform.localRotation = quat;
	}
}
