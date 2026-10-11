using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _divisionChance = 1.0f;

    public event Action<Cube> Clicked;

    public float DivisionChance => _divisionChance;
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke(this);
    }

    public void SetDivisionChance(float chance)
    {
        _divisionChance = Mathf.Clamp01(chance);
    }
}