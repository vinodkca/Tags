using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tags.Api.DAL;
using Tags.Model;

namespace Tags.Api.BAL
{
    public class TagService
    {       
        TagRepository tagRepo = null;
        
        public TagService()
        {
           tagRepo = new TagRepository(AppSetting.SQL_DIAD);
        }

       public bool InsertTagResult(TagResult objTagResult)
        {            
            if(objTagResult != null){
                InsertTagHierarchy(objTagResult.lstTagsHierarchy);
                InsertTag(objTagResult.lstTagNames);
                InsertTitle(objTagResult.lstTitleNames);
                return true;       
            }
            return false;
        }  
       public bool InsertTagHierarchy(List<TagHierarchy> lstTagHierarchy)
        {            
            return tagRepo.InsertTagHierarchy(lstTagHierarchy);       
        }
        public bool InsertTag(List<Tag> lstTag)
        {
            return tagRepo.InsertTag(lstTag);       
        }
        public bool InsertTitle(List<Title> lstTitle)
        {
            return tagRepo.InsertTitle(lstTitle);       
        }
        public int TruncateTagTable(){
            return tagRepo.TruncateTagTable();
        }

        public int MergeTagTable()
        {
            return tagRepo.MergeTagTables();       
        }
    }
}
