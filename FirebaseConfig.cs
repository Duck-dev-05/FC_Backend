using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace FootballClubBE
{
    public static class FirebaseConfig
    {
        public static void InitializeFirebase()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("D:\\Code\\FootballClubWebsite\\FootballClubBE\\Credentials\\SerivcesKey.json"), // Ensure the path is correct
            });
        }
    }
} 