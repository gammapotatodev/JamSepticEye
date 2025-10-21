using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PillarSystem : MonoBehaviour, IActivatable
{
  public GameObject activatedObject;
  public string requiredItemName;
  
  private InventorySystem inventory;
  private bool isActivated = false;

  private void Start()
  {
    inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
  }

  public void Activate()
  {
    for (int i = 0; i < inventory.slots.Length; i++)
    {
      if (inventory.isFull[i])
      {
        if (inventory.slots[i].transform.GetChild(0).name.Contains(requiredItemName))
        {
          inventory.isFull[i] = false;
          Destroy(inventory.slots[i].transform.GetChild(0).gameObject);
          isActivated = true;
          //activatedObject.SetActive(true);
          //if (activatedObject != null)
          //{
          //  activatedObject.SetActive(true);
          //  Debug.Log($"Pillar activated with {requiredItemName}!");
          //}
          return;
        }
      }
    }
    Debug.Log("Correct statuette not found in inventory!");
  }
  public bool IsActivated()
  {
    return isActivated;
  }
}
