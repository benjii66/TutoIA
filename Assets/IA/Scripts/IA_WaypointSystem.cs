using System.Collections.Generic;
using UnityEngine;

public class IA_WaypointSystem : MonoBehaviour
{
    [SerializeField] List<Vector3> wayPoint = new List<Vector3>();
    [SerializeField] int indexPoint = -1;

    public Vector3 PickPoint()
    {       
        //ça rentre deux fois
        indexPoint++;
        indexPoint %= wayPoint.Count;
        return wayPoint[indexPoint];
    }


    public void AddPoint()
    {
        //si aucun point on retourne vector zero sinon on compte -1 et on fait avancer
        Vector3 _point = wayPoint.Count == 0 ? Vector3.zero : wayPoint[wayPoint.Count - 1] + Vector3.forward;
        wayPoint.Add(_point);
    }


    public void Clear() => wayPoint.Clear();

    private void OnDrawGizmos()
    {
        for (int i = 0; i < wayPoint.Count; i++)
        {
            if (i < wayPoint.Count - 1) Gizmos.DrawLine(wayPoint[i], wayPoint[i + 1]);
            Gizmos.DrawWireCube(wayPoint[i], Vector3.one);
        }
    }
}
