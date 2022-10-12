using UnityEngine;

public class OffScreenDestroy : MonoBehaviour
{

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
