using System.Linq;
using PlaylistAPI.Models;

namespace PlaylistAPI.Business
{
    public class BaseBusiness<TModel> where TModel : BaseModel
    {
        private readonly PlaylistContext context;

        public BaseBusiness(PlaylistContext context)
        {
            this.context = context;
        }

        public TModel Get(int id)
        {
            var dbSet = context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                return dbSet.Where(item => item.Id == id && item.Active).FirstOrDefault();
            }
            return null;
        }

        public TModel Insert(TModel model)
        {
            var dbSet = context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                try
                {
                    dbSet.Add(model);
                    context.SaveChanges();
                    return model;
                }
                catch { return null; }
            }
            return null;
        }

        public TModel Update(TModel model)
        {
            var dbSet = context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                try
                {
                    dbSet.Update(model);
                    context.SaveChanges();
                    return model;
                }
                catch { return null; }
            }
            return null;
        }

        public TModel Delete(int id)
        {
            var dbSet = context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                try
                {
                    var model = dbSet.Where(item => item.Id == id && item.Active).FirstOrDefault() as TModel;
                    if (model != null)
                    {
                        model.Active = false;
                        return Update(model);
                    }
                    else return null;
                }
                catch { return null; }
            }
            return null;
        }

        public TModel Recover(int id)
        {
            var dbSet = context.ArquireDbSet<TModel>();
            if (dbSet != null)
            {
                try
                {
                    var model = dbSet.Where(item => item.Id == id).FirstOrDefault() as TModel;
                    if (model != null && !model.Active)
                    {
                        model.Active = true;
                        return Update(model);
                    }
                    return null;
                }
                catch { return null; }
            }
            return null;
        }
    }
}