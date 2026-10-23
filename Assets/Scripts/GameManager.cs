using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HandshakeManager handshakeManager;
    private void OnApplicationQuit()
    {
        handshakeManager.ResetStats();
    }
}
