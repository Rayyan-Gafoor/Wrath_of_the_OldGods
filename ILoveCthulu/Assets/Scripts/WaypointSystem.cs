using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    [Range(0, 2f)]
    [SerializeField] private float waypoint__size = 1f;
    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypoint__size);
        }
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform get__next__waypoint(Transform current__waypoint)
    {
        if (current__waypoint == null)
        {
            return transform.GetChild(0);
        }
        if (current__waypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(current__waypoint.GetSiblingIndex() + 1);
        }
        else
        {
            return transform.GetChild(0);
        }
    }
}
