using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pautik
{
    public static class GlobalFunctions
    {
        public static void CanvasGroupActivity(CanvasGroup canvasGroup, bool isEnabled)
        {
            if (isEnabled)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        public static void DebugLog(object message)
        {
            Debug.Log(message);
        }

        public static string TextWithColorCode(string colorCode, string text)
        {
            return "<color=" + colorCode + ">" + text + "</color>";
        }

        public static string RedColorText(string text)
        {
            return "<color=#F51B01>" + text + "</color>";
        }

        public static string BlueColorText(string text)
        {
            return "<color=#80BBF7>" + text + "</color>";
        }

        public static string GreenColorText(string text)
        {
            return "<color=#00F510>" + text + "</color>";
        }

        public static string WhiteColorText(string text)
        {
            return "<color=#FFFFFF>" + text + "</color>";
        }

        public static string PartiallyTransparentText(string text)
        {
            return "<color=#FFFFFFC8>" + text + "</color>";
        }

        public static string TextWithFontSize(int fontSize, string text)
        {
            return $"<size={fontSize}>{text}</size>";
        }

        /// <summary>
        /// 0: TriangularBullet 1: WhiteBullet 2: Fisheye 3: BlackSquare 4: WhiteSquare 5: ShadowedWhiteSquare 6: Dot
        /// </summary>
        /// <returns></returns>
        public static string[] UnicodeBulletChars()
        {
            return new string[] { "\u2023", "\u25E6", "\u25C9", "\u25A0", "\u25A1", "\u274F", "\u2022" };
        }

        public static bool LocalPlayerChecker(bool isLocalPlayer)
        {
            return isLocalPlayer;
        }

        public class Loop<T>
        {
            public static void Foreach(T[] t, Action<T> OnLoop)
            {
                if (t != null)
                {
                    foreach (var item in t)
                    {
                        OnLoop?.Invoke(item);
                    }
                }
            }

            public static void Foreach(List<T> t, Action<T> OnLoop)
            {
                if (t != null)
                {
                    foreach (var item in t)
                    {
                        OnLoop?.Invoke(item);
                    }
                }
            }
        }

        public class ObjectsOfType<T> where T : UnityEngine.Object
        {
            public static T Find(Predicate<T> p)
            {
                return MonoBehaviour.FindObjectsOfType<T>().ToList().Find(p);
            }
        }
    }
}
