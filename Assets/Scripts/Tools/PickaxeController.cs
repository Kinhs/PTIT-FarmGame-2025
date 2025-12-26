using UnityEngine;

public class PickaxeController : MonoBehaviour
{
    [Header("Angles")]
    [SerializeField] private float windUpAngle = 30f;
    [SerializeField] private float chopAngle = -90f;

    [Header("Speeds")]
    [SerializeField] private float windUpLerpSpeed = 8f;
    [SerializeField] private float chopAngularSpeed = 720f;
    [SerializeField] private float recoverAngularSpeed = 540f;

    [Header("Timing")]
    [SerializeField] private float useCooldown = 0.4f;

    [Header("Hitbox")]
    [SerializeField] private Collider2D chopCollider;

    private Quaternion initialRotation;
    private Quaternion windUpRotation;
    private Quaternion chopRotation;

    private bool isWindUp;
    private bool isChopping;
    private bool isRecovering;

    private float cooldownTimer;

    public bool IsReady =>
        !isWindUp &&
        !isChopping &&
        !isRecovering &&
        cooldownTimer <= 0f;

    private void Awake()
    {
        initialRotation = transform.localRotation;
        windUpRotation = initialRotation * Quaternion.Euler(0f, 0f, windUpAngle);
        chopRotation = initialRotation * Quaternion.Euler(0f, 0f, chopAngle);

        if (chopCollider != null)
            chopCollider.enabled = false;
    }

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (isWindUp)
        {
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                windUpRotation,
                windUpLerpSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.localRotation, windUpRotation) < 1f)
            {
                transform.localRotation = windUpRotation;
                isWindUp = false;
                isChopping = true;

                if (chopCollider != null)
                    chopCollider.enabled = true;
            }
        }
        else if (isChopping)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                chopRotation,
                chopAngularSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.localRotation, chopRotation) < 0.5f)
            {
                transform.localRotation = chopRotation;
                isChopping = false;
                isRecovering = true;

                if (chopCollider != null)
                    chopCollider.enabled = false;
            }
        }
        else if (isRecovering)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                initialRotation,
                recoverAngularSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.localRotation, initialRotation) < 0.5f)
            {
                transform.localRotation = initialRotation;
                isRecovering = false;
                cooldownTimer = useCooldown;
            }
        }
    }

    public void Use()
    {
        if (!IsReady)
            return;

        isWindUp = true;
    }
}
