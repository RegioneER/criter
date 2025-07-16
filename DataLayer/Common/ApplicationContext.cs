using System.Data.Entity.Infrastructure;
using System.Web;

namespace DataLayer.Common
{
    public sealed class ApplicationContext
    {
        public const string ContextKey = "CriterDataModelContext";
        private static object _sync = new object();
        private static volatile ApplicationContext _currentInstance;

        public static ApplicationContext Current
        {
            get
            {
                if (_currentInstance == null)
                {
                    lock (_sync)
                    {
                        if (_currentInstance == null)
                            _currentInstance = new ApplicationContext();
                    }
                }
                return _currentInstance;
            }
        }

        public bool HasContext
        {
            get
            {
                return (HttpContext.Current.Items[ContextKey] != null);
            }
        }

        public CriterDataModel Context
        {
            get
            {
                if (HttpContext.Current.Items[ContextKey] == null)
                {
                    HttpContext.Current.Items[ContextKey] = new CriterDataModel();
                }
                //throw new InvalidOperationException(
                //  "You must register the HttpModule to use per-Request impelementation.");

                return ((CriterDataModel)HttpContext.Current.Items[ContextKey]);
            }
        }

        public System.Data.Entity.Core.Objects.ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)Context).ObjectContext;
            }
        }

        public void DisposeContext()
        {
            var context = ((CriterDataModel)HttpContext.Current.Items[ContextKey]);

            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
