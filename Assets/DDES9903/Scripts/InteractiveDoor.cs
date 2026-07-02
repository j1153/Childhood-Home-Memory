using UnityEngine;
using UnityEngine.Events;

public class InteractiveDoor : MonoBehaviour
{
    [SerializeField] private Transform doorChild;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float smoothSpeed = 5f;

    public UnityEvent onOpened;
    public UnityEvent onClosed;

    private bool isOpen = false;
    private Quaternion closeRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (doorChild == null && transform.childCount > 0)
            doorChild = transform.GetChild(0);

        if (doorChild != null)
        {
            closeRotation = doorChild.localRotation;
            openRotation = closeRotation * Quaternion.Euler(0, openAngle, 0);
        }
    }

    void Update()
    {
        if (doorChild != null)
        {
            Quaternion targetRotation = isOpen ? openRotation : closeRotation;
            doorChild.localRotation = Quaternion.Slerp(doorChild.localRotation, targetRotation, Time.deltaTime * smoothSpeed);
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            onOpened?.Invoke();
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            onClosed?.Invoke();
        }
    }
}