using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace Tags.Api.BAL
{
    public class AppSetting{
        // Adding JSON file into IConfiguration.
        public static readonly IConfiguration config; 
        public static readonly string SQL_DIAD;
        public static readonly string API_URL_TAGS;

        //Initialize all static variable  after const have been called. Static constructor is called only once
        static AppSetting(){
            config = new ConfigurationBuilder()
                        .AddJsonFile("AppConfig.json",  optional: true, reloadOnChange: true)
                        .Build();  
            SQL_DIAD = config["DBConnection:SQLDIAD"]; 
            API_URL_TAGS = config["URL:Tags"]; 
        }
        //Tags URL
      
    }
}
