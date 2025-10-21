  using System.Collections;
  using UnityEngine;
  using UnityEngine.UIElements;

  public class CheckForPlatesActivated : MonoBehaviour
  {
    [SerializeField] private PlateSystem[] plates;

    [SerializeField] private float liftHeight = 5f;

    private Vector3 downPosition;
    private Vector3 upPosition;
    private bool isBridgeMoved = false;
    public float bridgeSpeed = 10f;

    private void Start()
    {
      if (gameObject != null) downPosition = gameObject.transform.position;
      upPosition = downPosition + Vector3.up * liftHeight;

      if (plates == null || plates.Length == 0)
      {
        plates = FindObjectsByType<PlateSystem>(FindObjectsSortMode.None);
        if (plates.Length == 0)
        {
          Debug.LogError("No Pillars on scene");
        }
      }
    }

    private void Update()
    {
      if (isBridgeMoved) return;
      bool allActivated = true;
      foreach (PlateSystem plate in plates)
      {
        if (!plate.IsActivated())
        {
          allActivated = false;
          break;
        }
      }

      if (allActivated)
      {
        Debug.Log($"All done! Moving: {gameObject.name}.");
        isBridgeMoved = true;
        StartCoroutine(MoveBridge(upPosition));
      }
    }
    private IEnumerator MoveBridge(Vector3 targetPos)
    {
      Vector3 startPos = gameObject.transform.position;
      float time = 0f;

      while (time < 1f)
      {
        time += Time.deltaTime * bridgeSpeed;
        gameObject.transform.position = Vector3.Lerp(startPos, targetPos, time);
        yield return null;
      }
      gameObject.transform.position = targetPos;
    }
  }
