using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheMissions : MonoBehaviour
{
    [Header("Spikes")]
    public GameObject spike1;
    public GameObject spike2;
    public GameObject spike3;
    public GameObject spike4;
    public GameObject spike5;
    public GameObject spikes;
    public Text spikeText;

    [Header("UI Canvases")]
    public GameObject canvasL1;
    public GameObject canvasL2;
    public GameObject nextLevelShow;
    public GameObject missionsCompletedGameUI;


    [Header("Energy Cell")]
    public GameObject energyCell;
    public GameObject energyCell_battery;
    public Text energyText;

    
    public static TheMissions occurrence;

    private int activeSpikeCount = 0;  // Aktif spike sayısını tutacak sayaç

    private void Awake()
    {
       occurrence = this;
    }

    void Start()
    {
        CheckActiveSpikes();
    }

    void Update()
    {
        CheckActiveSpikes();
    }

    private void CheckActiveSpikes()
    {
        int currentActiveSpikeCount = 0;
        if (!spike1.activeSelf) currentActiveSpikeCount++;
        if (!spike2.activeSelf) currentActiveSpikeCount++;
        if (!spike3.activeSelf) currentActiveSpikeCount++;
        if (!spike4.activeSelf) currentActiveSpikeCount++;
        if (!spike5.activeSelf) currentActiveSpikeCount++;

        if (currentActiveSpikeCount != activeSpikeCount)
        {
            activeSpikeCount = currentActiveSpikeCount;
            UpdateAmmoText(activeSpikeCount);

            if (currentActiveSpikeCount == 5)
            {
                HandleAllSpikesDeactivated();
            }
        }

        if (!energyCell_battery.activeSelf)
        {
            UpdateEnergyText();
        }
    }

    private void HandleAllSpikesDeactivated()
    {
        // Spike ve CanvasL1 kapat
        spikes.SetActive(false);
        canvasL1.SetActive(false);

        // EnergyCell ve CanvasL2 aç
        energyCell.SetActive(true);
        canvasL2.SetActive(true);

        // NextLevelShow canvas'ını 2 saniye boyunca açıp kapat
        StartCoroutine(ShowNextLevelCanvas());
    }

    private IEnumerator ShowNextLevelCanvas()
    {
        nextLevelShow.SetActive(true);
        yield return new WaitForSeconds(2);
        nextLevelShow.SetActive(false);
    }


    public void UpdateAmmoText(int count)
    {
        if (count == 1)
            spikeText.text = "Spike " + count + "  5";
        else
            spikeText.text = "Spike " + count + " 5";
    }

    public void UpdateEnergyText()
    {
       energyText.text = "Energy cell taken";
       StartCoroutine(ShowMissionCompletedAndEndUISequence());
    }

    private IEnumerator ShowMissionCompletedAndEndUISequence()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("EndMission");
        Cursor.lockState = CursorLockMode.None;
    }
}
