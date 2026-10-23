using TMPro;
using UnityEngine;

/// <summary>
/// DEMO: Physics.Raycast
///
/// Attach this to any GameObject. Every frame, it casts a straight,
/// invisible line forward (the object's blue axis in the Scene view),
/// up to Max Distance, checking only Target Layer.
///
/// Run the scene, select this object in the Hierarchy, and rotate it:
/// the line turns from green to red the instant it crosses something,
/// with a small sphere marking exactly where it hit. If Feedback
/// Renderer is assigned, its material also changes colour live in the
/// Game view - no Scene view required to see it working.
/// </summary>
public class RaycastDemo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthLabel;
    [SerializeField] private HandshakeManager gameData;
    [Header("Detection Settings")]
    [Tooltip("How far forward to check.")]
    public float maxDistance = 5f;

    [Tooltip("Only objects on this layer will be detected.")]
    public LayerMask targetLayer;

    [Header("Live Feedback (optional)")]
    [Tooltip("Drag a Renderer here (e.g. this object's own) to see a colour change in the Game view too.")]
    public Renderer feedbackRenderer;
    public LineRenderer lineRenderer;
    public GameObject lineObject;
    public Color noHitColor = Color.white;
    public Color hitColor = Color.red;

    // Stored each frame so OnDrawGizmosSelected can show the same
    // result Update() just calculated, instead of running the query twice.
    private RaycastHit hitInfo;
    private bool didHit;
    private void Awake()
    {
        lineObject = new GameObject("RayLine");
        lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.3f;
        lineRenderer.numCapVertices = 10;
        lineRenderer.numCornerVertices = 10;

        lineRenderer.material = new Material(
            Shader.Find("Sprites/Default")
        );
    }
    private void Update()
    {
        // Ask Unity: following a straight line forward from this
        // object, what is the first Collider on targetLayer it hits,
        // within maxDistance? hitInfo is filled in only if this
        // returns true - that's what "out" means here.
        didHit = Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, targetLayer);

        if (feedbackRenderer != null)
        {
            feedbackRenderer.material.color = didHit ? hitColor : noHitColor;
        }

        if (didHit)
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.blue;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hitObject.transform.position);

            gameData.numHandshakes += 0.261f;
            healthLabel.text = "Ice melted: " + gameData.numHandshakes;
            float yScaleBefore = hitObject.transform.localScale.y;
            hitObject.transform.localScale *= 0.999f;
            float yScaleAfter = hitObject.transform.localScale.y;
            float yScaleDiff = yScaleBefore - yScaleAfter;
            hitObject.transform.position -= new Vector3(0, yScaleDiff*2f, 0);

            print(hitObject.name);
        } else
        {
            lineRenderer.startColor = Color.gray;
            lineRenderer.endColor = Color.gray;

            Vector3 endPoint = didHit ? hitInfo.point : transform.position + transform.forward * maxDistance;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPoint);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = didHit ? Color.red : Color.green;

        // If nothing was hit, draw the ray at its full potential length
        // instead of stopping at a (nonexistent) hit point.
        Vector3 endPoint = didHit ? hitInfo.point : transform.position + transform.forward * maxDistance;
        Gizmos.DrawLine(transform.position, endPoint);

        if (didHit)
        {
            Gizmos.DrawWireSphere(hitInfo.point, 0.15f);
        }
    }
}
