//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AyloBot
{
    using System;
    using System.Collections.Generic;
    
    public partial class Filtered
    {
        public int FID { get; set; }
        public Nullable<int> FTID { get; set; }
        public Nullable<int> SID { get; set; }
    
        public virtual FilterType FilterType { get; set; }
        public virtual Symbol Symbol { get; set; }
    }
}
