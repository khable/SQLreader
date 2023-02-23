namespace occ.dmr.dataConnector
{
    public struct DMRGlobalAttribute
    {
        public object Name;
        public object UDA;
        public object Value;
        public object IsPrimary;
        public object Uom;
        public object Datatype;

        public string GetAttributeName(string poostfix = "")
        {
            return UDA.ToString() + poostfix;
        }

        public bool ValueIsNull()
        {
            if (Value == null || Value.ToString() == " " || Value.ToString() == "")
            {
                return true;
            }

            return false;
        }

        public string GetNumberDatatype()
        {
            return "REAL";
        }
    }
}
