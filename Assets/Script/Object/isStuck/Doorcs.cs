using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Doorcs : ItObjectBase
{
    public override bool isStuck => false;

    private Quaternion targetRot;
    private Quaternion closedRot;

    private bool isOpen = false;
    private void Awake()
    {
        closedRot = transform.rotation;
        targetRot = closedRot;
    }
    protected override void OnInteractInternal(Vector3 interacterPosition)
    {
        if (!isOpen)
        {
            float dot = Vector3.Dot(transform.forward,
                (interacterPosition - transform.position).normalized);
            float angle = dot >= 0 ? 90f : -90f;
            targetRot = closedRot * Quaternion.Euler(0f, angle, 0f);
            isOpen = true;
        }
        else
        {
            targetRot = closedRot;
            isOpen = false;
        }

    }
    private void Update()
    {
        if (transform.rotation == targetRot) return;

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, targetRot, 180f * Time.deltaTime);

        if (transform.rotation == targetRot)
            isInteracting = false;
    }

}

