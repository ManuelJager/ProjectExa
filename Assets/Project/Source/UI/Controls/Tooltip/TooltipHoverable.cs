using Exa.UI;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HoverEvent : UnityEvent<string>
{
}

[RequireComponent(typeof(Hoverable))]
public class TooltipHoverable : MonoBehaviour
{
    public string message;
    public HoverEvent onHover;

    private Hoverable hoverable;

    private void Awake()
    {
        hoverable = GetComponent<Hoverable>();
        hoverable.onPointerEnter.AddListener(() =>
        {
            onHover?.Invoke(message);
        });
        hoverable.onPointerExit.AddListener(() =>
        {
            onHover?.Invoke("");
        });
    }
}