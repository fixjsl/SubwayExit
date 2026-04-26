using UnityEngine;

public interface Iinterectable
{
    bool isStuck { get; }
    string InteractMessage { get; }
    void Oninterect(Vector3 interacterPosition);
}
