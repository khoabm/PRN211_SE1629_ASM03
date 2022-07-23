using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
        void Delete(Member member);
        Member GetMemberById(int? v);
        int GetLatestID();
        public void Add(Member member);
        void Update(Member member);
        public Member login(string email, string pass);
    }
}
