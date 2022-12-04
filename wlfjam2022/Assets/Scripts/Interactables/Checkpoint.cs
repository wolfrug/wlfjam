using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CheckpointReachedEvent : UnityEvent<Checkpoint> { }

public class Checkpoint : MonoBehaviour {

    public Cloth m_checkpointCloth;
    public CheckpointReachedEvent m_checkPointEvent;
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag ("Player")) {
            collision.GetComponent<Player> ().HitCheckPoint (transform);
            m_checkPointEvent.Invoke (this);
            if (m_checkpointCloth != null) {
                m_checkpointCloth.externalAcceleration = new Vector3 (-80f, 30f, 0f);
            }
        }
    }
}