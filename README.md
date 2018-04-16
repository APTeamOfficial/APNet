# APNet
A simple library For .NET, You can use this library for any HttpRequest.
# Example

Get Method :

<pre>
using (HttpRequest client = new HttpRequest())
{
     string Response = client.Get("http://site.com/");
}
</pre>

Post Method :

<pre>
using (HttpRequest client = new HttpRequest())
{
     client.AddParam("data1", "value1");
     client.AddParam("data2", "value2");
     client.AddParam("data3", "value3");
     string Response = client.Post("http://site.com/");
}
</pre>

Get Response Header :

<pre>
using (HttpRequest client = new HttpRequest())
{
     client.AddParam("data1", "value1");
     client.AddParam("data2", "value2");
     client.AddParam("data3", "value3");
     string Response = client.Post("http://site.com/");
     string GetHeader = client.GetResponseHeader(HeaderName);     
}
</pre>

Get String Between Two Word :

<pre>
using (HttpRequest client = new HttpRequest())
{
     client.AddParam("data1", "value1");
     client.AddParam("data2", "value2");
     client.AddParam("data3", "value3");
     string Response = client.Post("http://site.com/");
     string GetHeader = client.GetstringBetweeen( BeforeWord, AfterWord);     
}
</pre>

# How to use

Just compile it!

