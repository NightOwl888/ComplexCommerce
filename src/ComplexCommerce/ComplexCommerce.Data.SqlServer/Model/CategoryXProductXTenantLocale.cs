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
    
    public partial class CategoryXProductXTenantLocale
    {
        public System.Guid Id { get; set; }
        public System.Guid CategoryId { get; set; }
        public System.Guid ProductXTenantLocaleId { get; set; }
        public string RouteUrl { get; set; }
    
        public virtual ProductXTenantLocale ProductXTenantLocale { get; set; }
        public virtual Category Category { get; set; }
    }
}
