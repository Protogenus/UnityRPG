using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup deathPanel;
    [SerializeField] private Text deathText;
    [SerializeField] private Button respawnButton;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.5f;

    private void Start()
    {
        if (deathPanel == null || deathText == null || respawnButton == null)
        {
            Debug.LogError("DeathManager is missing a UI reference!");
            enabled = false;
            return;
        }

        // Hide everything at start
        deathPanel.alpha = 0f;
        deathPanel.gameObject.SetActive(false);
        deathText.canvasRenderer.SetAlpha(0f);
        respawnButton.gameObject.SetActive(false);

        respawnButton.onClick.AddListener(RespawnPlayer);
    }

    public void TriggerDeath()
    {
        Debug.Log("TriggerDeath called — starting death UI sequence.");

        Time.timeScale = 0f;
        deathPanel.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            deathPanel.alpha = t;
            deathText.canvasRenderer.SetAlpha(t);

            yield return null;
        }

        deathPanel.alpha = 1f;
        deathText.canvasRenderer.SetAlpha(1f);
        respawnButton.gameObject.SetActive(true);
    }

    private void RespawnPlayer()
    {
        Debug.Log("Respawn button clicked — resetting game.");

        Time.timeScale = 1f;

        deathPanel.alpha = 0f;
        deathText.canvasRenderer.SetAlpha(0f);
        respawnButton.gameObject.SetActive(false);
        deathPanel.gameObject.SetActive(false);

        PlayerHealth player = FindFirstObjectByType<PlayerHealth>();
        if (player != null)
        {
            player.Respawn();
            Debug.Log("Player respawned successfully.");
        }
        else
        {
            Debug.LogWarning("No PlayerHealth found — cannot respawn player!");
        }
    }
}
