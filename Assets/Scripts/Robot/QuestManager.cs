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
    private GameObject[] _beklenenİtemlerPrefablar = default;
    private GameObject spawnlanacakItemPrefab;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerClick()
    {
        if(!_questActive)
        {
            _questActive = true;
            //Fiş textini belirle
            var fisText = _allQuests[_currentQuestId]._fişVersiyonları[CurrentAnger];
            _currentQuestCountdown = _allQuests[_currentQuestId]._countdown;
            _currentlyExpectedItemId = _allQuests[_currentQuestId]._istenenItemId;
            _doesQuestSpawnItem = _allQuests[_currentQuestId].questBitinceİtemSpawn;
            _beklenenİtemlerPrefablar = _allQuests[_currentQuestId]._istenenİtemlerPrefab;
            spawnlanacakItemPrefab = _allQuests[_currentQuestId]._spawnlanacakİtemPrefab;
            _currentQuestId++;
            //Fiş textini spawnlanacak fiş prefabına ekle
            //Fiş animasyonunu oynat
            //Animasyon bitince fişi yerde spawnla
            //Countdown başlat

        }
    }
}
