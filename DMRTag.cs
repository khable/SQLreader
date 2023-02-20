using System.Collections.Generic;

namespace SQLReader
{
    public struct DMRTag
    {
        public object Name;
        public object UDET;
        public object TagList;
        public object GlobalClass;
        public object SourceApplication;
        public List<DMRGlobalAttribute> Attributes;
        public bool Validated;

        public string GetRefNAME()
        {
            return "/" + Name.ToString();
        }
    }
}