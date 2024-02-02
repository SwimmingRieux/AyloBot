

namespace AyloBot.MetaData
{

    class Filtereds_MetaData
    {
        public string FTName { get; set; }
        public string FTDesc { get; set; }
        public string SName { get; set; }
             
        
    }
}

namespace AyloBot
{

    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(AyloBot.MetaData.Filtereds_MetaData))]
    partial class Filtered
    {
        public string FTName
        {
            get
            {
                try
                {
                    return this.FilterType.FTName;
                }
                catch
                {
                    return "";
                }
            }
            set
            {

            }
        }
        public string FTDesc
        {
            get
            {
                try
                {
                    return this.FilterType.FTDesc;
                }
                catch
                {
                    return "";
                }
            }
            set
            {

            }
        }

        public string SName
        {
            get
            {
                try
                {
                    return this.Symbol.SName;
                }
                catch
                {
                    return "";
                }
            }
            set
            {

            }
        }
        
    }
}
