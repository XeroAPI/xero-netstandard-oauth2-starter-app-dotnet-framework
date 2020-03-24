using System;
using System.IO;
using System.Text.Json;
using Xero.NetStandard.OAuth2.Token;


public static class TokenUtilities
{
        
    public static void StoreToken(XeroOAuth2Token xeroToken)
    {
        string serializedXeroToken = JsonSerializer.Serialize(xeroToken);
        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = currentPath + "xerotoken.json";
        System.IO.File.WriteAllText(filePath, serializedXeroToken);


    }

    public static XeroOAuth2Token GetStoredToken()
    {
        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = currentPath + "xerotoken.json";
        string serializedXeroToken = System.IO.File.ReadAllText(filePath);
        var xeroToken = JsonSerializer.Deserialize<XeroOAuth2Token>(serializedXeroToken);

        return xeroToken;
    }

    public static bool TokenExists()
    {
        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = currentPath + "xerotoken.json";
        bool fileExist = File.Exists(filePath);

        return fileExist;
    }

}