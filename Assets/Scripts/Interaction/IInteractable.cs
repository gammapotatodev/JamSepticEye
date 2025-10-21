using UnityEngine;

public interface IInteractable
{
  void Interact();

  string GetPrompt();

  Vector3 GetPosition();
}
