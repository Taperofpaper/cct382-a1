using UnityEngine;

[CreateAssetMenu(menuName = "Game/My Data")]
public class HandshakeManager : ScriptableObject
{
    public float numHandshakes = 0f;

    public void ResetStats()
    {
        numHandshakes = 0f;
    }
}