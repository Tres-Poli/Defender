using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private float _sceneLoadPercents;
    [SerializeField] private int _configCount;
    [SerializeField] private Slider _slider;

    private int _loadedConfigCount;

    private TaskAwaiter<LevelProgressionConfig> _lvlProgressionConfigAwaiter;
    private TaskAwaiter<CharacterConfig[]> _charStatsConfigAwaiter;

    private AsyncOperation _sceneLoading;

    void Start()
    {
        var lvlProgressionLoader = new ConfigLoader<LevelProgressionConfig>(Application.dataPath, "/LevelProgressionConfig.json");
        var charStatsLoader = new ConfigLoader<CharacterConfig[]>(Application.dataPath, "/CharacterStatsConfig.json");

        var lvlProgressionLoadingTask = lvlProgressionLoader.Load();
        _lvlProgressionConfigAwaiter = lvlProgressionLoadingTask.GetAwaiter();
        _lvlProgressionConfigAwaiter.OnCompleted(() =>
        {
            GameSettings.LvlProgression = _lvlProgressionConfigAwaiter.GetResult();
            OnConfigLoaded();
        });

        var charStatsLoadingTask = charStatsLoader.Load();
        _charStatsConfigAwaiter = charStatsLoadingTask.GetAwaiter();
        _charStatsConfigAwaiter.OnCompleted(() =>
        {
            GameSettings.CharStats = _charStatsConfigAwaiter.GetResult();
            OnConfigLoaded();
        });

        _sceneLoading = SceneManager.LoadSceneAsync("Main");
        _sceneLoading.allowSceneActivation = false;
    }

    void Update()
    {
        var configsValue = (1f - _sceneLoadPercents) / _configCount * _loadedConfigCount;
        _slider.value = _sceneLoadPercents * _sceneLoading.progress + configsValue;
    }

    private void OnConfigLoaded()
    {
        Interlocked.Increment(ref _loadedConfigCount);
        Debug.Log("Config loaded");
        if (_loadedConfigCount >= _configCount)
        {
            _sceneLoading.allowSceneActivation = true;
            Debug.Log("Scene activated");
        }
    }
}
