using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceppSDK
{
    public interface ISerive
    {
        T HttpPost<T>(string partUrl, IDictionary<object, object> dictionary) where T : class;
        T HttpGet<T>(string partUrl, IDictionary<object, object> dictionary) where T : class;
    }

    public interface IHttpMethod
    {
        string HttpPost(string url, string param);
        string HttpPost(string url, IDictionary<object, object> param, byte[] fileByte);
        string HttpGet(string url);
    }
}
