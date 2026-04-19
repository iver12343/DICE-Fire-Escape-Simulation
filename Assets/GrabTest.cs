using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabTest : MonoBehaviour
{
    private XRGrabInteractable grab;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener((args) => {
            Debug.Log("GRABBED!");
        });
        grab.hoverEntered.AddListener((args) => {
            Debug.Log("HAND IS NEAR!");
        });
    }
}