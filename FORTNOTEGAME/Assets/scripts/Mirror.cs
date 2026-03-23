using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Mirror : MonoBehaviour
{
    [Range(0f, 1f)]
    public float reflectivity = 1f;

    private bool _isBeingHit;

    public void SetHit(bool hit)
    {
        if (_isBeingHit == hit) return;
        _isBeingHit = hit;
    }

    private void OnDisable()
    {
        SetHit(false);
    }
}