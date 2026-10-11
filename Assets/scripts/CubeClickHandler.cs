using UnityEngine;
using System;

public class CubeClickHandler : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    public event Action<Cube> CubeClicked;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Cube>(out Cube cube))
                {
                    CubeClicked?.Invoke(cube);
                }
            }
        }
    }
}