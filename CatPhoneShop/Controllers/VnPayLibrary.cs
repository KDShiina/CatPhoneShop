// VnPayLibrary.cs
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;

namespace CatPhoneShop.Models
{
    public class VnPayLibrary
    {
        private SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
        private SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

        public void AddRequestData(string key, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                _requestData.Add(key, value);
            }
        }

        public void AddResponseData(string key, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                _responseData.Add(key, value);
            }
        }

        public string GetResponseData(string key)
        {
            string retValue;
            if (_responseData.TryGetValue(key, out retValue))
            {
                return retValue;
            }
            else
            {
                return string.Empty;
            }
        }

        #region Request

        public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in _requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string queryString = data.ToString();

            baseUrl += "?" + queryString;
            String signData = queryString;
            if (signData.Length > 0)
            {
                signData = signData.Remove(signData.Length - 1, 1);
            }
            string vnp_SecureHash = HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

            return baseUrl;
        }

        #endregion

        #region Response process

        public bool ValidateSignature(Dictionary<string, string> vnpayData, string secretKey)
        {
            string vnp_SecureHash = vnpayData["vnp_SecureHash"];
            if (!string.IsNullOrEmpty(vnp_SecureHash))
            {
                vnpayData.Remove("vnp_SecureHash");
                if (vnpayData.ContainsKey("vnp_SecureHashType"))
                {
                    vnpayData.Remove("vnp_SecureHashType");
                }

                StringBuilder signData = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in vnpayData.OrderBy(x => x.Key, StringComparer.Ordinal))
                {
                    if (!String.IsNullOrEmpty(kv.Value))
                    {
                        signData.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                    }
                }

                string signDataString = signData.ToString();
                if (signDataString.Length > 0)
                {
                    signDataString = signDataString.Remove(signDataString.Length - 1, 1);
                }

                string checkSum = HmacSHA512(secretKey, signDataString);
                return vnp_SecureHash.Equals(checkSum, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public SortedList<string, string> GetFullResponseData(NameValueCollection queryString, string vnp_HashSecret)
        {
            var responseData = new SortedList<string, string>(new VnPayCompare());
            foreach (string key in queryString.Keys)
            {
                responseData.Add(key, queryString[key]);
            }
            return responseData;
        }

        #endregion

        #region Util Methods

        public string GetIpAddress()
        {
            string ipAddress;
            try
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown") || ipAddress.Length > 45)
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }

            return ipAddress;
        }

        public string HmacSHA512(string key, string inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        #endregion
    }

    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            var vnpCompare = CompareInfo.GetCompareInfo("en-US");
            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}

// WebUtility class (if needed)
public static class WebUtility
{
    public static string UrlEncode(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return Uri.EscapeDataString(value)
            .Replace("%20", "+")
            .Replace("!", "%21")
            .Replace("*", "%2A")
            .Replace("'", "%27")
            .Replace("(", "%28")
            .Replace(")", "%29");
    }
}