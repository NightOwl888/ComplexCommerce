//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplexCommerce.Data.SqlServer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.ProductXTenantLocale = new HashSet<ProductXTenantLocale>();
        }
    
        public System.Guid Id { get; set; }
        public int ChainId { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual Chain Chain { get; set; }
        public virtual ICollection<ProductXTenantLocale> ProductXTenantLocale { get; set; }
    }
}
