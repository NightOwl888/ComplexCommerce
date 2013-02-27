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
    public partial class ProductXStoreLocale
    {
        #region Primitive Properties
    
        public virtual System.Guid Id
        {
            get;
            set;
        }
    
        public virtual System.Guid ProductId
        {
            get { return _productId; }
            set
            {
                if (_productId != value)
                {
                    if (Product != null && Product.Id != value)
                    {
                        Product = null;
                    }
                    _productId = value;
                }
            }
        }
        private System.Guid _productId;
    
        public virtual System.Guid StoreLocaleId
        {
            get { return _storeLocaleId; }
            set
            {
                if (_storeLocaleId != value)
                {
                    if (StoreLocale != null && StoreLocale.Id != value)
                    {
                        StoreLocale = null;
                    }
                    _storeLocaleId = value;
                }
            }
        }
        private System.Guid _storeLocaleId;
    
        public virtual string Name
        {
            get;
            set;
        }
    
        public virtual string Description
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual ICollection<CategoryXProductXStoreLocale> CategoryXProductXStoreLocale
        {
            get
            {
                if (_categoryXProductXStoreLocale == null)
                {
                    var newCollection = new FixupCollection<CategoryXProductXStoreLocale>();
                    newCollection.CollectionChanged += FixupCategoryXProductXStoreLocale;
                    _categoryXProductXStoreLocale = newCollection;
                }
                return _categoryXProductXStoreLocale;
            }
            set
            {
                if (!ReferenceEquals(_categoryXProductXStoreLocale, value))
                {
                    var previousValue = _categoryXProductXStoreLocale as FixupCollection<CategoryXProductXStoreLocale>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupCategoryXProductXStoreLocale;
                    }
                    _categoryXProductXStoreLocale = value;
                    var newValue = value as FixupCollection<CategoryXProductXStoreLocale>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupCategoryXProductXStoreLocale;
                    }
                }
            }
        }
        private ICollection<CategoryXProductXStoreLocale> _categoryXProductXStoreLocale;
    
        public virtual Product Product
        {
            get { return _product; }
            set
            {
                if (!ReferenceEquals(_product, value))
                {
                    var previousValue = _product;
                    _product = value;
                    FixupProduct(previousValue);
                }
            }
        }
        private Product _product;
    
        public virtual StoreLocale StoreLocale
        {
            get { return _storeLocale; }
            set
            {
                if (!ReferenceEquals(_storeLocale, value))
                {
                    var previousValue = _storeLocale;
                    _storeLocale = value;
                    FixupStoreLocale(previousValue);
                }
            }
        }
        private StoreLocale _storeLocale;

        #endregion

        #region Association Fixup
    
        private void FixupProduct(Product previousValue)
        {
            if (previousValue != null && previousValue.ProductXStoreLocale.Contains(this))
            {
                previousValue.ProductXStoreLocale.Remove(this);
            }
    
            if (Product != null)
            {
                if (!Product.ProductXStoreLocale.Contains(this))
                {
                    Product.ProductXStoreLocale.Add(this);
                }
                if (ProductId != Product.Id)
                {
                    ProductId = Product.Id;
                }
            }
        }
    
        private void FixupStoreLocale(StoreLocale previousValue)
        {
            if (previousValue != null && previousValue.ProductXStoreLocale.Contains(this))
            {
                previousValue.ProductXStoreLocale.Remove(this);
            }
    
            if (StoreLocale != null)
            {
                if (!StoreLocale.ProductXStoreLocale.Contains(this))
                {
                    StoreLocale.ProductXStoreLocale.Add(this);
                }
                if (StoreLocaleId != StoreLocale.Id)
                {
                    StoreLocaleId = StoreLocale.Id;
                }
            }
        }
    
        private void FixupCategoryXProductXStoreLocale(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (CategoryXProductXStoreLocale item in e.NewItems)
                {
                    item.ProductXStoreLocale = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (CategoryXProductXStoreLocale item in e.OldItems)
                {
                    if (ReferenceEquals(item.ProductXStoreLocale, this))
                    {
                        item.ProductXStoreLocale = null;
                    }
                }
            }
        }

        #endregion

    }
}