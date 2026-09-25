using UnityEngine;

[System.Serializable]
public class ZoneEntry
{
    public string zoneName;
    public Blockade gateBlockade;
    public Transform[] startPoints;
    public Transform[] exitPoints;
    public BaseReturnExit exitPrefab;
    public Transform entryPoint;

    public bool IsOpen => gateBlockade == null || !gateBlockade.gameObject.activeSelf;
}
