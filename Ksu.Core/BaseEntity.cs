using System;
using System.Collections.Generic;
using System.Linq;

namespace Arabia.Core
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        //public int Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }  
        private Type GetUnproxiedType()
        {
            return GetType();
        } 
        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }

        #region Code could be used for nHibernate

        //protected virtual void SetParent(dynamic child)
        //{

        //}
        //protected virtual void SetParentToNull(dynamic child)
        //{

        //}

        //protected void ChildCollectionSetter<T>(ICollection<T> collection, ICollection<T> newCollection) where T : class
        //{
        //    if (CommonHelper.OneToManyCollectionWrapperEnabled)
        //    {
        //        collection.Clear();
        //        if (newCollection != null)
        //            newCollection.ToList().ForEach(x => collection.Add(x));
        //    }
        //    else
        //    {
        //        collection = newCollection;
        //    }
        //}


        //protected ICollection<T> ChildCollectionGetter<T>(ref ICollection<T> collection, ref ICollection<T> wrappedCollection) where T : class
        //{
        //    return ChildCollectionGetter(ref collection, ref wrappedCollection, SetParent, SetParentToNull);
        //}

        //protected ICollection<T> ChildCollectionGetter<T>(ref ICollection<T> collection, ref ICollection<T> wrappedCollection, Action<dynamic> setParent, Action<dynamic> setParentToNull) where T : class
        //{
        //    if (CommonHelper.OneToManyCollectionWrapperEnabled)
        //        return wrappedCollection ?? (wrappedCollection = (collection ?? (collection = new List<T>())).SetupBeforeAndAfterActions(setParent, SetParentToNull));
        //    return collection ?? (collection = new List<T>());
        //}

        #endregion
    }
}
