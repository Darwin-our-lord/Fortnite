using System.Collections.Generic;
using UnityEngine;
public class LaserPointer : MonoBehaviour
{
    [Header("Beam appearance")]
    public Color laserColor = Color.red;
    public float laserWidth = 0.02f;
    public Material laserMaterial;

    [Header("Beam behaviour")]
    public int maxReflections = 8;
    public float maxDistance = 50f;
    public LayerMask hitLayers = Physics.DefaultRaycastLayers;
    public float reflectionBias = 0.001f;

    [Header("State")]
    public bool startActive = false;

    private LineRenderer _line;
    private bool _isActive;

    private bool turnedON = false;

    [Header("Cat:D")]
    public CatMovement catMov;


    private void Awake()
    {
        catMov = GameObject.FindWithTag("Cat").GetComponent<CatMovement>();

        _line = GetComponent<LineRenderer>();
        ConfigureLineRenderer();
        SetActive(startActive);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { turnedON = !turnedON;  catMov.StopChaseLaser(); }
        if (_isActive && turnedON)
            CastLaser();
        else
            _line.enabled = false;
    }

    public void SetActive(bool active)
    {
        _isActive = active;
        if (!active)
            _line.enabled = false;
    }

    private void CastLaser()
    {
        var points = new List<Vector3>();
        points.Add(transform.position);

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float remaining = maxDistance;

        for (int bounce = 0; bounce <= maxReflections; bounce++)
        {
            if (remaining <= 0f) break;

            bool hit = Physics.Raycast(origin, direction, out RaycastHit info, remaining, hitLayers);

            if (!hit)
            {
                points.Add(origin + direction * remaining);
                break;
            }

            points.Add(info.point);
            remaining -= info.distance;

            var mirror = info.collider.GetComponentInParent<Mirror>();
            if (mirror != null && bounce < maxReflections)
            {
                mirror.SetHit(true);
                direction = Vector3.Reflect(direction, info.normal).normalized;
                origin = info.point + direction * reflectionBias;
            }
            else
            {
                catMov.SetFollow(info.point);
                break;
            }
        }

        _line.positionCount = points.Count;
        _line.SetPositions(points.ToArray());
        _line.enabled = true;
    }

    private void ConfigureLineRenderer()
    {
        _line.useWorldSpace = true;
        _line.startWidth = laserWidth;
        _line.endWidth = laserWidth;
        _line.startColor = laserColor;
        _line.endColor = laserColor;
        _line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _line.receiveShadows = false;
        _line.positionCount = 0;

        if (laserMaterial != null)
        {
            _line.material = laserMaterial;
        }
        else
        {
            var mat = new Material(Shader.Find("Unlit/Color")) { color = laserColor };
            _line.material = mat;
        }
    }

    private void OnValidate()
    {
        if (_line == null) _line = GetComponent<LineRenderer>();
        if (_line != null)
        {
            _line.startWidth = laserWidth;
            _line.endWidth = laserWidth;
            _line.startColor = laserColor;
            _line.endColor = laserColor;
        }
    }
}