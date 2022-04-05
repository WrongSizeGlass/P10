
using UnityEngine;
using UnityEditor;
public class DrawGizmo : MonoBehaviour
{
    [SerializeField]
    protected float debugDrawRadius = 1.0f;
    
    public bool red;
    public bool green;
    public bool blue;
    public bool black;
    public bool white;
    public bool cyan;
    public bool yellow;
    // Visual aid for placing nodes in the scene
    public virtual void OnDrawGizmos()
    {
        if (red)
            Gizmos.color = Color.red;  
        if(green)
            Gizmos.color = Color.green;
        if(blue)
            Gizmos.color = Color.blue;
        if (black)
            Gizmos.color = Color.black;
        if(white)
            Gizmos.color = Color.white;
        if(cyan)
            Gizmos.color = Color.cyan;
        if (yellow)
            Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
