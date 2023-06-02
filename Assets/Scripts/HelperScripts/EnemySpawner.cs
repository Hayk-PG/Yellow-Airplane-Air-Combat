using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Airplane Prefab")]
    [SerializeField] private Transform _enemyAirplanePrefab;

    private Transform _playerAirplane;

    public float EnemyRotationLerp { get; private set; }




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
            Transform enemyAirplane = Instantiate(_enemyAirplanePrefab, (Vector2)_playerAirplane.position + new Vector2(5f, 5f), Quaternion.identity);
            EnemyRotationLerp += 0.001f;
            isSpawned = true;

            yield return new WaitForSeconds(1f);
        }
    }
}
