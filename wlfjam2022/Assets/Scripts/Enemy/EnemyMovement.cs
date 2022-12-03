using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_targets = new List<Transform>();
    [SerializeField, Tooltip("If enemy goes back and forth between points")]
    private bool m_isPingPong;
    [SerializeField]
    private float m_movementSpeed = 1;

    private Transform m_currentTarget;

    private int m_direction = 1;
    private int m_currentIndex = 0;

    private void Start() {
        if(m_targets.Count > 0) {
            m_currentTarget = m_targets[m_currentIndex];
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine() {
        while(transform.position != m_currentTarget.position) {
            transform.position = Vector2.MoveTowards(transform.position, m_currentTarget.position, m_movementSpeed * Time.deltaTime);
            yield return null;
        }
        GetNextTarget();
    }

    private void GetNextTarget() {
        if(m_direction == 1) {
            if(m_currentIndex < m_targets.Count - 1) {
                m_currentIndex++;
            }
            else {
                if (m_isPingPong) {
                    m_direction = -1;
                    m_currentIndex--;
                }
                else {
                    m_currentIndex = 0;
                }
            }
        }
        else {
            if(m_currentIndex > 0) {
                m_currentIndex--;
            }
            else {
                m_direction = 1;
                m_currentIndex++;
            }
        }
        m_currentTarget = m_targets[m_currentIndex];
        StartCoroutine(MoveCoroutine());
    }

    private void OnDrawGizmos() {
        foreach (var item in m_targets) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(item.position, 1);
        }
    }
}
