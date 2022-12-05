using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintObject : MonoBehaviour {
    public GameObject revealedObject;
    // Start is called before the first frame update
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.transform.CompareTag ("Player")) {
            revealedObject.SetActive (true);
        }
    }
    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.transform.CompareTag ("Player")) {
            revealedObject.SetActive (false);
        }
    }
}