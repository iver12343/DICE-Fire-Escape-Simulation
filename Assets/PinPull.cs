using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PinPull : MonoBehaviour
{
    public ExtinguisherController extinguisher;
    
    private XRGrabInteractable grab;
    private Vector3 startPos;
    private bool pulled = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectExited.AddListener(OnReleased);
        startPos = transform.position;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (pulled) return;

        float distance = Vector3.Distance(
            transform.position, startPos);

        // If moved more than 5cm it's pulled
        if (distance > 0.05f)
        {
            pulled = true;
            extinguisher.PinPulled();
            Debug.Log("Pin pulled!");
            
            // Detach pin from extinguisher
            transform.SetParent(null);
        }
        else
        {
            // Snap back if not pulled far enough
            transform.position = startPos;
        }
    }
}