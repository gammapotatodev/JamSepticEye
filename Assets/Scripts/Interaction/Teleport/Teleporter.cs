using System;
using UnityEngine;
using static GameManager;

public class Teleporter : MonoBehaviour
{
    private Transform _to;
    public bool Used;

    private void Awake() => _to = transform.GetChild(0);

    private void OnTriggerEnter(Collider other)
    {
        if (GM_Instance.PlayerController.PlayerForm == PlayerForm.Returning) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            GM_Instance.PlayerController._rigidbody.transform.position = _to.position;
            GM_Instance.PlayerController._rigidbody.linearVelocity = Vector3.zero;
            Used = true;
        }
    }
}
