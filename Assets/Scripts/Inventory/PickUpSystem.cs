using UnityEngine;
using static GameManager;

public class PickUpSystem : MonoBehaviour
{
  private InventorySystem inventory;
  public GameObject slotItem;

  private void Start()
  {
    inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.CompareTag("Player") && GM_Instance.PlayerController.PlayerForm == PlayerForm.Being)
    {
      for(int i = 0; i < inventory.slots.Length; i++)
      {
        if (inventory.isFull[i] == false)
        {
          inventory.isFull[i] = true;
          Instantiate(slotItem, inventory.slots[i].transform);
          Destroy(gameObject);
          break;
        }
      }
    }
  }
}
