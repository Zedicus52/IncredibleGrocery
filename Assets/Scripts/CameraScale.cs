using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    [SerializeField] private Vector2 _defaultResolution = new Vector2(1920, 1080);
    [Range(0f, 1f)] [SerializeField] private float _widthOrHeight = 0;

    private Camera _camera;

    private float _initialSize;
    private float _targetAspect;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _initialSize = _camera.orthographicSize;

        _targetAspect = _defaultResolution.x / _defaultResolution.y;
    }

    private void Update()
    {
        float constantWidhtSize = _initialSize * (_targetAspect / _camera.aspect);
        _camera.orthographicSize = Mathf.Lerp(constantWidhtSize, _initialSize, _widthOrHeight);
    }
}
