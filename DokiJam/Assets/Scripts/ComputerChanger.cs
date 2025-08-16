using UnityEngine;
using UnityEngine.UI;

public class ComputerChanger : MonoBehaviour
{
    public GameObject computerPanel;
    public GameObject[] viruses;
    public GameObject[] everythingElse;
    public GameObject noButton;
    public Sprite[] magicAdImages;
    private int currSpriteIndex = 0;

    public void ShutdownEverythingElse()
    {
        foreach (var item in everythingElse)
        {
            item.SetActive(false);
        }
    }

    public void DisplayAd(int num)
    {
        // // Instantiate first virus at random location within computerPanel
        // Vector2 panelSize = computerPanel.transform.position;
        // Vector2 rectSize = viruses[num].transform.position;

        // // Calculate min/max positions so it stays fully inside
        // float minX = -panelSize.x / 2 + rectSize.x / 2;
        // float maxX = panelSize.x / 2 - rectSize.x / 2;
        // float minY = -panelSize.y / 2 + rectSize.y / 2;
        // float maxY = panelSize.y / 2 - rectSize.y / 2;

        // // Pick a random anchored position
        // float randomX = Random.Range(minX, maxX);
        // float randomY = Random.Range(minY, maxY);

        // // Apply the new position
        // viruses[num].transform.position = new Vector2(randomX, randomY);
        viruses[num].gameObject.SetActive(true);
    }

    public void secondVirusStart()
    {
        YarnCommandHandler commandHandler = FindFirstObjectByType<YarnCommandHandler>();
        commandHandler.PlayYarn("workFinish2");
        viruses[0].SetActive(false);
    }

    public void thirdVirusStart()
    {
        YarnCommandHandler commandHandler = FindFirstObjectByType<YarnCommandHandler>();
        commandHandler.PlayYarn("workFinish3");
        viruses[1].SetActive(false);
    }

    public void itsTime()
    {
        YarnCommandHandler commandHandler = FindFirstObjectByType<YarnCommandHandler>();
        commandHandler.MeetGod();
        viruses[2].SetActive(false);
    }

    public void boo()
    {
        if (viruses[2].GetComponent<Image>().sprite != magicAdImages[4])
        {
            currSpriteIndex += 1;
            viruses[2].GetComponent<Image>().sprite = magicAdImages[currSpriteIndex];
        }
        else
        {
            itsTime();
        }
    }

    public void HideNoButton()
    {
        noButton.SetActive(false);
    }

    void Start()
    {
        // Hide all viruses at the start
        foreach (var virus in viruses)
        {
            virus.SetActive(false);
        }
    }
}
