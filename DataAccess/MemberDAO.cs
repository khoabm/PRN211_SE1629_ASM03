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
        //Singleton
        private static MemberDAO? instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }

        public Member GetMembersByID(int? v)
        {
            Member? member = null;
            try
            {
                using FStoreContext context = new FStoreContext();
                member = context.Members.FirstOrDefault(o => o.MemberId == v);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return member;
        }

        public void Update(Member member)
        {
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                _fStoreContext.Entry<Member>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _fStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Add(Member member)
        {
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                _fStoreContext.Members.Add(member);
                _fStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetLatestID()
        {
            int index = 0;
            using FStoreContext context = new FStoreContext();
            return index = context.Members.Max(m => m.MemberId);
        }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Member> GetMembers()
        {
            List<Member> members = new List<Member>();
            try
            {
                using FStoreContext _fStoreContext = new FStoreContext();
                members = _fStoreContext.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }

        public void Delete(Member member)
        {
            try
            {
                using FStoreContext context = new FStoreContext();
                var foundMem = context.Members.SingleOrDefault(o => o.MemberId == member.MemberId);
                context.Members.Remove(foundMem);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
