using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pipesObject;
    public float pipesSpawnRate = 1f;
    public float pipesMinHeight = -1f;
    public float pipesMaxHeight = 2f;

    public GameObject boostObject;
    public float boostSpawnRate;
    public float boostMinHeight = -2f;
    public float boostMaxHeight = 2f;

    public GameObject invObject;
    public float invSpawnRate;
    public float invMinHeight = -2f;
    public float invMaxHeight = 2f;

    private void OnEnable()
    {
        // Random spawn rates for powerups
        boostSpawnRate = Random.Range(9f, 18f);
        invSpawnRate = Random.Range(9f, 18f);
        // Constantly spawn prefabs
        InvokeRepeating(nameof(SpawnPipes), pipesSpawnRate, pipesSpawnRate);
        InvokeRepeating(nameof(SpawnBoost), boostSpawnRate, boostSpawnRate);
        InvokeRepeating(nameof(SpawnInvincible), invSpawnRate, invSpawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnPipes));
        CancelInvoke(nameof(SpawnBoost));
        CancelInvoke(nameof(SpawnInvincible));
    }

    private void SpawnPipes()
    {
        GameObject pipes = Instantiate(pipesObject, transform.position, Quaternion.identity);
        // Randomly position pipes prefab
        pipes.transform.position += Vector3.up * Random.Range(pipesMinHeight, pipesMaxHeight);
    }

    private void SpawnBoost()
    {
        GameObject boost = Instantiate(boostObject, transform.position, Quaternion.identity);
        // Randomly position boost prefab
        boost.transform.position += Vector3.up * Random.Range(boostMinHeight, boostMaxHeight);
    }

    private void SpawnInvincible()
    {
        GameObject inv = Instantiate(invObject, transform.position, Quaternion.identity);
        // Randomly position invincible mode prefab
        inv.transform.position += Vector3.up * Random.Range(invMinHeight, invMaxHeight);
    }
}
