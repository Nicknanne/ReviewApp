using System.Diagnostics;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;

namespace ReviewApp.Services;

public class SecureSessionPersistence : IGotrueSessionPersistence<Session>
{
    private const string SessionKey = "supabase_session";

    public Session? LoadSession()
    {
        try
        {
            var json = Preferences.Get(SessionKey, null);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonConvert.DeserializeObject<Session>(json);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void SaveSession(Session session)
    {
        var json = JsonConvert.SerializeObject(session);
        Preferences.Set(SessionKey, json);
    }

    public void DestroySession()
    {
        Preferences.Remove(SessionKey);
    }
}