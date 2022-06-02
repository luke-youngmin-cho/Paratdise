using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인터넷 연결 확인
/// </summary>
public static class InternetConnection
{
    public static string googleURI = "http://google.com";
    public static string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }

    public static bool IsGoogleWebsiteReachable()
    {
        bool isReachable = false;
        string HtmlText = GetHtmlFromUri(googleURI);
        if (HtmlText == "")
        {
            // 연결없음
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            //Redirecting since the beginning of googles html contains that 
            //phrase and it was not found
        }
        else
        {
            // 성공
            isReachable = true;
        }
        return isReachable;
    }
}
