using System.Collections;
using UnityEngine;

namespace Tanks.Complete
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [Tooltip("Array that holds different power-up prefabs that can be spawned.")]
        public PowerUp[] m_PowerUps;
        [Tooltip("Time in seconds that will wait this spawner to instantiate a new power up when collected the new one.")]
        public float m_RespawnCooldown = 10f;

        private void Start()
        {
            // Start coroutine to spawn first power-up after 10 seconds
            StartCoroutine(SpawnAfterDelay(10f));
        }

        private IEnumerator SpawnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SpawnRandomPowerUp();
        }

        private void SpawnRandomPowerUp()
        {
            if (m_PowerUps.Length > 0)
            {
                int randomNumber = Random.Range(0, m_PowerUps.Length);
                Vector3 positionToSpawn = transform.position;
                positionToSpawn.y = 1.09f;
                PowerUp m_SpawnedPowerup = Instantiate(m_PowerUps[randomNumber], positionToSpawn, Quaternion.identity);
                m_SpawnedPowerup.SetSpawner(this);
            }
        }

        public void CollectPowerUp()
        {
            StartCoroutine(RespawnPowerUp());
        }

        private IEnumerator RespawnPowerUp()
        {
            yield return new WaitForSeconds(m_RespawnCooldown);
            SpawnRandomPowerUp();
        }
    }
}
