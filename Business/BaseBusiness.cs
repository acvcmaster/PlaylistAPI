using System.Collections.Generic;
using System.Linq;
using PlaylistAPI.Models;

namespace PlaylistAPI.Business
{
    public class BaseBusiness<TModel> where TModel : BaseModel
    {
        public PlaylistContext Context { get; set; }
        private Dictionary<string, object> _auxiliaryBusiness { get; set; }

        public BaseBusiness(PlaylistContext context)
        {
            this.Context = context;
            this._auxiliaryBusiness = new Dictionary<string, object>();
        }

        public virtual TModel Get(int id)
        {
            var dbSet = Context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                return dbSet.Where(item => item.Id == id).FirstOrDefault();
            }
            return null;
        }

        public virtual TModel Insert(TModel model)
        {
            var dbSet = Context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                try
                {
                    dbSet.Add(model);
                    Context.SaveChanges();
                    return model;
                }
                catch { return null; }
            }
            return null;
        }

        public virtual TModel Update(TModel model)
        {
            var dbSet = Context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                try
                {
                    dbSet.Update(model);
                    Context.SaveChanges();
                    return model;
                }
                catch { return null; }
            }
            return null;
        }

        public virtual TModel Delete(int id)
        {
            var dbSet = Context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                try
                {
                    var model = dbSet.Where(item => item.Id == id).FirstOrDefault() as TModel;
                    if (model != null)
                    {
                        dbSet.Remove(model);
                        Context.SaveChanges();
                        return model;
                    }
                    else return null;
                }
                catch { return null; }
            }
            return null;
        }

        public void AddAuxiliraryBusiness<TBusiness, TBusinessModel>()
            where TBusiness : BaseBusiness<TBusinessModel>, new()
            where TBusinessModel : BaseModel
        {
            string typeName = typeof(TBusiness).Name;
            if (!_auxiliaryBusiness.ContainsKey(typeName))
            {
                TBusiness business = new TBusiness();
                business.Context = Context;
                _auxiliaryBusiness.Add(typeName, business);
            }
        }

        public TBusiness GetAuxiliraryBusiness<TBusiness, TBusinessModel>()
            where TBusiness : BaseBusiness<TBusinessModel>, new()
            where TBusinessModel : BaseModel
        {
            string typeName = typeof(TBusiness).Name;
            if (_auxiliaryBusiness.ContainsKey(typeName))
                return _auxiliaryBusiness[typeName] as TBusiness;
            else return null;
        }
    }
}