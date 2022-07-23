using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance;
        private static readonly object InstanceLock = new object();
        public static MemberDAO Instance { 
            get 
            {
                lock (InstanceLock)
                {
                    if(instance == null)
                    {
                        instance = new MemberDAO();
                    }
                }
                return instance;
            } 
        }
        public Member checkLogin(string email, string pass)
        {
            try
            {
                using FStoreContext  context = new FStoreContext();
                Member admin = context.Admin();
                if (admin.Email.Equals(email) && admin.Password.Equals(pass))
                {
                    return admin;
                }
                var query = (from mem in context.Members.ToList()
                             where mem.Email.Equals(email) && mem.Password.Equals(pass)
                             select mem).SingleOrDefault();
                return query;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
