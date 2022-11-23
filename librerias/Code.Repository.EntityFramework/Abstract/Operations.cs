

using Code.Repository.EntityFramework.Context;
using System.Collections.Generic;

namespace Code.Repository.EntityFramework.Abstract
{
    public class OperationsEF
    {
        public T GetEntity<T>(int _id) where T : class
        {
            ApplicationDatabaseContext _cnn;

            _cnn = new ApplicationDatabaseContext();
            return _cnn.Set<T>().Find(_id);
        }


        public void UpdateEntity<T>(T modelo) where T : class
        {
            ApplicationDatabaseContext _cnn;

            _cnn = new ApplicationDatabaseContext();

            _cnn.Entry<T>(modelo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _cnn.SaveChanges();

        }

        public T AddEntity<T>(T modelo) where T : class
        {
            ApplicationDatabaseContext _cnn;

            _cnn = new ApplicationDatabaseContext();

            _cnn.Entry<T>(modelo).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _cnn.SaveChanges();

            return modelo;
        }


        public IEnumerable<T> GetAllEntity<T>() where T : class
        {
            ApplicationDatabaseContext _cnn;

            _cnn = new ApplicationDatabaseContext();

            return _cnn.Set<T>();
        }
    }
}
