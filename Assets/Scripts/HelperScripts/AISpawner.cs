using System.Collections;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [Header("Enemy Airplane Prefab")]
    [SerializeField] private Transform _aiAirplanePrefab;

    private Transform _playerAirplane;




    private void Start()
    {
        GetPlayerAirplane();
        StartCoroutine(Spawn());
    }

    private void GetPlayerAirplane()
    {
        _playerAirplane = FindObjectOfType<MovementController>().transform;
    }

    private IEnumerator Spawn()
    {
        bool isSpawned = false;

        while (!isSpawned)
        {
            Transform aiAirplane = Instantiate(_aiAirplanePrefab, (Vector2)_playerAirplane.position + new Vector2(5f, 5f), Quaternion.identity);
            isSpawned = true;

            yield return new WaitForSeconds(1f);
        }
    }
}
