using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace APNet
{
    public class HttpRequest : IDisposable
    {
        private HttpWebRequest request;// Make a private HttpWebRequest
        private HttpWebResponse response;// Make a private HttpWebResponse
        private WebResponse response_error;// Make a private WebResponse (For sometime, Request Give error for example error-->400,404,500 and etc)
        private string html;// Make a private string to send page source in it
        public bool KeepAlive = true;// Use Public bool for user control KeepAlive 
        public bool Expect100Continue=false;// Use Public bool for user control Expect100Continue
        public bool UseNagleAlgorithm = false;// Use Public bool for user control UseNagleAlgorithm
        public bool AllowAutoRedirect = true;// Use Public bool for user control AllowAutoRedirect
        public string Accept = "*/*";// Use Public string for user control Accept
        public string Accept_Encoding = "gzip, deflate";// Use Public string for user control Accept_Encoding
        public string Accept_Language = "en-US,en;q=0.5";// Use Public string for user control Accept_Language
        public string UserAgent ="Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";// Use Public string for user control UserAgent
        public int Timeout = 1500;// Use Public int for user control Timeout
        public int ConnectionLimit = 10000;// Use Public int for user control ConnectionLimit
        private bool UseProxy = false;// A Private bool for --> if user need Proxy change it to true and use Proxy
        private bool haserror = false;// Its use in GetResponseHeader for get error Response Header  (For sometime, Request Give error for example error-->400,404,500 and etc)
        private WebProxy proxyip;// Make a private WebProxy for use Proxy in HttpWebRequest
        private Dictionary<string, string> CHeader = new Dictionary<string, string>();// A Dictionary To add custom Header
        private Dictionary<string, string> Param = new Dictionary<string, string>();// A Dictionary To add Parameter
        private CookieContainer Cookiereq = new CookieContainer();//Add custom Cookie

        public void Dispose()
        {



        }
        public string Get(string url)// GET Method
        {
            
            try
            {

                request = (HttpWebRequest)WebRequest.Create(url);
                if (Cookiereq.Count > 0)
                {
                    request.CookieContainer = Cookiereq;
                }
                if (UseProxy == true)
                {
                    request.Proxy = proxyip;

                }
                else
                {
                    request.Proxy = null;
                }
                request.Timeout = Timeout;
                request.Method = "GET";


                request.ContentType = "application/x-www-form-urlencoded";
                if (CHeader.Count > 0)
                {
                    foreach (var Header in CHeader)
                    {
                        request.Headers[Header.Key] = Header.Value;
                    }
                }
                request.KeepAlive = KeepAlive;
                request.Accept = Accept;
                request.Headers.Add("Accept-Encoding", Accept_Encoding);
                request.Headers.Add("Accept-Language", Accept_Language);
                request.ServicePoint.Expect100Continue = Expect100Continue;
                request.ServicePoint.ConnectionLimit = ConnectionLimit;
                request.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                request.AllowAutoRedirect = AllowAutoRedirect;

                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                response_error = e.Response;
                haserror = true;

                response = (HttpWebResponse)response_error;

                using (Stream data1 = response.GetResponseStream())
                using (var reader1 = new StreamReader(data1))
                {
                    html = reader1.ReadToEnd();

                }

            }
            return html;
        }

        public string Get(string url, string ContentType = "application/x-www-form-urlencoded")// GET Method
        {
            
            try
            {

                request = (HttpWebRequest)WebRequest.Create(url);
                if (Cookiereq.Count > 0)
                {
                    request.CookieContainer = Cookiereq;
                }
                if (UseProxy == true)
                {
                    request.Proxy = proxyip;

                }
                else
                {
                    request.Proxy = null;
                }
                request.Timeout = Timeout;
                request.Method = "GET";

                
                request.ContentType = ContentType;
                if (CHeader.Count > 0)
                {
                    foreach (var Header in CHeader)
                    {
                        request.Headers[Header.Key] = Header.Value;
                    }
                }
                request.KeepAlive = KeepAlive;
                request.Accept = Accept;
                request.Headers.Add("Accept-Encoding", Accept_Encoding);
                request.Headers.Add("Accept-Language", Accept_Language);
                request.ServicePoint.Expect100Continue = Expect100Continue;
                request.ServicePoint.ConnectionLimit = ConnectionLimit;
                request.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                request.AllowAutoRedirect = AllowAutoRedirect;

                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                response_error = e.Response;
                haserror = true;

                response = (HttpWebResponse)response_error;

                using (Stream data1 = response.GetResponseStream())
                using (var reader1 = new StreamReader(data1))
                {
                    html = reader1.ReadToEnd();

                }

            }
            return html;
        }

        public string Post(string url)// POST Method
        {
           
            try
            {

                request = (HttpWebRequest)WebRequest.Create(url);
                if (Cookiereq.Count > 0)
                {
                    request.CookieContainer = Cookiereq;
                }
                if (UseProxy == true)
                {
                    request.Proxy = proxyip;
                }
                else
                {
                    request.Proxy = null;
                }
                request.Timeout = Timeout;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                if (CHeader.Count > 0)
                {
                    foreach (var Header in CHeader)
                    {
                        request.Headers[Header.Key] = Header.Value;
                    }
                }
                string data = null;
                if (Param.Count > 0)
                {
                    foreach (var item in Param)
                    {
                        data += item.Key + "=" + item.Value + "&";
                    }
                }

                byte[] dataToSend = Encoding.UTF8.GetBytes(data);
                request.ContentLength = dataToSend.Length;
                request.KeepAlive = KeepAlive;
                request.Accept = Accept;
                request.Headers.Add("Accept-Encoding", Accept_Encoding);
                request.Headers.Add("Accept-Language", Accept_Language);
                request.UserAgent = UserAgent;
                request.ServicePoint.Expect100Continue = Expect100Continue;
                request.ServicePoint.ConnectionLimit = ConnectionLimit;
                request.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                request.AllowAutoRedirect = AllowAutoRedirect;
                request.GetRequestStream().Write(dataToSend, 0, dataToSend.Length);
                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
                response.Close();

            }
            catch (WebException e)
            {
                response_error = e.Response;
                haserror = true;

                response = (HttpWebResponse)response_error;

                using (Stream data1 = response.GetResponseStream())
                using (var reader1 = new StreamReader(data1))
                {
                    html = reader1.ReadToEnd();

                }


            }
            return html;

        }

        public string Post(Uri uri)// POST Method
        {
            
            try
            {

                request = (HttpWebRequest)WebRequest.Create(uri);
                if (Cookiereq.Count > 0)
                {
                    request.CookieContainer = Cookiereq;
                }
                if (UseProxy == true)
                {
                    request.Proxy = proxyip;
                }
                else
                {
                    request.Proxy = null;
                }
                request.Timeout = Timeout;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                if (CHeader.Count > 0)
                {
                    foreach (var Header in CHeader)
                    {
                        request.Headers[Header.Key] = Header.Value;
                    }
                }
                string data = null;
                if (Param.Count > 0)
                {
                    foreach (var item in Param)
                    {
                        data += item.Key + "=" + item.Value + "&";
                    }
                }

                byte[] dataToSend = Encoding.UTF8.GetBytes(data);
                request.ContentLength = dataToSend.Length;
                request.KeepAlive = KeepAlive;
                request.Accept = Accept;
                request.Headers.Add("Accept-Encoding", Accept_Encoding);
                request.Headers.Add("Accept-Language", Accept_Language);
                request.UserAgent = UserAgent;
                request.ServicePoint.Expect100Continue = Expect100Continue;
                request.ServicePoint.ConnectionLimit = ConnectionLimit;
                request.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                request.AllowAutoRedirect = AllowAutoRedirect;
                request.GetRequestStream().Write(dataToSend, 0, dataToSend.Length);
                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
                response.Close();

            }
            catch (WebException e)
            {
                response_error = e.Response;
                haserror = true;

                response = (HttpWebResponse)response_error;

                using (Stream data1 = response.GetResponseStream())
                using (var reader1 = new StreamReader(data1))
                {
                    html = reader1.ReadToEnd();

                }


            }
            return html;

        }
        public string Post(string url, string ContentType = "application/x-www-form-urlencoded")// POST Method
        {

            try
            {

                request = (HttpWebRequest)WebRequest.Create(url);
                if (Cookiereq.Count > 0)
                {
                    request.CookieContainer = Cookiereq;
                }
                if (UseProxy == true)
                {
                    request.Proxy = proxyip;
                }
                else
                {
                    request.Proxy = null;
                }
                request.Timeout = Timeout;
                request.Method = "POST";
                request.ContentType = ContentType;
                if (CHeader.Count > 0)
                {
                    foreach (var Header in CHeader)
                    {
                        request.Headers[Header.Key] = Header.Value;
                    }
                }
                string data = null;
                if (Param.Count > 0)
                {
                    foreach (var item in Param)
                    {
                        data += item.Key + "=" + item.Value + "&";
                    }
                }

                byte[] dataToSend = Encoding.UTF8.GetBytes(data);
                request.ContentLength = dataToSend.Length;
                request.KeepAlive = KeepAlive;
                request.Accept = Accept;
                request.Headers.Add("Accept-Encoding", Accept_Encoding);
                request.Headers.Add("Accept-Language", Accept_Language);
                request.UserAgent = UserAgent;
                request.ServicePoint.Expect100Continue = Expect100Continue;
                request.ServicePoint.ConnectionLimit = ConnectionLimit;
                request.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                request.AllowAutoRedirect = AllowAutoRedirect;
                request.GetRequestStream().Write(dataToSend, 0, dataToSend.Length);
                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
                response.Close();

            }
            catch (WebException e)
            {
                response_error = e.Response;
                haserror = true;

                response = (HttpWebResponse)response_error;

                using (Stream data1 = response.GetResponseStream())
                using (var reader1 = new StreamReader(data1))
                {
                    html = reader1.ReadToEnd();

                }


            }
            return html;

        }
        public string Post(string url, HTTPMethod Method)// Custom Method
        {

            try
            {

                request = (HttpWebRequest)WebRequest.Create(url);
                if (Cookiereq.Count > 0)
                {
                    request.CookieContainer = Cookiereq;
                }
                if (UseProxy == true)
                {
                    request.Proxy = proxyip;
                }
                else
                {
                    request.Proxy = null;
                }
                request.Timeout = Timeout;
                request.Method = Method.ToString();
                request.ContentType = "application/x-www-form-urlencoded";
                if (CHeader.Count > 0)
                {
                    foreach (var Header in CHeader)
                    {
                        request.Headers[Header.Key] = Header.Value;
                    }
                }
                string data = null;
                if (Param.Count > 0)
                {
                    foreach (var item in Param)
                    {
                        data += item.Key + "=" + item.Value + "&";
                    }
                }

                byte[] dataToSend = Encoding.UTF8.GetBytes(data);
                request.ContentLength = dataToSend.Length;
                request.KeepAlive = KeepAlive;
                request.Accept = Accept;
                request.Headers.Add("Accept-Encoding", Accept_Encoding);
                request.Headers.Add("Accept-Language", Accept_Language);
                request.UserAgent = UserAgent;
                request.ServicePoint.Expect100Continue = Expect100Continue;
                request.ServicePoint.ConnectionLimit = ConnectionLimit;
                request.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                request.AllowAutoRedirect = AllowAutoRedirect;
                request.GetRequestStream().Write(dataToSend, 0, dataToSend.Length);
                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
                response.Close();

            }
            catch (WebException e)
            {
                response_error = e.Response;
                haserror = true;

                response = (HttpWebResponse)response_error;

                using (Stream data1 = response.GetResponseStream())
                using (var reader1 = new StreamReader(data1))
                {
                    html = reader1.ReadToEnd();

                }


            }
            return html;

        }
        public string Post(string url,string ReqParam, string ContentType = "application/x-www-form-urlencoded")// POST Method
        {
            
            try
            {

                request = (HttpWebRequest)WebRequest.Create(url);
                if (Cookiereq.Count > 0)
                {
                    request.CookieContainer = Cookiereq;
                }
                if (UseProxy == true)
                {
                    request.Proxy = proxyip;
                }
                else
                {
                    request.Proxy = null;
                }
                request.Timeout = Timeout;
                request.Method = "POST";
                request.ContentType = ContentType;
                if (CHeader.Count > 0)
                {
                    foreach (var Header in CHeader)
                    {
                        request.Headers[Header.Key] = Header.Value;
                    }
                }

                byte[] dataToSend = Encoding.UTF8.GetBytes(ReqParam);
                request.ContentLength = dataToSend.Length;
                request.KeepAlive = KeepAlive;
                request.Accept = Accept;
                request.Headers.Add("Accept-Encoding", Accept_Encoding);
                request.Headers.Add("Accept-Language", Accept_Language);
                request.ServicePoint.Expect100Continue = Expect100Continue;
                request.ServicePoint.ConnectionLimit = ConnectionLimit;
                request.ServicePoint.UseNagleAlgorithm = UseNagleAlgorithm;
                request.AllowAutoRedirect = AllowAutoRedirect;
                request.GetRequestStream().Write(dataToSend, 0, dataToSend.Length);
                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                }
                

                   
                response.Close();

            }
            catch (WebException e)
            {
                response_error = e.Response;
                haserror = true;

                response = (HttpWebResponse)response_error;

                using (Stream data1 = response.GetResponseStream())
                using (var reader1 = new StreamReader(data1))
                {
                    html = reader1.ReadToEnd();

                }


            }
            return html;

        }
        
        public string GetResponseHeader(string HeaderName)// User Can Get Response Header With this
        {
            if(haserror == true)
            {
                return response_error.Headers[HeaderName];
            }
            else
            {
                return response.GetResponseHeader(HeaderName);
            }
           
        }
        public string GetstringBetween(string Before,string After)// User Can Get String Between Two Word With this
        {
            int Beforeword = html.IndexOf(Before) + Before.Length;
            int Afterword = html.LastIndexOf(After);

            return html.Substring(Beforeword, Afterword - Beforeword);

        }
        public void AddHeader(string RequestHeader, string Value)// User Can Add Header With this
        {
            CHeader.Add(RequestHeader, Value);


        }
        public void AddParam(string Name, string Value = null)// User Can Add Parameter With this
        {
            Param.Add(Name, Value);


        }


        public void Cookie(CookieContainer cokie)// User Can Add custom Cookie With this
        {
            Cookiereq = cokie;
        }

        public CookieContainer Cookie()// User Can Get Cookie With this
        {
           return request.CookieContainer;
        }

        public int HttpStatus()// User Can Get Http Status With this
        {
            return (int)response.StatusCode;
        }

        public void Proxy(string Proxy, bool bypass = true)// User Can Use HTTP and HTTPs Proxy With this
        {
            UseProxy = true;
            proxyip = new WebProxy(Proxy, bypass);
        }
    }
}
