using Exa.UI;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TooltipHoverEvent : UnityEvent<string>
{
}

[RequireComponent(typeof(Hoverable))]
public class StaticTooltipHoverable : MonoBehaviour
{
    public string message;
    public TooltipHoverEvent onHover = new TooltipHoverEvent();

    private Hoverable hoverable;

    private void Awake()
    {
        hoverable = GetComponent<Hoverable>();
        hoverable.onPointerEnter.AddListener(() =>
        {
            onHover?.Invoke(message);
        });
    }
}