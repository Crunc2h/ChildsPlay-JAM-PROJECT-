using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject fişPrefab = null;
    public Quest[] _allQuests = new Quest[16];
    public int CurrentAnger { get; private set; } = 0;
    private bool _questActive = false;
    private bool _doesQuestSpawnItem = false;
    private int _currentQuestId = default;
    private float _currentQuestCountdown = default;
    private int _currentlyExpectedItemId = default;
    private GameObject spawnlanacakItemPrefab;
    private float _timer = default;
    private GameObject _player;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        AdjustCountdown();
        if(_questActive)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                Debug.Log("QuestFailed");
                CurrentAnger++;
                AdjustAngerUI();
                _questActive = false;
                if(CurrentAnger >= 3)
                {
                    Debug.Log("GameOver");
                }
            }
        }
    }

    public void PlayerClick()
    {
        if(!_questActive && !_player.GetComponent<ItemInteraction>().interactionActive)
        {
            //Fiş textini belirle
            var fisText = _allQuests[_currentQuestId]._fişVersiyonları[CurrentAnger];
            _currentQuestCountdown = _allQuests[_currentQuestId]._countdown;
            _currentlyExpectedItemId = _allQuests[_currentQuestId]._istenenItemId;
            _doesQuestSpawnItem = _allQuests[_currentQuestId].questBitinceİtemSpawn;
            spawnlanacakItemPrefab = _allQuests[_currentQuestId]._spawnlanacakİtemPrefab;
            _timer = _currentQuestCountdown;;


            //Fiş textini spawnlanacak fiş prefabına ekle
            //Fiş animasyonunu oynat

            //Animasyon bitince fişi yerde spawnla
            //Animasyon eventiyle questi başlat
            StartQuest();
            Debug.Log("QuestTaken");
            Debug.Log(fisText);
        }
        else if(_questActive && 
            _player.GetComponent<ItemInteraction>().interactionActive &&
            _player.GetComponent<ItemInteraction>().ActiveItemID == _currentlyExpectedItemId)
        {
            
            QuestSuccessful();
        }
        else
        {
            //Anlamsız tıklama, sfx konulabilir
            Debug.Log("Anlamsız interaksyon");
        }

    }
    private void StartQuest() => _questActive = true;
    private void QuestSuccessful()
    {
        _questActive = false;
        if(CurrentAnger > 0)
        {
            CurrentAnger--;
        }
        if(_doesQuestSpawnItem)
        {
            Instantiate(spawnlanacakItemPrefab, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), transform.rotation);
        }
        var parentSlot = _player.GetComponent<ItemInteraction>().CurrentItemSlot;
        var inventory = _player.GetComponent<Inventory>();
        var itemIcon = parentSlot.transform.GetChild(1).gameObject;
        
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i] == parentSlot)
            {
                inventory.isFull[i] = false;
            }
        }
        _currentQuestId++;
        Destroy(itemIcon);
        _player.GetComponent<ItemInteraction>().interactionActive = false;
        _player.GetComponent<ItemInteraction>().ActiveItemID = default;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInteraction>().CurrentItemSlot = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        Debug.Log("QuestSuccessful");
        Debug.Log(_currentQuestId);
    }

    private void AdjustAngerUI()
    {
        var angerBar = GameObject.FindGameObjectWithTag("AngerBar");
        for(int i = 0; i < 3; i++)
        {
            if(i <= CurrentAnger - 1)
            {
                angerBar.transform.GetChild(i).GetComponent<Image>().color = Color.red;
            }
            else
            {
                angerBar.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    }
    private void AdjustCountdown()
    {
        if(_questActive)
        {
            if(!GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>().enabled)
            {
                GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>().enabled = true;
            }
            GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>().text = _timer.ToString();
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>().enabled)
            {
                GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>().text = string.Empty;
                GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}
