using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Tags.Model;
using System.Threading.Tasks;

namespace Tags.Api.DAL
{
    public class TagRepository : Repository
    {
         private static IDbConnection db = null;

        public TagRepository(string strConn)
        {
            db = new SqlConnection(strConn);
        } 

      //    public bool InsertLines(List<LineItem> lstLines)
      //   {
      //       //Match DB STG_Line
      //       List<Line>  lstLinesDB = new List<Line>();

      //       foreach (LineItem lineItem in lstLines) {

      //           Line line = new Line();
      //           line.ID = lineItem.ID;
      //           line.TrackingNumber = lineItem.TrackingNumber;
      //           line.RingTo = lineItem.RingTo;
      //           line.TrackingLineName = lineItem.TrackingLineName;
      //           line.Type = lineItem.Type;
      //           line.Year = lineItem.Year;
      //           line.AccountNumber = lineItem.AccountNumber;
      //           line.Market = lineItem.Market;
      //           line.Heading = lineItem.Heading;
      //           line.UDAC = lineItem.UDAC;
      //           line.PubDate = lineItem.PubDate;
      //           line.EmailAddress = lineItem.EmailAddress; 
      //           line.PortType = lineItem.PortType; 
      //           line.PortDate = lineItem.PortDate;                
      //           line.CreatedDate = lineItem.CreatedDate;

      //           lstLinesDB.Add(line);
      //       }

      //        return BulkCopy<Line>(db as SqlConnection, "STG_Line", lstLinesDB);
      //   }
        public int TruncateTagTable()
        {
            return db.Execute(sql:"dbo.spTruncateTagTables", commandType: CommandType.StoredProcedure);                   
        }

        public bool InsertTagHierarchy(List<TagHierarchy> lstTagHierarchy)
        {
            return BulkCopy<TagHierarchy>(db as SqlConnection, "STG_TagHierarchy", lstTagHierarchy);
        }

        public bool InsertTag(List<Tag> lstTag)
        {
            return BulkCopy<Tag>(db as SqlConnection, "STG_Tag", lstTag);
        }  

        public bool InsertTitle(List<Title> lstTitle)
        {
            return BulkCopy<Title>(db as SqlConnection, "STG_Title", lstTitle);
        }           

        public int MergeTagTables()
        {           
           return db.Execute(sql:"dbo.spMergeTagTables", commandType: CommandType.StoredProcedure);        
        }
     }
}
