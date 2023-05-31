using System;
using UnityEngine;

namespace Pautik
{
    public class Get<T> : MonoBehaviour
    {
        public static T From(GameObject obj)
        {
            return obj.GetComponent<T>() != null ? obj.GetComponent<T>() :
                   obj.GetComponentInParent<T>() != null ? obj.GetComponentInParent<T>() : default;
        }

        public static T FromChild(GameObject obj)
        {
            return obj.GetComponentInChildren<T>() != null ? obj.GetComponentInChildren<T>() : default;
        }

        public static T FromChild(GameObject obj, bool includeInactive)
        {
            return obj.GetComponentInChildren<T>(includeInactive) != null ? obj.GetComponentInChildren<T>(includeInactive) : default;
        }

        public static Type Type(object obj)
        {
            return obj.GetType();
        }
    }
}
