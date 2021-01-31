using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Doozy.Engine;

public class TextChain : MonoBehaviour
{
    public TextFX textFX;
    public bool useGameEvents = true;
    [Space]
    public List<string> textList;
    [Space]
    public UnityEvent onComplete;
    int i = 0;

    private void OnEnable()
    {
        Message.AddListener<GameEventMessage>(OnMessage);
    }

    private void OnDisable()
    {
        Message.RemoveListener<GameEventMessage>(OnMessage);
    }

    public void NextString()
    {
        if (!this.enabled) return;

        if (i < textList.Count)
            textFX.AnimateNewText(textList[i++]);
        else
        {
            onComplete?.Invoke();
            this.enabled = false;
        }
    }

    public void SendGameEvent(string message)
    {
        GameEventMessage.SendEvent(message);
    }

    private void OnMessage(GameEventMessage message)
    {
        if (!useGameEvents) return;

        switch (message.EventName)
        {
            case "Text":
                    textFX.AnimateNewText(textList[i++]);
                break;
            case "Ready Text":
                if (i < textList.Count)
                    GameEventMessage.SendEvent("New Text");
                else
                {
                    GameEventMessage.SendEvent("End Text");
                    onComplete?.Invoke();
                    this.enabled = false;
                }
                break;
            default:
                break;
        }
    }
}
