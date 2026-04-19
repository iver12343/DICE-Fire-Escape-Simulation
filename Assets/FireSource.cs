using UnityEngine;

public class FireSource : MonoBehaviour
{
    [Header("Settings")]
    public float health = 100f;
    public float decreaseSpeed = 5f;

    [Header("References")]
    public ParticleSystem fireParticles;
    public Light fireLight;

    private bool isExtinguished = false;
    private float initialHealth;
    private ParticleSystem.MainModule fireMain;
    private float initialLightIntensity;
    private Vector3 initialScale;

    void Start()
    {
        initialHealth = health;
        initialScale = transform.localScale;

        if (fireParticles == null)
            fireParticles = GetComponent<ParticleSystem>();

        if (fireParticles != null)
            fireMain = fireParticles.main;

        if (fireLight == null)
            fireLight = GetComponentInChildren<Light>();

        if (fireLight != null)
            initialLightIntensity = fireLight.intensity;
    }

    public void Suppress(float amount)
    {
        Debug.Log("Suppress called on: " + gameObject.name + " health: " + health);

        if (isExtinguished) return;

        // Fixed: removed * decreaseSpeed to prevent instant kill
        health -= amount;
        health = Mathf.Clamp(health, 0f, initialHealth);

        float ratio = health / initialHealth;

        transform.localScale = initialScale * ratio;

        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            emission.rateOverTimeMultiplier = ratio * 50f;
            fireMain.startSizeMultiplier = ratio;
            fireMain.startSpeedMultiplier = ratio;
        }

        if (fireLight != null)
            fireLight.intensity = initialLightIntensity * ratio;

        if (health <= 0f) Extinguish();
    }

    void Extinguish()
    {
        isExtinguished = true;

        if (fireParticles != null) fireParticles.Stop();
        if (fireLight != null) fireLight.enabled = false;

        // Fixed: removed transform.localScale = Vector3.zero
        Debug.Log("Fire extinguished!");
        gameObject.SetActive(false);
    }
}