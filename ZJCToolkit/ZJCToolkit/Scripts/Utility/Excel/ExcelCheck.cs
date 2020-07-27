using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExcelCheck
{
    public static bool CheckContentType(ContentType type, string content)
    {
        switch (type)
        {
            case ContentType.stringValue:
                return true;
            case ContentType.None:
                return false;
            case ContentType.uint32:
                return TryParseUint32(content);
            case ContentType.int32:
                return TryParseInt32(content);
            case ContentType.int64:
                return TryParseInt64(content);
            case ContentType.floatValue:
                return TryParseFloat(content);
            case ContentType.IntList:
                return TryParseIntList(content);
            case ContentType.IntListJingHao:
                return TryParseIntListJingHao(content);
            case ContentType.IntListJingHaoMeiYuan:
                return TryParseIntListJingHaoMeiYuan(content);
            case ContentType.IntListList:
                return TryParseIntListList(content);
            case ContentType.IntListXiaHuaXian:
                return TryParseIntListXiaHuaXian(content);
            case ContentType.IntListXiaHuaXianFenHao:
                return TryParseIntListXiaHuaXian(content);
            case ContentType.StringList:
                return TryParseStringList(content);
        }
        return false;
    }

    private static bool TryParseUint32(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        uint i = 0;
        return uint.TryParse(content, out i);
    }
    private static bool TryParseInt32(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        int i = 0;
        return int.TryParse(content, out i);
    }
    private static bool TryParseInt64(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        long i = 0;
        return long.TryParse(content, out i);
    }

    private static bool TryParseFloat(string content)
    {

        if (string.IsNullOrEmpty(content)) return true;
        float i = 0;
        return float.TryParse(content, out i);
    }

    /// <summary>
    /// 格式IntList : 0/1
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static bool TryParseIntList(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        string[] strs = content.Split('/');
        int i = 0;
        foreach (var str in strs)
        {
            if (int.TryParse(str, out i) == false) return false;
        }
        return true;
    }

    /// <summary>
    /// 格式0#0
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static bool TryParseIntListJingHao(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        string[] strs = content.Split('#');
        int i = 0;
        foreach (var str in strs)
        {
            if (int.TryParse(str, out i) == false) return false;
        }
        return true;
    }

    /// <summary>
    /// 格式0#1$2#3
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static bool TryParseIntListJingHaoMeiYuan(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        string[] strs = content.Split('&');
        string[] strs2;

        int i = 0;
        foreach (var str in strs)
        {
            strs2 = str.Split('#');

            foreach (var str2 in strs2)
            {
                if (int.TryParse(str2, out i) == false) return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 格式1#2&3#4
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static bool TryParseIntListList(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        string[] strs = content.Split('&');
        string[] strs2;

        int i = 0;
        foreach (var str in strs)
        {
            strs2 = str.Split('#');

            foreach (var str2 in strs2)
            {
                if (int.TryParse(str2, out i) == false) return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 格式1_2
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static bool TryParseIntListXiaHuaXian(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        string[] strs = content.Split('_');
        int i = 0;
        foreach (var str in strs)
        {
            if (int.TryParse(str, out i) == false) return false;
        }
        return true;
    }

    /// <summary>
    /// 格式1_2;3_4
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static bool TryParseIntListXiaHuaXianFenHao(string content)
    {
        if (string.IsNullOrEmpty(content)) return true;
        string[] strs = content.Split(';');
        string[] strs2;

        int i = 0;
        foreach (var str in strs)
        {
            strs2 = str.Split('_');

            foreach (var str2 in strs2)
            {
                if (int.TryParse(str2, out i) == false) return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 格式str1/str2,基本上都是符合要求的
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private static bool TryParseStringList(string content)
    {
        return true;
    }
}
