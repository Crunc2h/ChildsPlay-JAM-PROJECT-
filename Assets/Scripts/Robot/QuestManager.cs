using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(_questActive)
        {
            _timer -= Time.deltaTime;
            if(_timer % 1 == 0)
            {
                Debug.Log(_timer);
            }
            if(_timer <= 0)
            {
                Debug.Log("QuestFailed");
                CurrentAnger++;
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
        if(_doesQuestSpawnItem)
        {
            Instantiate(spawnlanacakItemPrefab, transform.position, transform.rotation);
        }
        _currentQuestId++;
        Debug.Log("QuestSuccessful");
        Debug.Log(_currentQuestId);
    }
}
