using System.Collections.Generic;
using UnityEngine;

public class CubeExplode
{
    public void Explode(float explosionPower, Vector3 explosionPosition, float explosionRadius, List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            Rigidbody rigidbody = cube.Rigidbody;

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionPower, explosionPosition, explosionRadius);
            }
        }
    }
}