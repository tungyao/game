using System;
using Nakama;
using UnityEngine;

namespace Script
{
    public static class GlobalData
    {
        public static UserData userData;
    }

    public class UserData
    {
        public string authToken;
        public bool created;
        public long expireTime;
        public string userName;
        public string userId;
    }
}