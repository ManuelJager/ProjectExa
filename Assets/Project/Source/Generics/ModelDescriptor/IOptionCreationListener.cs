using UnityEngine;

namespace Exa.Generics
{
    /// <summary>
    /// Supports a dropdown option view creation listener
    /// </summary>
    public interface IOptionCreationListener
    {
        /// <summary>
        /// Called after a dropdown view is created
        /// </summary>
        /// <param name="value">dropdown value</param>
        /// <param name="viewObject">dropdown view object</param>
        void OnOptionCreation(string value, GameObject viewObject);
    }
}