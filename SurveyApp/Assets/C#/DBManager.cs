using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string username;
    public static string role;

    public static bool LoggedIn { get { return username != null; } }

    public static void LogOut() {
        username = null;
    }
}
