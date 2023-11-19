using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject fişPrefab = null;
    public float[] _sinireGöreGörevlerArasıCountdown = null;
    public int[] _changeStateItemIds = null;
    public int CurrentAnger { get; private set; } = 0;
    public Quest[] _allQuests = new Quest[16];


    public string _currentFişString { get; private set; } = string.Empty;
    private int _stateNumber = 0;
    private int _currentQuestId = default;
    private int _currentlyExpectedItemId = default;
    private float _timer = default;
    private float _currentQuestCountdown = default;
    private bool _doesQuestSpawnItem = false;
    private bool _questActive = false;
    private bool _questCountdownOn = false;
    private bool _interumCountdownOn = false;
    private bool _takingQuest = false;
    
    private GameObject spawnlanacakItemPrefab;

    private GameObject _player;
    private Animator _anim;
    private TextMeshProUGUI _countdownTextMesh;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _countdownTextMesh = GameObject.FindGameObjectWithTag("Countdown").GetComponent<TextMeshProUGUI>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        AdjustCountdown();
        if(_anim.GetInteger("statenumber") != _stateNumber)
        {
            _anim.SetInteger("statenumber", _stateNumber);
        }
        if(!_takingQuest)
        {
            if (_interumCountdownOn && !_questActive)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    InterumFailure();
                    if (CurrentAnger >= 3)
                    {
                        GameOver();
                    }
                }
            }
            else if (!_interumCountdownOn && _questActive)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    QuestFailure();
                    if (CurrentAnger >= 3)
                    {
                        GameOver();
                    }
                }
            }
        }
    }

    private static void GameOver()
    {
        Debug.Log("GameOver");
    }
    private void QuestFailure()
    {
        Debug.Log("QuestFailed");
        CurrentAnger++;
        AdjustAngerUI();
        _questActive = false;
    }
    private void InterumFailure()
    {
        CurrentAnger++;
        AdjustAngerUI();
        _interumCountdownOn = false;
    }

    public void PlayerClick()
    {
        Debug.Log(_player.GetComponent<ItemInteraction>().interactionActive);
        Debug.Log(_player.GetComponent<ItemInteraction>().ActiveItemID);
        if (!_questActive && !_player.GetComponent<ItemInteraction>().interactionActive)
        {
            _takingQuest = true;
            GetQuestProperties();
            Debug.Log(_currentlyExpectedItemId);






            //Fiş animasyonunu oynat
            _anim.SetTrigger("FişAt");

            
            
            Debug.Log("QuestTaken");
            Debug.Log(_currentFişString);
        }
        else if(_questActive 
            && _player.GetComponent<ItemInteraction>().interactionActive 
            && _player.GetComponent<ItemInteraction>().ActiveItemID == _currentlyExpectedItemId)
        {     
            if(_changeStateItemIds.Contains(_player.GetComponent<ItemInteraction>().ActiveItemID))
            {
                _anim.SetTrigger("changestate");
                _stateNumber++;
            }
            QuestSuccessful();
        }
        else
        {
            //Anlamsız tıklama, sfx konulabilir
            Debug.Log("Anlamsız interaksyon");
        }

    }

    private void GetQuestProperties()
    {
        _currentFişString = _allQuests[_currentQuestId]._fişVersiyonları[CurrentAnger];
        _currentQuestCountdown = _allQuests[_currentQuestId]._countdown;
        _currentlyExpectedItemId = _allQuests[_currentQuestId]._istenenItemId;
        _doesQuestSpawnItem = _allQuests[_currentQuestId].questBitinceİtemSpawn;
        spawnlanacakItemPrefab = _allQuests[_currentQuestId]._spawnlanacakİtemPrefab;
    }

    public void StartQuest()
    {
        _questActive = true;
        _interumCountdownOn = false;
        _takingQuest = false;
        var newfişToSpawn = Instantiate(fişPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
        newfişToSpawn.SetActive(true);
    }
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
        var angerBar = GameObject.FindGameObjectWithTag("AngerBar").transform.GetChild(0);
        for(int i = 0; i < 3; i++)
        {
            if(i <= CurrentAnger - 1)
            {
                Color darkRed = new Color(109, 0, 0);
                darkRed.a = 1;
                angerBar.transform.GetChild(i).GetComponent<Image>().color = darkRed;

            }
            else
            {
                Color transparent = new Color(1, 1, 1);
                transparent.a = 0;
                angerBar.transform.GetChild(i).GetComponent<Image>().color = transparent;
            }
        }
    }
    private void AdjustCountdown()
    {
        if(!_interumCountdownOn && !_questActive)
        {
            if (!_countdownTextMesh.enabled)
            {
                _countdownTextMesh.enabled = true;
            }
            _interumCountdownOn = true;
            _timer = _sinireGöreGörevlerArasıCountdown[CurrentAnger];
        }

        if (!_interumCountdownOn && _questActive)
        {
            if (!_countdownTextMesh.enabled)
            {
                _countdownTextMesh.enabled = true;
            }
            if(!_questCountdownOn)
            {
                _questCountdownOn = true;
                _timer = _currentQuestCountdown;
            }
        }
        if(!_takingQuest)
        {
            _countdownTextMesh.text = ((int)_timer).ToString();
        }
        else
        {
            _countdownTextMesh.text = string.Empty;
        }
    }
}
