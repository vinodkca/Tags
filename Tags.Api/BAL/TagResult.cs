using System;
using System.Collections.Generic;
using Tags.Model;

namespace Tags.Api.BAL
{
    public class TagResult
    {
        public List<TagHierarchy> lstTagsHierarchy;
        public List<Tag> lstTagNames;
        public List<Title> lstTitleNames; 
    }
}
