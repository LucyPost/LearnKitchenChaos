using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMirrorer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform mirror;
    [SerializeField] private Axis axis;

    private enum Axis { X, Y, Z }
    // Update is called once per frame
    void Update()
    {
        Vector3 mirrorPosition = mirror.position;
        float distance;
        Vector3 distanceVec = Vector3.zero;

        switch (axis) {
            case Axis.X:
                distance = target.position.x - mirror.position.x;
                distanceVec = new Vector3(distance, 0, 0);
                break;
            case Axis.Y:
                distance = target.position.y - mirror.position.y;
                distanceVec = new Vector3(0, distance, 0);
                break;
            case Axis.Z:
                distance = target.position.z - mirror.position.z;
                distanceVec = new Vector3(0, 0, distance);
                break;
        }

        transform.position = target.position - 2.0f * distanceVec;
    }
}
