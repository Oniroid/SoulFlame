using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static DialogEvent ThrowDialog = new DialogEvent();
    public static AudioEvent PlaySFX = new AudioEvent();
    public static BoolEvent OnAlphaInput = new BoolEvent();
    public static BoolEvent OnAlphaChange = new BoolEvent();
    public static UnityEvent OnStopAlpha = new UnityEvent();
    public static UnityEvent OnStopMovement = new UnityEvent();
    public static UnityEvent OnRestart = new UnityEvent();
    public static UnityEvent ConsoleFadeOut = new UnityEvent();
    public static MovementEvent MobileMovement = new MovementEvent();
    public class DialogEvent : UnityEvent<string> { };
    public class BoolEvent : UnityEvent<bool> { };
    public class AudioEvent : UnityEvent<SFXManager.ClipName> { };
    public class MovementEvent : UnityEvent<GameFunctionsController.Direction> { };
}
