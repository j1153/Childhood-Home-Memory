using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ProximityTrigger : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    public UnityEvent onTriggered;

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            onTriggered?.Invoke();
        }
    }
}