using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HeadMotionEffects : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Volume volume;

    [SerializeField] private float linearThreshold = 2.5f;
    [SerializeField] private float angularThreshold = 250f;

    [Range(0f, 1f)]
    [SerializeField] private float maxIntensity = 0.4f;

    [SerializeField] private float fadeInSpeed = 8f;
    [SerializeField] private float fadeOutSpeed = 3f;
    [SerializeField] private float holdDuration = 0.15f;

    private Vignette vignette;
    private VolumeProfile runtimeProfile;

    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private float currentIntensity;
    private float holdTimer;

    private void Start()
    {
        // Head gotta exist, duh
        if (head == null)
            head = transform;

        if (volume == null)
        {
            enabled = false;
            return;
        }

        runtimeProfile = volume.profile;

        if (!runtimeProfile.TryGet(out vignette))
        {
            enabled = false;
            return;
        }

        vignette.active = true;
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0f;

        previousPosition = head.position;
        previousRotation = head.rotation;
    }

    private void LateUpdate()
    {
        float dt = Time.deltaTime;

        if (dt <= 0f)
            return;


        // You have no idea how long it took me to get the math right
        // 3 years of computer science and math finally paying off :'D
        float linearVelocity =
            Vector3.Distance(head.position, previousPosition) / dt;

        float angularVelocity =
            Quaternion.Angle(head.rotation, previousRotation) / dt;

        previousPosition = head.position;
        previousRotation = head.rotation;

        bool rapidMovement =
            linearVelocity > linearThreshold ||
            angularVelocity > angularThreshold;

        if (rapidMovement)
            holdTimer = holdDuration;
        else
            holdTimer = Mathf.Max(0f, holdTimer - dt);

        float targetIntensity =
            holdTimer > 0f ? maxIntensity : 0f;

        float speed =
            targetIntensity > currentIntensity
                ? fadeInSpeed
                : fadeOutSpeed;

        currentIntensity = Mathf.MoveTowards(
            currentIntensity,
            targetIntensity,
            speed * dt
        );

        vignette.intensity.value = currentIntensity;
    }

    private void OnDisable()
    {
        if (vignette != null)
            vignette.intensity.value = 0f;
    }

    private void OnDestroy()
    {
        if (runtimeProfile != null)
            Destroy(runtimeProfile);
    }
}

// I HATE working with C# vro