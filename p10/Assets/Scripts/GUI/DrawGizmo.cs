
using UnityEngine;
using UnityEditor;
public class DrawGizmo : MonoBehaviour
{
    [SerializeField]
    protected float debugDrawRadius = 1.0f;

    // Visual aid for placing nodes in the scene
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
