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
    public partial class Chain
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
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
    
        public virtual ICollection<Product> Product
        {
            get
            {
                if (_product == null)
                {
                    var newCollection = new FixupCollection<Product>();
                    newCollection.CollectionChanged += FixupProduct;
                    _product = newCollection;
                }
                return _product;
            }
            set
            {
                if (!ReferenceEquals(_product, value))
                {
                    var previousValue = _product as FixupCollection<Product>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupProduct;
                    }
                    _product = value;
                    var newValue = value as FixupCollection<Product>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupProduct;
                    }
                }
            }
        }
        private ICollection<Product> _product;
    
        public virtual ICollection<Store> Store
        {
            get
            {
                if (_store == null)
                {
                    var newCollection = new FixupCollection<Store>();
                    newCollection.CollectionChanged += FixupStore;
                    _store = newCollection;
                }
                return _store;
            }
            set
            {
                if (!ReferenceEquals(_store, value))
                {
                    var previousValue = _store as FixupCollection<Store>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupStore;
                    }
                    _store = value;
                    var newValue = value as FixupCollection<Store>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupStore;
                    }
                }
            }
        }
        private ICollection<Store> _store;

        #endregion

        #region Association Fixup
    
        private void FixupProduct(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Product item in e.NewItems)
                {
                    item.Chain = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Product item in e.OldItems)
                {
                    if (ReferenceEquals(item.Chain, this))
                    {
                        item.Chain = null;
                    }
                }
            }
        }
    
        private void FixupStore(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Store item in e.NewItems)
                {
                    item.Chain = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Store item in e.OldItems)
                {
                    if (ReferenceEquals(item.Chain, this))
                    {
                        item.Chain = null;
                    }
                }
            }
        }

        #endregion

    }
}