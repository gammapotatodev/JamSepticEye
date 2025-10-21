using System;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class CameraController : MonoBehaviour
{
    private enum CameraState
    {
        Unfocused,
        Focused
    }
    [SerializeField] private CameraState _cameraState = CameraState.Focused;
    [SerializeField] private List<Vector3> _cameraPositions = new();
    [SerializeField] private List<Quaternion> _cameraOrientations = new();
    private float _stateSwapSpeed = 20f;
    private bool _canCollide = false;
    
    public Transform Target;
    public Transform Collider;
    private Vector3 _origin;
    private float _targetYAngle; 
    
    public float SpeedToTarget = 15f;
    public float RotationSpeed = 15f;

    private void Awake() => Collider = transform.GetChild(0);

    private void Start()
    {
        _origin = GameObject.Find("Collision Point").transform.localPosition; // set focused, store origin for focused as its cached, then unfocus
        _cameraState =  CameraState.Unfocused;
    }

    void Update()
    {
        SwapAngle();
        Collision();
    }

    private void SwapAngle()
    {
        #region Y Axis
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _targetYAngle -= 90f;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) _targetYAngle += 90f;
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, _targetYAngle, 0), Time.deltaTime * RotationSpeed);
        #endregion
        
        #region Positional
        
        if (Input.GetKeyDown(KeyCode.UpArrow)) _cameraState =  CameraState.Unfocused;
        if (Input.GetKeyDown(KeyCode.DownArrow)) _cameraState =  CameraState.Focused;

        if (_cameraState == CameraState.Unfocused)
        {
            GM_Instance.Camera.transform.localPosition = Vector3.Lerp(GM_Instance.Camera.transform.localPosition, _cameraPositions[1], Time.deltaTime * _stateSwapSpeed);
            GM_Instance.Camera.transform.localRotation = Quaternion.Lerp(GM_Instance.Camera.transform.localRotation, _cameraOrientations[1], Time.deltaTime * _stateSwapSpeed);
            _canCollide = false;
        }
        else if (_cameraState == CameraState.Focused)
        {
            GM_Instance.Camera.transform.localPosition = Vector3.Lerp(GM_Instance.Camera.transform.localPosition, _cameraPositions[0], Time.deltaTime * _stateSwapSpeed);
            GM_Instance.Camera.transform.localRotation = Quaternion.Lerp(GM_Instance.Camera.transform.localRotation, _cameraOrientations[0], Time.deltaTime * _stateSwapSpeed);
            _canCollide = true;
        }
        
        #endregion
    }

    private bool _colliding = false;
    /// <summary>
    /// this camera collision code is very quick and dirty, but it gets the job done
    /// a lot of repeated code, but it works flawlessly so ill just leave it for
    /// times sake.
    /// </summary>
    private void Collision()
    {
        if (!_canCollide)
        {
            Collider.localPosition = Vector3.Lerp(
                Collider.localPosition,
                _origin,
                Time.deltaTime * SpeedToTarget
            );
            
            return;
        }
        
        Vector3 desiredWorldPos = transform.TransformPoint(_origin);
        Vector3 direction = (desiredWorldPos - Target.position).normalized;
        float distance = Vector3.Distance(Target.position, desiredWorldPos);

        if (Physics.Raycast(Target.position, direction, out RaycastHit hit, distance))
        {
            // only collide with ghost phasable objects if not in ghost mode
            if (GM_Instance.PlayerController.PlayerForm == PlayerForm.Being)
            {
                Collider.localPosition = transform.InverseTransformPoint(hit.point);
                _colliding = true;
            }
            else
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GhostPhase"))
                {
                    Collider.localPosition = Vector3.Lerp(
                        Collider.localPosition,
                        _origin,
                        Time.deltaTime * SpeedToTarget
                    );
                    _colliding = false;
                }
                else
                {
                    Collider.localPosition = transform.InverseTransformPoint(hit.point);
                    _colliding = true;
                }
            }
        }
        else
        {
            Collider.localPosition = Vector3.Lerp(
                Collider.localPosition,
                _origin,
                Time.deltaTime * SpeedToTarget
            );
            _colliding = false;
        }

        Debug.DrawRay(Target.position, direction * distance, Color.red);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * SpeedToTarget);
    }
}
