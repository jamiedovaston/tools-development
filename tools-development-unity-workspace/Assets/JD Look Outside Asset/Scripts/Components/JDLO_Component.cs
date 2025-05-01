using UnityEngine;

namespace JD.LookOutside
{
    public abstract class JDLO_Component : MonoBehaviour
    {
        public void Awake() {
            gameObject.hideFlags = HideFlags.NotEditable;
            DontDestroyOnLoad(gameObject);
        }
    }
}
