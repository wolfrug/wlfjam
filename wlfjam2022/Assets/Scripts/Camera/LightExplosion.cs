using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LightExplosionDone : UnityEvent<LightExplosion> { }

public class LightExplosion : Interactable {
    public Animator m_lightExplosionAnimator;
    public LightMeter m_lightMeter;
    public MovementSettingsData m_stopMovementData;
    public MovementSettingsData m_normalMoveData;
    public Transform m_playerSpot;

    public bool m_allowContinueAfter = false;

    private Player m_player;
    private PlayerMovement m_playerMovement;
    private PlayerInteractions m_playerInteractions;
    private PlayerAnimator m_playerAnimator;

    private bool m_finishedAnimation = false;

    public LightExplosionDone m_doneEvent;
    public LightExplosionDone m_StartedEvent;
    // Start is called before the first frame update
    void Start () {
        m_playerMovement = FindObjectOfType<PlayerMovement> ();
        m_playerInteractions = FindObjectOfType<PlayerInteractions> ();
        m_player = FindObjectOfType<Player> ();
        m_playerAnimator = FindObjectOfType<PlayerAnimator> ();
        m_lightMeter = FindObjectOfType<LightMeter> ();
        InputEventSender.OnDance += StartDance;
    }

    public override void Activate () {
        StartLightExplosion ();
    }

    public void StartLightExplosion () {
        m_finishedAnimation = false;
        m_playerMovement.SetMovementValues (m_stopMovementData);
        m_playerInteractions.CanDance = false;
        m_playerInteractions.CanHide = false;
        m_playerMovement.enabled = false;
        StartCoroutine (LerpPlayerToSpotAndAnimate ());
    }

    IEnumerator LerpPlayerToSpotAndAnimate () {
        float t = 0;
        float speed = 8f;
        Vector3 start = m_player.transform.position;
        while (t <= 1) {
            t += Time.fixedDeltaTime / speed;
            m_playerAnimator.rb.MovePosition (Vector3.Lerp (start, m_playerSpot.transform.position, t));

            yield return null;
        }
        m_player.transform.position = m_playerSpot.transform.position;
        m_playerAnimator.rb.velocity = Vector3.zero;
        m_playerMovement.enabled = true;
        yield return new WaitForSeconds (0.1f);
        Debug.Log ("Setting trigger!");
        m_StartedEvent.Invoke (this);
        m_lightExplosionAnimator.SetTrigger ("explode");
        yield return new WaitForSeconds (0.2f);
        m_playerAnimator.animator.SetTrigger ("unveil");
        yield return new WaitForSeconds (2f);
        // Unveil? Return to normal?
        m_finishedAnimation = true;
        if (!m_allowContinueAfter) { // no continuing, so we just invoke the done
            yield return new WaitForSeconds (2f);
            m_doneEvent.Invoke (this);
            Debug.Log ("Victory!");
        }

    }

    void StartDance () {
        if (m_finishedAnimation && m_allowContinueAfter) {
            m_finishedAnimation = false;
            StartCoroutine (EndExplosion ());
        }
    }

    IEnumerator EndExplosion () {
        m_lightMeter.currentFill = 0f;
        m_playerAnimator.animator.SetTrigger ("endunveil");
        m_lightExplosionAnimator.SetTrigger ("hide");
        yield return new WaitForSeconds (2f);
        m_playerInteractions.CanHide = true;
        m_playerInteractions.CanDance = true;
        //m_playerMovement.SetMovementValues (m_normalMoveData);
        m_doneEvent.Invoke (this);

    }

    // Update is called once per frame
    void Update () {

    }
}