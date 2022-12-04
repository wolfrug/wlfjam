using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventSender : MonoBehaviour {

    public delegate void DanceEvent();

    public static DanceEvent RequestDanceStartEvent;
    public static DanceEvent RequestDanceEndEvent;
    public static DanceEvent OnDanceStart;
    public static DanceEvent OnDanceEnd;
    public static void SendRequestDanceStart() {
        RequestDanceStartEvent?.Invoke();
    }
    public static void SendRequestDanceEnd() {
        RequestDanceEndEvent?.Invoke();
    }

    public static void SendDanceStart() {
        OnDanceStart?.Invoke();
    }

    public static void SendDanceEnd() {
        OnDanceEnd?.Invoke();
    }

    public delegate void HideEvent();

    public static HideEvent RequestHideStart;
    public static HideEvent RequestHideEnd;
    public static HideEvent OnHideStart;
    public static HideEvent OnHideEnd;

    public static void SendRequestHideStart() {
        RequestHideStart?.Invoke();
    }
    public static void SendRequestHideEnd() {
        RequestHideEnd?.Invoke();
    }

    public static void SendHideStart() {
        OnHideStart?.Invoke();
    }

    public static void SendHideEnd() {
        OnHideEnd?.Invoke();
    }
}
