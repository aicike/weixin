using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF;
using System.Data;
using System.Data.Entity;
using Entity;
using System.Data.Entity.Validation;


namespace Business
{
    public class EFModel
    {
        public Context Context= new Context();

        public T Get<T>(int id) where T : class,BaseEntity
        {
            return Context.Set<T>().Where(a => a.ID == id).AsNoTracking().FirstOrDefault();
        }

        public IQueryable<T> List<T>() where T : class, BaseEntity
        {
            return Context.Set<T>();
        }


        public Result Add<T>(T entity) where T : class, BaseEntity
        {
            Context.Set<T>().Add(entity);
            return SaveChanges();
        }

        public Result AddList<T>(List<T> list) where T : class, BaseEntity
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            Context.Configuration.ValidateOnSaveEnabled = false;
            Context.Set<T>().AddRange(list);
            return SaveChanges();
        }

        public Result Edit<T>(T entity) where T : class, BaseEntity
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return SaveChanges();
        }



        public Result Delete<T>(int id) where T : class, BaseEntity
        {
            var entity = Context.Set<T>().FirstOrDefault(a => a.ID == id);
            if (entity == null)
            {
                return new Result("未找到数据");
            }
            Context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            return SaveChanges();
        }

        private Result SaveChanges()
        {
            try
            {
                Context.SaveChanges();
                return new Result();
            }
            catch (InvalidOperationException ex)
            {
                return new Result(ex.Message);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder error = new StringBuilder();
                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var itemErrors in item.ValidationErrors)
                    {
                        error.Append(itemErrors.ErrorMessage);
                    }
                }
                //throw new Exception(error.ToString());
                return new Result(error.ToString());
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                Exception e = ex;
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                return new Result(e.Message.ToString());
            }
            catch (Exception ex)
            {
                return new Result(ex.Message);
            }
        }
    }
}
