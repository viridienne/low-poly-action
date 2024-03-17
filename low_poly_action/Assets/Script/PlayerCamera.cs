using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;
    [SerializeField] private PlayerManager _playerManager;
    public Camera _camera;
    
    [Header("Settings")] 
    private Vector3 camVeclocity;
    private float camSmooth = 1;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void HandleCamera()
    {
        if (_playerManager)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        var camPosition = Vector3.SmoothDamp(transform.position, _playerManager.transform.position, 
                                            ref camVeclocity, camSmooth * Time.deltaTime);
        transform.position = camPosition;
    }

    private void HandleRotation()
    {
        // IF LOCK-ON -> LOCK-ON ROTATION
        // ELSE NORMAL ROTATION
    }
}
