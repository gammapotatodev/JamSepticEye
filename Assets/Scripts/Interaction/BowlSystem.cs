using UnityEngine;

public class BowlSystem : MonoBehaviour, IActivatable
{
  public string requiredItemName;
  public GameObject item;

  private InventorySystem inventory;

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
          //inventory.isFull[i] = false;
          //Destroy(inventory.slots[i].transform.GetChild(0).gameObject);
          item.SetActive(true);
          return;
        }
      }
    }
  }
}
