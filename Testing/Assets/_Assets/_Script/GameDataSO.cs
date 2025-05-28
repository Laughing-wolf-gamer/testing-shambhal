using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


[CreateAssetMenu(fileName = "Game Data",menuName = "ScriptableObject/Game Data")]
public class GameDataSO : ScriptableObject {
    
    public PlayerSaveData saveData;
    public void setPhoneNumber(string phonNumber)
    {
        saveData.phonNumber = phonNumber;
    }
    public void setPassword(string password)
    {
        saveData.password = password;
    }
    public void SetHighScore(int currentScore)
    {
        if (currentScore > 0 && currentScore > saveData.highScore)
        {
            saveData.highScore = currentScore;
        }
    }
    public int GetHighestScore()
    {
        return saveData.highScore;
    }
    public string getPhoneNumber()
    {
        return saveData.phonNumber;
    }
    public string getPassword()
    {
        return saveData.password;
    }
    public void SetReviewd()
    {
        saveData.settingsSaveData.isReviewd = true;
    }
    public void SetHasAdsInGame(bool value){
        saveData.settingsSaveData.hasAdsInGame = value;
    }
    public bool GetHasAds(){
        return saveData.settingsSaveData.hasAdsInGame;
    }
    public bool IsRevived(){
        return saveData.settingsSaveData.isReviewd;
    }
    public void ToggelMusic(){
        saveData.settingsSaveData.isMusicOn = !saveData.settingsSaveData.isMusicOn;
    }
    public void ToggelSound(){
        saveData.settingsSaveData.isSoundOn = !saveData.settingsSaveData.isSoundOn;
    }
    public void ToggleHaptics()
    {
        saveData.settingsSaveData.hapticsOn = !saveData.settingsSaveData.hapticsOn;
    }
    public bool GetMusicState()
    {
        return saveData.settingsSaveData.isMusicOn;
    }
    public bool GetHapticsState()
    {
        return saveData.settingsSaveData.hapticsOn;
    }
    public bool GetSoundState()
    {
        return saveData.settingsSaveData.isSoundOn;
    }



    #region Saving and Loading................

    [ContextMenu("Save")]
    public void Save(){
        string data = JsonUtility.ToJson(saveData,true);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath,"/",name,"GameData",".dat"));
        formatter.Serialize(file,data);
        file.Close();
    }

    [ContextMenu("Load")]
    public void Load(){
        if(File.Exists((string.Concat(Application.persistentDataPath,"/",name,"GameData",".dat")))){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream Stream = File.Open(string.Concat(Application.persistentDataPath,"/",name,"GameData",".dat"),FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(Stream).ToString(),saveData);
            Stream.Close();
        }
    }

    #endregion
}
[System.Serializable]
public class PlayerSaveData{
    public string phonNumber;
    public string password;

    public int highScore;

    [Header("Settings")]
    public SettingsSaveData settingsSaveData;
}
[System.Serializable]
public class SettingsSaveData{
    public bool hasAdsInGame = true;
    public bool isReviewd;
    public bool hapticsOn;
    public bool isMusicOn;
    public bool isSoundOn;
}
