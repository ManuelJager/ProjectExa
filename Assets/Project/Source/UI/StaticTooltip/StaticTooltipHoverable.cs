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
    public TooltipHoverEvent onHover;

    private Hoverable _hoverable;

    private void Awake()
    {
        _hoverable = GetComponent<Hoverable>();
        _hoverable.onPointerEnter.AddListener(() =>
        {
            onHover?.Invoke(message);
        });
        _hoverable.onPointerExit.AddListener(() =>
        {
            onHover?.Invoke("");
        });
    }
}