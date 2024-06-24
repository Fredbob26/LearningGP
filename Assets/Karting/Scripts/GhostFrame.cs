using UnityEngine;

[System.Serializable]
public class GhostFrame
{
    public Vector3 position;
    public Quaternion rotation;

    public GhostFrame(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}
