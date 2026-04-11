using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject ragdoll;
    [SerializeField] private GameObject animatedModel;

    private bool ragdollActive = false;

    private void Awake()
    {
        ragdoll.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ToggleRagdoll()
    {
        ragdollActive = !ragdollActive;

        if (ragdollActive)
        {
            CopyTransformData(animatedModel.transform, ragdoll.transform);
            ragdoll.gameObject.SetActive(true);
            animatedModel.gameObject.SetActive(false);
        }
        else
        {
            ragdoll.gameObject.SetActive(false);
            animatedModel.SetActive(true);
        }
    }

    private void CopyTransformData(Transform sourceTransform, Transform destinationTransform)
    {
        if (sourceTransform.childCount != destinationTransform.childCount)
        {
            Debug.LogWarning("Invalid transform copy, they need to match transform hierarchies");
        }

        for (int i = 0; i < sourceTransform.childCount; i++)
        {
            var source = sourceTransform.GetChild(i);
            var destination = destinationTransform.GetChild(i);
            destination.position = source.position;
            destination.rotation = source.rotation;
            var rbDest = destination.GetComponent<Rigidbody>();
            var rbSrc = source.GetComponent<Rigidbody>();
            if (rbDest != null && rbSrc != null)
            {
                rbDest.linearVelocity = rbSrc.linearVelocity;
            }

            CopyTransformData (source, destination);
        }
    }

}
