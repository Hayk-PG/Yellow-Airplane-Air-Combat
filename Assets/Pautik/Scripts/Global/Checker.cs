
namespace Pautik
{
    public class Checker
    {
        /// <summary>
        /// Checks if the specified key exists in the given dictionary.
        /// </summary>
        /// <typeparam name="T">Type of the dictionary.</typeparam>
        /// <typeparam name="T1">Type of the key.</typeparam>
        /// <param name="dictionary">The dictionary to check for the key.</param>
        /// <param name="key">The key to search for in the dictionary.</param>
        /// <returns>True if the key exists in the dictionary, false otherwise.</returns>
        public static bool ContainsKey<T, T1>(System.Collections.Generic.Dictionary<T, T1> dict, T key) 
        {
            return dict.ContainsKey(key);
        }

        /// <summary>
        /// Checks if the specified key exists in the dictionary and retrieves the corresponding value.
        /// </summary>
        /// <typeparam name="T">The type of the dictionary key.</typeparam>
        /// <typeparam name="T1">The type of the dictionary value.</typeparam>
        /// <param name="dict">The dictionary to check.</param>
        /// <param name="key">The key to check for.</param>
        /// <param name="value">The retrieved value associated with the key.</param>
        /// <returns><c>true</c> if the key exists in the dictionary; otherwise, <c>false</c>.</returns>
        public static bool IsValueInDictionary<T, T1>(System.Collections.Generic.Dictionary<T, T1> dict, T key, out T1 value) 
        {
            if(ContainsKey(dict, key))
            {
                value = dict[key];

                return true;              
            }

            value = default;

            return false;
        }
    }

    public class AdjacentPositionCalculator
    {
        /// <summary>
        /// Returns an array of adjacent positions based on the given starting position.
        /// </summary>
        /// <param name="from">The starting position to calculate adjacent positions from.</param>
        /// <returns>An array of adjacent positions in the right, left, forward, and back directions.</returns>
        public static UnityEngine.Vector3[] GetAdjacentPositions(UnityEngine.Vector3 from)
        {
            return new UnityEngine.Vector3[]
            {
               from + UnityEngine.Vector3.right,   // Adjacent position in the right direction
               from + UnityEngine.Vector3.left,    // Adjacent position in the left direction
               from + UnityEngine.Vector3.forward, // Adjacent position in the forward direction
               from + UnityEngine.Vector3.back     // Adjacent position in the back direction
            };
        }
    }
}
