using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace Cache.Redis
{
    public interface IRedisManager
    {
        bool DeleteHase(RedisKey key, RedisValue hashField);
        List<T> GetHashAll<T>(string key);
        T GetHashKey<T>(string key, string hasFildValue);
        List<T> GetHashKey<T>(string key, List<RedisValue> listhashFields);
        string GetMyKey(string resourceid = "");
        RedisValue GetStringKey(string key);
        RedisValue[] GetStringKey(List<RedisKey> listKey);
        T GetStringKey<T>(string key);
        List<T> HashGetAll<T>(string key);
        void HashSet<T>(string key, List<T> list, Func<T, string> getModelId);
        bool KeyDelete(string key);
        long keyDelete(RedisKey[] keys);
        bool KeyExists(string key);
        bool KeyRename(string key, string newKey);
        bool SetStringKey(KeyValuePair<RedisKey, RedisValue>[] arr);
        bool SetStringKey(string key, string value, TimeSpan? expiry = default(TimeSpan?));
        bool SetStringKey<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?));
        void StringAppend(string key, string value);

        TimeSpan? KeyTimeToLive(string key);
    }
}