//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VNPT_TTS_PROJECT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int idR { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public Nullable<int> numOfPeople { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string idTour { get; set; }
        public string username { get; set; }
    
        public virtual LOGIN LOGIN { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
