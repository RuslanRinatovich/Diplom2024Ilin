//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirSystemApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Air
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Air()
        {
            this.AirOrders = new HashSet<AirOrder>();
        }
    
        public int AirID { get; set; }
        public string AirName { get; set; }
        public string AirDescription { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AirOrder> AirOrders { get; set; }
    }
}
