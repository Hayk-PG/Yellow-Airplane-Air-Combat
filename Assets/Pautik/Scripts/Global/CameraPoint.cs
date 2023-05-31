using UnityEngine;

namespace Pautik
{
    public class CameraPoint : MonoBehaviour
    {
        public static Vector3 ScreenPoint(Camera mainCamera, Vector3 position)
        {
            return mainCamera.WorldToScreenPoint(position);
        }

        public static Vector3 WorldPoint(Camera mainCamera, Vector3 position)
        {
            return mainCamera.ScreenToWorldPoint(position);
        }

        public static Vector3 ViewportPoint(Camera mainCamera, Vector3 position)
        {
            return mainCamera.WorldToViewportPoint(position);
        }

        public static Ray ScreenPointToRay(Camera mainCamera, Vector3 position)
        {
            return mainCamera.ScreenPointToRay(position);
        }
    }
}
