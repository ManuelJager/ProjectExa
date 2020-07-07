using Exa.Utils;
using UnityEngine;

namespace Exa.Data
{
    /// <summary>
    /// Base class for a scriptable object that owns a method to register itself to game systems
    /// <para>
    /// The <see cref="RegisterSelf(MainManager)"/> method is called when the <see cref="MainManager"/> Instance is created
    /// </para>
    /// </summary>
    public abstract class RegisterableScriptableObject : ScriptableObject
    {
        public void Awake()
        {
            MonoSingletonUtils.OnInstanceCreated<MainManager>(RegisterSelf);
        }

        public abstract void RegisterSelf(MainManager gameManager);
    }
}