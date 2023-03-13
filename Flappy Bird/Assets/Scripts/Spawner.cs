using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pipesObject;
    public float pipesSpawnRate = 1f;
    public float pipesMinHeight = -1f;
    public float pipesMaxHeight = 2f;

    public GameObject boostObject;
    public float boostSpawnRate = 10.5f;
    public float boostMinHeight = -2f;
    public float boostMaxHeight = 2f;

    private void OnEnable()
    {
        // Constantly spawn pipes prefab
        InvokeRepeating(nameof(SpawnPipes), pipesSpawnRate, pipesSpawnRate);
        InvokeRepeating(nameof(SpawnBoost), boostSpawnRate, boostSpawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnPipes));
        CancelInvoke(nameof(SpawnBoost));
    }

    private void SpawnPipes()
    {
        GameObject pipes = Instantiate(pipesObject, transform.position, Quaternion.identity);
        // Randomly position pipes prefab
        pipes.transform.position += Vector3.up * Random.Range(pipesMinHeight, pipesMaxHeight);
    }

    private void SpawnBoost()
    {
        GameObject slow = Instantiate(boostObject, transform.position, Quaternion.identity);
        // Randomly position slow powerup prefab
        slow.transform.position += Vector3.up * Random.Range(boostMinHeight, boostMaxHeight);
    }
}
