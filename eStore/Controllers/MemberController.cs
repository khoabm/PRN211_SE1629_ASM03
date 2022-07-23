using BusinessObject.DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        private IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            this._memberRepository = memberRepository;
        }

        // GET: MemberController
        public ActionResult Index()
        {
            
            List<Member> members = new List<Member>();
            if (HttpContext.Session.GetInt32("user") != null)
            {
                members = _memberRepository.GetMembers().ToList();
            }
            else return RedirectToAction("Login", "Home");
            return View(members);
        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            int loggedIn = CheckLogin();
            if (loggedIn == -1)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (loggedIn == 0)
                {
                    var member = _memberRepository.GetMemberById(id);
                    if (member != null)
                        return View(member);
                    else return NotFound();
                }
                else
                {
                    ViewBag.Error = "you can not see information of other member";
                    return RedirectToAction("Index", "Member");
                }
            }
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            int loggedIn = CheckLogin();
            if (loggedIn == -1)
                return RedirectToAction("Login", "Home");
            int memberID = _memberRepository.GetMembers().Max(m => m.MemberId) + 1;
            ViewData["id"] = memberID;
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _memberRepository.Add(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message=ex.Message;
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            int loggedIn = CheckLogin();
            if (loggedIn == -1)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (loggedIn == 0)
                {
                    var member = _memberRepository.GetMemberById(id);
                    if (member != null)
                        return View(member);
                    else return NotFound();
                }
                else
                {
                    ViewBag.Error = "you can not see information of other member";
                    return RedirectToAction("Index", "Member");
                }
            }
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Member member)
        {
            try
            {
                if(id != member.MemberId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _memberRepository.Update(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MemberController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int loggedIn = CheckLogin();
            if (loggedIn == -1)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (loggedIn != 0)
            {
                ViewBag.Message = "you dont have this permission";
                return RedirectToAction("Index", "Member");
            }    
            var member = _memberRepository.GetMemberById(id);
            if (member != null)
                return View(member);
            else return NotFound();
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Del(int id)
        {
            try
            {
                _memberRepository.Delete(_memberRepository.GetMemberById(id));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public int CheckLogin()
        {
            var session = HttpContext.Session.GetInt32("user");
            if (session == null)
                return -1; //not logged in
            else return (int)session; //return memberID
            
        }
    }
}
