//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ComplexCommerce.Data.SqlServer.Model
{
    public partial class Page
    {
        #region Primitive Properties
    
        public virtual System.Guid Id
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> ParentId
        {
            get;
            set;
        }
    
        public virtual System.Guid TenantLocaleId
        {
            get { return _tenantLocaleId; }
            set
            {
                if (_tenantLocaleId != value)
                {
                    if (TenantLocale != null && TenantLocale.Id != value)
                    {
                        TenantLocale = null;
                    }
                    _tenantLocaleId = value;
                }
            }
        }
        private System.Guid _tenantLocaleId;
    
        public virtual string RouteUrl
        {
            get;
            set;
        }
    
        public virtual int ContentTypeId
        {
            get;
            set;
        }
    
        public virtual System.Guid ContentId
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual TenantLocale TenantLocale
        {
            get { return _tenantLocale; }
            set
            {
                if (!ReferenceEquals(_tenantLocale, value))
                {
                    var previousValue = _tenantLocale;
                    _tenantLocale = value;
                    FixupTenantLocale(previousValue);
                }
            }
        }
        private TenantLocale _tenantLocale;

        #endregion

        #region Association Fixup
    
        private void FixupTenantLocale(TenantLocale previousValue)
        {
            if (previousValue != null && previousValue.Page.Contains(this))
            {
                previousValue.Page.Remove(this);
            }
    
            if (TenantLocale != null)
            {
                if (!TenantLocale.Page.Contains(this))
                {
                    TenantLocale.Page.Add(this);
                }
                if (TenantLocaleId != TenantLocale.Id)
                {
                    TenantLocaleId = TenantLocale.Id;
                }
            }
        }

        #endregion

    }
}
