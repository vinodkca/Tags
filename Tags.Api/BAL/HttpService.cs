using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tags.Api.BAL;
using Tags.Model;

namespace Tags.Api.BAL {
    public static class HttpService {
        private static HttpClient client;
        public static TagService tagService;

        static HttpService () {
            //Static constructor
            client = new HttpClient ();
            tagService = new TagService ();
        }

        //public static async Task InitializeService () {
        public static void InitializeService () {
            //Generate URL   
            //client.BaseAddress = new Uri (AppSetting.API_URL_TAGS);
            client.DefaultRequestHeaders.Clear ();
            client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

        }

        #region API calls

        public static TagHierarchy CreateTagHierarchy (int iTagHierarchyId, int iTitleId, int iTitleTagId, int iTitleTagTitleId, int iTitleTagTitleTagId, int iTitleTagTitleTagTitleId, int iTitleTagTitleTagTitleTagId) {
            TagHierarchy objTagHierarchy = new TagHierarchy ();
            objTagHierarchy.ID = iTagHierarchyId; //Level 0
            objTagHierarchy.TitleID = iTitleId;
            objTagHierarchy.TitleTagID = iTitleTagId;
            objTagHierarchy.TitleTagTitleID = iTitleTagTitleId;
            objTagHierarchy.TitleTagTitleTagID = iTitleTagTitleTagId;
            objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
            objTagHierarchy.TitleTagTitleTagTitleTagID = iTitleTagTitleTagTitleTagId; //Level 6
            return objTagHierarchy;
        }
        public static async Task<bool> GetTagsAll ( TagResult objResult) {
           
            string strTagsAllPath = AppSetting.API_URL_TAGS;

            HttpResponseMessage resp = await client.GetAsync (strTagsAllPath);
            if (resp.IsSuccessStatusCode) {

                Stream received = await resp.Content.ReadAsStreamAsync ();
                StreamReader readStream = new StreamReader (received);
                string jsonString = readStream.ReadToEnd ();

                JObject jo = JObject.Parse (jsonString);
                objResult.lstTagsHierarchy  = PopulateTagHierarchy (jo);
                objResult.lstTagNames = PopulateTagNames (jo);
                objResult.lstTitleNames = PopulateTitleNames (jo);             
                return true;
            }
            return false;
        }
        public static List<TagHierarchy> PopulateTagHierarchy (JObject jo) {
            int iTagHierarchyId = -1, iTitleId = -1, iTitleTagId = -1, iTitleTagTitleId = -1, iTitleTagTitleTagId = -1, iTitleTagTitleTagTitleId = -1, iTitleTagTitleTagTitleTagId = -1;
            List<TagHierarchy> lstTagsHierarchy = new List<TagHierarchy> ();

           foreach (JToken token0 in jo.SelectToken ("tagHierarchy")) {

                    if (token0.HasValues) {
                        // Console.WriteLine (token.ToString ());
                        //Converts string to JSON object
                        JObject obj = JObject.Parse ("{" + token0.ToString () + "}");
                        foreach (var pair in obj) {
                            //Level 0
                            if (pair.Key == "id") {
                                iTagHierarchyId = (int) obj[pair.Key];
                                //objTagHierarchy.ID = iTagHierarchyId;
                            }

                            //Console.WriteLine(pair.Key + "," + pair.Value);
                            if (pair.Key == "titles") {
                                foreach (JToken token1 in pair.Value) {
                                    // Console.WriteLine (token1.ToString ());
                                    //Converts string to JSON object
                                    if (token1.HasValues) {
                                        JObject obj1 = JObject.Parse (token1.ToString ());
                                        foreach (var pair1 in obj1) {
                                            //Level 1 
                                            if (pair1.Key == "id") {
                                                iTitleId = (int) obj1[pair1.Key];
                                                //objTagHierarchy.TitleID = iTitleId;
                                            }

                                            //Console.WriteLine(pair2.Key + "," + pair2.Value);
                                            if (pair1.Key == "tags") {
                                                foreach (JToken token2 in pair1.Value) {
                                                    // Console.WriteLine (token2.ToString ());
                                                    if (token2.HasValues) {
                                                        JObject obj2 = JObject.Parse (token2.ToString ());
                                                        foreach (var pair2 in obj2) {
                                                            //Level 2          
                                                            if (pair2.Key == "id") {
                                                                iTitleTagId = (int) obj2[pair2.Key];
                                                                //objTagHierarchy.TitleTagID = iTitleTagId;
                                                            }

                                                            if (pair2.Key == "titles") {
                                                                foreach (JToken token3 in pair2.Value) {
                                                                    //    Console.WriteLine (token3.ToString ());
                                                                    if (token3.HasValues) {
                                                                        JObject obj3 = JObject.Parse (token3.ToString ());
                                                                        foreach (var pair3 in obj3) {
                                                                            //Level 3                                
                                                                            if (pair3.Key == "id") {
                                                                                iTitleTagTitleId = (int) obj3[pair3.Key];
                                                                                //objTagHierarchy.TitleTagTitleID = iTitleTagTitleId;
                                                                            }

                                                                            if (pair3.Key == "tags") {
                                                                                foreach (JToken token4 in pair3.Value) {
                                                                                    // Console.WriteLine (token4.ToString ());
                                                                                    if (token4.HasValues) {
                                                                                        JObject obj4 = JObject.Parse (token4.ToString ());
                                                                                        foreach (var pair4 in obj4) {
                                                                                            //Level 4   
                                                                                            if (pair4.Key == "id") {
                                                                                                iTitleTagTitleTagId = (int) obj4[pair4.Key];
                                                                                                //objTagHierarchy.TitleTagTitleTagID = iTitleTagTitleTagId;
                                                                                            }

                                                                                            if (pair4.Key == "titles") {
                                                                                                foreach (JToken token5 in pair4.Value) {

                                                                                                    //Console.WriteLine (token5.ToString ());
                                                                                                    if (token5.HasValues) {

                                                                                                        JObject obj5 = JObject.Parse (token5.ToString ());
                                                                                                        foreach (var pair5 in obj5) {
                                                                                                            //Level 5   
                                                                                                            if (pair5.Key == "id") {
                                                                                                                iTitleTagTitleTagTitleId = (int) obj5[pair5.Key];
                                                                                                                //objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
                                                                                                            }

                                                                                                            if (pair5.Key == "tags") {
                                                                                                                foreach (JToken token6 in pair5.Value) {

                                                                                                                    if (token6.HasValues) //over
                                                                                                                    {

                                                                                                                    } else {
                                                                                                                        //Level 6     
                                                                                                                        //Console.WriteLine ("LEVEL6 " + token6.ToString ());
                                                                                                                        iTitleTagTitleTagTitleTagId = Convert.ToInt32 (token6.ToString ());
                                                                                                                        //objTagHierarchy.TitleTagTitleTagTitleTagID = iTitleTagTitleTagTitleTagId;
                                                                                                                        TagHierarchy obFinalObject = CreateTagHierarchy (iTagHierarchyId, iTitleId, iTitleTagId, iTitleTagTitleId, iTitleTagTitleTagId, iTitleTagTitleTagTitleId, iTitleTagTitleTagTitleTagId);
                                                                                                                        lstTagsHierarchy.Add (obFinalObject);
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }

                                                                                                    } else { //Level 5
                                                                                                        if (token5.HasValues) //over
                                                                                                        {

                                                                                                        } else {
                                                                                                            //Level 5 
                                                                                                            //Console.WriteLine ("LEVEL5 " + token5.ToString ());

                                                                                                            iTitleTagTitleTagTitleId = Convert.ToInt32 (token5.ToString ());
                                                                                                            //objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
                                                                                                            TagHierarchy obFinalObject = CreateTagHierarchy (iTagHierarchyId, iTitleId, iTitleTagId, iTitleTagTitleId, iTitleTagTitleTagId, iTitleTagTitleTagTitleId, -1);
                                                                                                            lstTagsHierarchy.Add (obFinalObject);
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    } //token4 if
                                                                                    else {
                                                                                        //Level 4 
                                                                                        //Console.WriteLine ("LEVEL4 " + token4.ToString ());

                                                                                        iTitleTagTitleTagId = Convert.ToInt32 (token4.ToString ());
                                                                                        //objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
                                                                                        TagHierarchy obFinalObject = CreateTagHierarchy (iTagHierarchyId, iTitleId, iTitleTagId, iTitleTagTitleId, iTitleTagTitleTagId, -1, -1);
                                                                                        lstTagsHierarchy.Add (obFinalObject);
                                                                                    }

                                                                                }
                                                                            }
                                                                        }
                                                                    } //token3 if
                                                                    else {
                                                                        //Level 3
                                                                        //Console.WriteLine ("LEVEL3 " + token3.ToString ());

                                                                        iTitleTagTitleId = Convert.ToInt32 (token3.ToString ());
                                                                        //objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
                                                                        TagHierarchy obFinalObject = CreateTagHierarchy (iTagHierarchyId, iTitleId, iTitleTagId, iTitleTagTitleId, -1, -1, -1);
                                                                        lstTagsHierarchy.Add (obFinalObject);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    } //token2 if 
                                                    else {
                                                        //Level 2
                                                        //Console.WriteLine ("LEVEL2 " + token2.ToString ());

                                                        iTitleTagId = Convert.ToInt32 (token2.ToString ());
                                                        //objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
                                                        TagHierarchy obFinalObject = CreateTagHierarchy (iTagHierarchyId, iTitleId, iTitleTagId, -1, -1, -1, -1);
                                                        lstTagsHierarchy.Add (obFinalObject);
                                                    }

                                                }
                                                //lstTagNames.Add( new TagName{ ID = Convert.ToInt32(pair.Key),  Description =  pair.Value.ToString() });
                                            }
                                        }
                                    } //token1 if 
                                    else {
                                        //Level 1
                                        //Console.WriteLine ("LEVEL1 " + token1.ToString ());

                                        iTitleId = Convert.ToInt32 (token1.ToString ());
                                        //objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
                                        TagHierarchy obFinalObject = CreateTagHierarchy (iTagHierarchyId, iTitleId, -1, -1, -1, -1, -1);
                                        lstTagsHierarchy.Add (obFinalObject);
                                    }

                                }
                                //lstTagNames.Add( new TagName{ ID = Convert.ToInt32(pair.Key),  Description =  pair.Value.ToString() });

                            }
                        }

                    } //token0 if
                    else {
                        //Level 0
                        //Console.WriteLine ("LEVEL0 " + token0.ToString ());

                        iTagHierarchyId = Convert.ToInt32 (token0.ToString ());
                        //objTagHierarchy.TitleTagTitleTagTitleID = iTitleTagTitleTagTitleId;
                        TagHierarchy obFinalObject = CreateTagHierarchy (iTagHierarchyId, -1, -1, -1, -1, -1, -1);
                        lstTagsHierarchy.Add (obFinalObject);
                    }

                    //int iCount = lstTagsHierarchy.Count;

              

                    //     JObject obj = JObject.Parse(jsonString);
                    //     List<Object> lstTransactionBatches = new List<Object>();
                    //     foreach (var pair in obj) {
                    //         Console.WriteLine (pair.Key);
                    //            Console.WriteLine (pair.Value);
                    //          foreach (JToken result in pair.Value)
                    //         {
                    //              Object objTransactionBatch = result.ToObject<Object>();
                    //             lstTransactionBatches.Add(objTransactionBatch);
                    //         }

                    //     }
                    // //    JObject jo = JObject.Parse(jsonString);

                    //     foreach (JToken token in jo.SelectToken("tagHierarchy"))
                    //     {
                    //         Console.WriteLine(token.Path + ": " + token.ToString());

                    //     }

                   // return null;
                    // Object objRoot = JsonConvert.DeserializeObject<Object> (jsonString);
                    // JArray objResult = ((dynamic) objRoot) ["ChildrenTokens"];

                    // foreach (JToken result in objResult) {
                    //     Object objTransactionBatch = result.ToObject<Object> ();
                    //     //lstTransactionBatches.Add(objTransactionBatch);
                    // }

                    // DataTable dtTable = jsonStringToTable(objResult.ToString());
                    // return dtTable;

                    // if (callService.TruncateTable ()) {
                    //     string strInsertCalls = callService.InsertCalls (lstCalls) ? $"Inserted {lstCalls.Count} in STG_Call table" : "Failed to insert calls in STG_Call table";
                    //     Console.WriteLine (strInsertCalls);
                    // } else {
                    //     Console.WriteLine ("STG_Call table was not truncated");
                    // }
                }

            return lstTagsHierarchy;
        }
        public static List<Title> PopulateTitleNames (JObject jo) {
            List<Title> lstTitleNames = new List<Title> ();
            foreach (JToken token in jo.SelectToken ("titleNames")) {
                //Converts string to JSON object
                JObject obj = JObject.Parse ("{" + token.ToString () + "}");
                foreach (var pair in obj) {
                    //Console.WriteLine(pair.Key + "," + pair.Value);
                    lstTitleNames.Add (new Title { ID = Convert.ToInt32 (pair.Key), Description = pair.Value.ToString () });
                }
            }
            return lstTitleNames;
        }
        public static List<Tag> PopulateTagNames (JObject jo) {
            List<Tag> lstTagNames = new List<Tag> ();
            foreach (JToken token in jo.SelectToken ("tagNames")) {
                //Converts string to JSON object
                JObject obj = JObject.Parse ("{" + token.ToString () + "}");
                foreach (var pair in obj) {
                    //Console.WriteLine(pair.Key + "," + pair.Value);
                    lstTagNames.Add (new Tag { ID = Convert.ToInt32 (pair.Key), Description = pair.Value.ToString () });
                }
            }
            return lstTagNames;
        }
       
        #endregion

        #region Helper functions
        // private static void ConvertLineItem (string ColName, LineItem line) {
        //     switch (ColName) {
        //         case "Year":
        //             line.Year = line.LabelValue01;
        //             break;
        //         case "Account Number":
        //             line.AccountNumber = line.LabelValue02;
        //             break;
        //         case "Market":
        //             line.Market = line.LabelValue03;
        //             break;
        //         case "Heading":
        //             line.Heading = line.LabelValue04;
        //             break;
        //         case "UDAC":
        //             line.UDAC = line.LabelValue05;
        //             break;
        //         case "Pub Date":
        //             line.PubDate = line.LabelValue06;
        //             break;
        //         case "Email Address":
        //             line.EmailAddress = line.LabelValue07;
        //             break;
        //         case "Type":
        //             line.PortType = line.LabelValue08;
        //             break;
        //         case "Port Date":
        //             line.PortDate = line.LabelValue09;
        //             break;
        //         default:
        //             break;
        //     }
        //}
        #endregion
    }
}