using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Vector3 closedPosition;
    private float currentAngle = 0f;
    private Vector3 hinge;

    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private bool isOpen = false;

    void Start()
    {
        closedRotation = transform.rotation;
        closedPosition = transform.position;

        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));

        if (hinge == Vector3.zero)
        {
            hinge = transform.position + new Vector3(-1, 0, 0);
        }
    }

    void Update()
    {
        if (isOpen)
        {
            currentAngle = Mathf.MoveTowards(currentAngle, 1f, openSpeed * Time.deltaTime);
        }
        else
        {
            currentAngle = Mathf.MoveTowards(currentAngle, 0f, openSpeed * Time.deltaTime);
        }

        Quaternion targetRotation = Quaternion.Slerp(closedRotation, openRotation, currentAngle);

        transform.rotation = targetRotation;

        Vector3 hingeOffset = closedPosition - hinge;

        Quaternion rotationDelta = targetRotation * Quaternion.Inverse(closedRotation);

        transform.position = hinge + (rotationDelta * hingeOffset);
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hinge, 0.1f);
    }
}
