using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventBinder : MonoBehaviour
{

    public const string ON_LAST_BALL_ARRIVED = "lba";

    private static EventBinder eventManager;

    private Dictionary<string, UnityEvent> eventDictionary;


    public static EventBinder instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventBinder)) as EventBinder;
                if (!eventManager)
                {
                    Debug.LogError("Couldn't find script");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }
    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string EventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(EventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(EventName, thisEvent);
        }
    }

    public static void StopListening(string EventName, UnityAction listener)
    {
        if (eventManager == null)
        {
            return;
        }

        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(EventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string EventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(EventName, out thisEvent))
        {
            thisEvent.Invoke();
        }

    }
}
