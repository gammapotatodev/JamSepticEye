using UnityEngine;

public class CheckForPillarsActivated : MonoBehaviour
{
  [SerializeField] private PillarSystem[] pillars;
  [SerializeField] private GameObject blade;
  private void Start()
  {
    if (pillars == null || pillars.Length == 0)
    {
      pillars = FindObjectsByType<PillarSystem>(FindObjectsSortMode.None);
      if (pillars.Length == 0)
      {
        Debug.LogError("No Pillars on scene");
      }
    }
  }

  private void Update()
  {
    bool allActivated = true;
    foreach (PillarSystem pillar in pillars)
    {
      if (!pillar.IsActivated())
      {
        allActivated = false;
        break;
      }
    }

    if (allActivated)
    {
      Debug.Log($"All done! deleting: {gameObject.name}.");
      Destroy(gameObject);
      blade.SetActive(true);
    }
  }
}
