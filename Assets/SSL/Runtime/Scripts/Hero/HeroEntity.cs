using System;
using UnityEngine;

public class HeroEntity : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Horizontal Movements")]
    [SerializeField] private HeroHorizontalMovementsSettings _movementsSettings;
    private float _horizontalSpeed = 0f;
    private float _moveDirX = 0f;
    private float _orientX = 1f;

    [Header("Orientation")]
    [SerializeField] private Transform _orientVisualRoot;

    [Header("Debug")]
    [SerializeField] private bool _guiDebug = false;

    public void SetMoveDirX(float dirX)
    {
        _moveDirX = dirX;
    }

    private void FixedUpdate()
    {
        _UpdateHorizontalSpeed();
        _ChangeOrientFromHorizontalMovement();
        _ApplyHorizontalSpeed();
    }

    private void _ChangeOrientFromHorizontalMovement()
    {
        if (_moveDirX == 0f) return;
        _orientX = Mathf.Sign(_moveDirX);
    }

    private void _ApplyHorizontalSpeed()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _horizontalSpeed * _moveDirX;
        _rigidbody.velocity = velocity;
    }
    
    private void Update()
    {
        _UpdateOrientVisual();
    }

    private void _UpdateOrientVisual()
    {
        Vector3 newScale = _orientVisualRoot.localScale;
        newScale.x = _orientX;
        _orientVisualRoot.localScale = newScale;
    }
    private void _Accelerate()
    {
        _horizontalSpeed += _movementsSettings.acceleration * Time.fixedDeltaTime;
        if (_horizontalSpeed > _movementsSettings.speedMax)
        {
            _horizontalSpeed = _movementsSettings.speedMax;
        }
    }

    private void _UpdateHorizontalSpeed()
    {
        if (_moveDirX != 0f)
        {
            _Accelerate();
        }
        else
        {
            _horizontalSpeed = 0f; 
        }
    }
    
    private void OnGUI()
    {
        if (!_guiDebug) return;

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label(gameObject.name);
        GUILayout.Label($"MoveDirX = {_moveDirX}");
        GUILayout.Label($"OrientX = {_orientX}");
        GUILayout.Label($"Horizontal Speed = {_horizontalSpeed}");
        GUILayout.EndVertical();
    }
}