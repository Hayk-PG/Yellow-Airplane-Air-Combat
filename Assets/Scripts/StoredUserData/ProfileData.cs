using UnityEngine;
using PlayFab.ClientModels;

public class ProfileData : MonoBehaviour
{
    public static ProfileData Manager { get; private set; }

    public GetLeaderboardResult LeaderboardResult { get; private set; }

    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }

    public string SavedUsername
    {
        get => PlayerPrefs.GetString(Keys.Username);
        private set => PlayerPrefs.SetString(Keys.Username, value);
    }
    public string SavedPassword
    {
        get => PlayerPrefs.GetString(Keys.Password);
        private set => PlayerPrefs.SetString(Keys.Password, value);
    }

    public bool IsCreatingAccountPromptDisabled
    {
        get => PlayerPrefs.GetInt(Keys.AccountCreationAskPrompt, 1) < 1;
        private set => PlayerPrefs.SetInt(Keys.AccountCreationAskPrompt, value ? 0 : 1);
    }
    public bool IsLoginReminderDisabled
    {
        get => PlayerPrefs.GetInt(Keys.LoginReminder, 1) < 1;
        private set => PlayerPrefs.SetInt(Keys.LoginReminder, value ? 0 : 1);
    }

    public bool IsAutoLoginEnabled => !string.IsNullOrEmpty(SavedUsername) && !string.IsNullOrEmpty(SavedPassword);




    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public void CacheUserCredentials(string username, string password, string email = null)
    {
        Username = username;
        Password = password;
        Email = email != null ? email : Email;
    }

    public void CacheLeaderboardResult(GetLeaderboardResult leaderboardResult)
    {
        LeaderboardResult = leaderboardResult;
    }

    public void SaveOrDeleteUserCredentials(string username, string password, bool delete = false)
    {
        if (delete)
        {
            PlayerPrefs.DeleteKey(Keys.Username);
            PlayerPrefs.DeleteKey(Keys.Password);
            return;
        }

        SavedUsername = username;
        SavedPassword = password;
    }

    public void SetAccountCreationAskPromptState(bool isDisabled)
    {
        IsCreatingAccountPromptDisabled = isDisabled;
    }

    public void SetLoginReminderState(bool isDisabled)
    {
        IsLoginReminderDisabled = isDisabled;
    }

    public void ResetToDefault()
    {
        SaveOrDeleteUserCredentials(null, null, true);
        SetAccountCreationAskPromptState(false);
        SetLoginReminderState(false);

        Username = null;
        Password = null;
        Email = null;

        LeaderboardResult = null;
    }
}