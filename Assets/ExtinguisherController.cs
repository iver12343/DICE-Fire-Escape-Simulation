using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ExtinguisherController : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem spray;
    public XRGrabInteractable grabInteractable;
    public InputActionProperty triggerAction;

    [Header("Fire Suppression")]
    public Transform nozzleTip;
    public float rayDistance = 10f;
    public float suppressionRate = 30f;

    private bool isHeld = false;
    private bool pinPulled = false;
    private FireSource[] allFires;

    void Start()
    {
        allFires = FindObjectsOfType<FireSource>();
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("Extinguisher grabbed!");
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        if (spray.isPlaying) spray.Stop();
    }

    public void PinPulled()
    {
        pinPulled = true;
        Debug.Log("Pin pulled! Extinguisher unlocked!");
    }

    void Update()
    {
        // Pin pull - return immediately to prevent same frame spray
        if (Input.GetKeyDown(KeyCode.P))
        {
            PinPulled();
            return;
        }

        if (!isHeld) return;

        if (!pinPulled)
        {
            if (spray.isPlaying) spray.Stop();
            return;
        }

        bool shouldSpray = false;

        if (triggerAction.action != null && triggerAction.action.controls.Count > 0)
        {
            float val = triggerAction.action.ReadValue<float>();
            if (val > 0.5f) shouldSpray = true;
                }
                if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("F key triggered spray"); // check this in console
            shouldSpray = true;
        }
        // Keyboard fallback for testing
        if (Input.GetKey(KeyCode.F))
            shouldSpray = true;

        if (shouldSpray)
        {
            if (!spray.isPlaying) spray.Play();
            SuppressFire();
        }
        else
        {
            if (spray.isPlaying) spray.Stop();
        }
    }

    void SuppressFire()
    {
        foreach (FireSource fire in allFires)
        {
            if (fire == null) continue;

            float distance = Vector3.Distance(nozzleTip.position, fire.transform.position);

            if (distance < 10f)
            {
                fire.Suppress(suppressionRate * Time.deltaTime);
            }
        }
    }
}