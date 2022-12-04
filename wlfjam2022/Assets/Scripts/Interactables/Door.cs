using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool m_isOpen = false;
    private Vector2 m_startPosition;
    private Vector2 m_targetPosition;
    private bool m_isInTransition;

    private void Awake() {
        m_startPosition = transform.position;
    }

    public override void Activate() {
        m_isOpen = !m_isOpen;
        if (m_isOpen) {
            m_targetPosition = m_startPosition + Vector2.up * 5;
        }
        else {
            m_targetPosition = m_startPosition;
        }
        if (!m_isInTransition) {
            StartCoroutine(MoveDoor());
        }
    }

    private IEnumerator MoveDoor() {
        m_isInTransition = true;
        while(transform.position != (Vector3)m_targetPosition) {
            transform.position = Vector2.MoveTowards(transform.position, m_targetPosition, Time.deltaTime * 5);
            yield return null;
        }
        m_isInTransition = false;
    }
}
