using UnityEngine;

public interface Iinterectable
{
    bool isStuck { get; }
    void Oninterect(Vector3 interacterPosition);
}
