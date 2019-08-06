using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tags.Api.BAL;
using Tags.Model;
using Newtonsoft.Json;

namespace Tags.Api {
    class Program {
       //Creates one instances of connection and avaids exhaust of ssockets
       
       public static void Main (string[] args) {
            try
            {                           
                Console.WriteLine( "Started Tag Api application");
                HttpService.InitializeService();
                TagResult objResult = new TagResult();               
                bool bSuccess =  HttpService.GetTagsAll(objResult).GetAwaiter().GetResult(); 
                Console.WriteLine( $"Populated All Tags :  {bSuccess}");
               
                if(bSuccess){                    
                    //Insert into DB               
                    HttpService.tagService.TruncateTagTable();                                        
                    bool bSuccessResult = HttpService.tagService.InsertTagResult(objResult);       
                    HttpService.tagService.MergeTagTable();                                                                         
                    Console.WriteLine( "Inserted data into Tags table successfully");
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine( $"ERROR : {ex.Message}");                
            }

            Console.WriteLine( "Finished Tag Api application");
        }      
    }
}