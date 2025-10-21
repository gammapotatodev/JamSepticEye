// fuck my life - sso

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class LightCulling : MonoBehaviour
{
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _cullFramesPerSecond = 30f;
    private List<Light> _lights = new List<Light>();

    private void Awake()
    {
        // cache all lights
        var lights = GameObject.FindGameObjectsWithTag("RealtimeLights");
        foreach (var light in lights) _lights.Add(light.GetComponent<Light>());
    }

    void Start() => InvokeRepeating("UpdateLights", 0f, (1f / _cullFramesPerSecond));

    private void UpdateLights()
    {
        foreach (var light in _lights)
        {
            float plrDistToLight = Vector3.Distance(GM_Instance.Player.transform.position, light.transform.position);

            light.enabled = plrDistToLight < _radius;
        }
    }
}
