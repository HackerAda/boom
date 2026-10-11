using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private float spawnRadius = 0.5f;

    [Header("Spawn Settings")]
    [SerializeField] private int startCubesAmount = 4;
    [SerializeField] private float startCubesDivisionChance = 1f;

    [Header("Physics Settings")]
    [SerializeField] private float explosionPower = 100f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float sizeReduction = 0.5f;
    [SerializeField] private float decreaseChance = 2f;

    private void Start()
    {
        for (int i = 0; i < startCubesAmount; i++)
            CreateRegularCube(new Vector3(i * 2, 0, i), startCubesDivisionChance);
    }

    private Cube CreateRegularCube(Vector3 position, float divisionChance)
    {
        Cube newCube = Instantiate(cubePrefab, position, Random.rotation);
        newCube.SetDivisionChance(divisionChance);
        newCube.Clicked += HandleCubeClick;
        return newCube;
    }

    private void HandleCubeClick(Cube cube)
    {
        cube.Clicked -= HandleCubeClick;

        if (Random.value <= cube.DivisionChance) // Деление
        {
            List<Cube> children = SpawnChildren(cube.transform.position, cube.transform.localScale, cube.DivisionChance / decreaseChance);
            Explode(cube.transform.position, explosionPower, explosionRadius, children);
        }

        StartCoroutine(DelayedDestroy(cube.gameObject));
    }

    private List<Cube> SpawnChildren(Vector3 parentPos, Vector3 parentScale, float childDivisionChance)
    {
        List<Cube> children = new List<Cube>();
        int numChildren = Random.Range(2, 7);

        for (int i = 0; i < numChildren; i++)
        {
            Vector3 spawnOffset = Random.insideUnitSphere * spawnRadius;
            Vector3 spawnPos = parentPos + spawnOffset;

            Cube child = CreateRegularCube(spawnPos, childDivisionChance);
            child.transform.localScale = parentScale * sizeReduction;
            children.Add(child);
        }

        return children;
    }

    private static void Explode(Vector3 center, float power, float radius, IEnumerable<Cube> cubes)
    {
        foreach (var cube in cubes)
        {
            var rb = cube.Rigidbody;
            if (rb != null)
                rb.AddExplosionForce(power, center, radius);
        }
    }

    private IEnumerator DelayedDestroy(GameObject obj)
    {
        yield return null;
        Destroy(obj);
    }
}