using Exa.Utils;
using UnityEngine;

namespace Exa.Data
{
    /// <summary>
    /// Base class for a scriptable object that owns a method to register itself to game systems
    /// <para>
    /// The <see cref="RegisterSelf(GameManager)"/> method is called when the <see cref="GameManager"/> Instance is created
    /// </para>
    /// </summary>
    public abstract class RegisterableScriptableObject : ScriptableObject
    {
        public void Awake()
        {
            MonoSingletonUtils.OnInstanceCreated<GameManager>(RegisterSelf);
        }

        public abstract void RegisterSelf(GameManager gameManager);
    }
}