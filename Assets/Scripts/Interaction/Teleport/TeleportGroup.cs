using System;
using UnityEngine;

public class TeleportGroup : MonoBehaviour
{
    [SerializeField] private bool _disablePreviousTeleporter = false;
    public Teleporter FromTeleporter;
    public Teleporter ToTeleporter;

    private void Start()
    {
        FromTeleporter = transform.GetChild(1).gameObject.GetComponent<Teleporter>();
        ToTeleporter = transform.GetChild(0).gameObject.GetComponent<Teleporter>();
        
        if (_disablePreviousTeleporter) FromTeleporter.gameObject.SetActive(false);
    }

    void Update()
    {
        if (FromTeleporter.Used)
        {
            if (_disablePreviousTeleporter) FromTeleporter.gameObject.SetActive(false);
            ToTeleporter.gameObject.SetActive(true);
            FromTeleporter.Used = false;
        }
        
        if (ToTeleporter.Used)
        {
            FromTeleporter.gameObject.SetActive(true);
            if (_disablePreviousTeleporter) ToTeleporter.gameObject.SetActive(false);
            ToTeleporter.Used = false;
        }
    }
}
