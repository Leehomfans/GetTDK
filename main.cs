public void GetTDKInfo(HttpContext context)
    {
        string website = DataConvert.ToString(context.Request["website"]);
        string resHtml = GetRequestHtml(website);
        TDKInfo info = new TDKInfo();
        Regex reg = new Regex("title>(.+)<");
        //获取title
        Match match = reg.Match(resHtml);
        info.title = match.Groups[1].Value;
        Regex reg1 = new Regex("description(.+)content=\"(\\S+)\"");
        //获取description
        Match match1 = reg1.Match(resHtml);
        info.description = match1.Groups[2].Value;
        Regex reg2 = new Regex("keywords(.+)content=\"(\\S+)\"");
        //获取keyword
        Match match2 = reg2.Match(resHtml);
        info.keyword = match2.Groups[2].Value;
        Regex reg3 = new Regex("applicable-device(.+)content=\"(\\S+)\"");
        //获取meta application-device是否包含mobile,是否可以移动端自适应
        Match match3 = reg3.Match(resHtml);
        string meta = match3.Groups[2].Value;
        if (!string.IsNullOrEmpty(meta))
        {
            info.isAdaptive = match3.Groups[2].Value.Contains("mobile");
        }
        WriteString(context, true, "", info);
    }
    /// <summary>
    /// 获取页面html文本
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public string GetRequestHtml(string url)
    {
        string strResult = string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        request.Headers.Add("Accept-Language", "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 Safari/537.36";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream streamReceive = response.GetResponseStream();
        StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
        strResult = streamReader.ReadToEnd();
        streamReader.Close();
        streamReader.Dispose();
        streamReader.Close();
        streamReader.Dispose();
        return strResult;
    }
