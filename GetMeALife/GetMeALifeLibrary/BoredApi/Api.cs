using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GetMeALifeLibrary.BoredApi
{
    public static class Api
    {
        const string boredUrl = "http://www.boredapi.com/api/activity";

        public static Activity GetRandomActivity()
        {
            return Get(boredUrl);
        }

        public static Activity GetActivityByKey(string key)
        {
            return Get(boredUrl + $"?key={key}");
        }

        public static Activity GetActivityByType(string type)
        {
            return Get(boredUrl + $"?type={type}");
        }

        public static Activity GetActivityByParticipants(int participants)
        {
            return Get(boredUrl + $"?participants={participants}");
        }

        public static Activity GetActivityByPrice(double price)
        {
            return Get(boredUrl + $"?price={price}");
        }

        public static Activity GetActivityByPriceRange(double minprice, double maxprice)
        {
            return Get(boredUrl + $"?minprice={minprice}&maxprice={maxprice}");
        }
        
        public static Activity GetActivityByAccessibility(double accessibility)
        {
            return Get(boredUrl + $"?accessibility={accessibility}");
        }

        public static Activity GetActivityByAccessibilityRange(double minaccessibility, double maxaccessibility)
        {
            return Get(boredUrl + $"?minaccessibility={minaccessibility}&maxaccessibility={maxaccessibility}");
        }

        private static Activity Get(string url)
        {
            var request = new HttpClient().GetStringAsync(url);
            var json = request.Result;
            return Activity.ReadFromJson(json);
        }
    }
}
