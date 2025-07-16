using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataUtilityCore
{
    public class CriterPrincipal : IPrincipal
    {
        public CriterPrincipal(IPrincipal user)
        {
            Identity = new GenericIdentity(user.Identity.Name);
        }

        public CriterPrincipal(string userName, bool isSpid)
        {
            Identity = new GenericIdentity(userName);
            this.IsSpidUser = isSpid;
        }

        public CriterPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

        #region IPrincipal
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            return System.Web.Security.Roles.IsUserInRole(role);
        }
        #endregion

        public bool IsSpidUser { get; set; }
    }

    public class CriterPrincipalSerializeModel
    {
        public bool IsSpidUser { get; set; }
    }
}
