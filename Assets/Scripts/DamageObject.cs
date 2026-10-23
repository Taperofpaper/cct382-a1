using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class DamageObject : MonoBehaviour
{
    [SerializeField]
    private HandshakeManager handshakeManager;
    [SerializeField]
    private TMP_Text healthLabel;
    void OnTriggerEnter(Collider other)
    {
        handshakeManager.numHandshakes -= 0.5f;
        handshakeManager.numHandshakes = Mathf.Clamp(handshakeManager.numHandshakes, 0, 100);
        healthLabel.text = "Handshakes: " + handshakeManager.numHandshakes;
        print("Event fired! Handshakes = " + handshakeManager.numHandshakes);
    }
}
