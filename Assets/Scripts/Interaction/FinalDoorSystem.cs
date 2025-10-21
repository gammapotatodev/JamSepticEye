using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorSystem : MonoBehaviour
{
  private InventorySystem inventory;
  public CutsceneManager cutsceneManager;

  void Start()
  {
    inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    if (inventory == null)
    {
      Debug.LogError("InventorySystem nah!");
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.CompareTag("Player"))
    {
      int items = 0;
      for (int i = 0; i < inventory.slots.Length; i++)
      {
        if (inventory.isFull[i] == true)
        {
          items++;
        }
      }
      if (items == 4)
      {
        PlayerPrefs.SetString("ending", "good");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
        
      }

        
    }
  }
}
