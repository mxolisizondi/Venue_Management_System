using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Venue_Management_System.Models;
using Venue_Management_System.ViewModels;
using System.Web.UI.WebControls;

namespace Venue_Management_System.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context;

        public StudentController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyGroups()
        {

            var userId = User.Identity.GetUserId();
            var listOfMyGroupsId = _context.GroupMembers.Where(s => s.UserId == userId).ToList();
            var distinctMyGroupId = listOfMyGroupsId.Distinct(new DistinctItemComparer()).ToList();
            var onlyId = distinctMyGroupId.Where(s => s.UserId == userId).Select(g => g.GroupId).ToList();
            var myGroup = _context.Groups.Include(s => s.Student).Include(m => m.GroupMembers).Where(g => onlyId.Contains(g.Id)).ToList();

            return View(myGroup);
        }

        public ActionResult CreateGroup()
        {
            //var student = _context.Students.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(Group group)
        {
            //validate name no need to create two group with the same
            var userId = User.Identity.GetUserId();

            if (userId == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                return View(group);
            }
            group.UserId = userId;
            _context.Groups.Add(group);
            _context.SaveChanges();

            var groupId = group.Id;
            var groupOwner = new GroupMember
            {
                GroupId = groupId,
                UserId = userId
            };
            //check if the user is not already added on the groupmember table on the same group
            _context.GroupMembers.Add(groupOwner);
            _context.SaveChanges();
            return RedirectToAction("CreateGroup"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult AddMembers()
        {
            var userId = User.Identity.GetUserId();
            var listOfMyGroups = _context.Groups.Where(s => s.UserId == userId).ToList();

            var groupViewModel = new GroupViewModel
            {
                Groups = listOfMyGroups
            };
            return View(groupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMembers(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new GroupViewModel
                {
                    Groups = _context.Groups.ToList(),
                    GroupMembers = groupViewModel.GroupMembers,
                    Student = groupViewModel.Student
                };
                return View(viewModel);
            }

            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == groupViewModel.Student.StudentNumber);

            if (student == null)
                return HttpNotFound(); // diplay message student not found

            var groupMember = new GroupMember
            {
                UserId = student.UserId,
                Student = student,
                GroupId = groupViewModel.GroupMembers.Id
            };
            _context.GroupMembers.Add(groupMember);
            _context.SaveChanges();
            return RedirectToAction("MyGroups"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult GetEvent()
        {
            return View("GetEvents");
        }

        public JsonResult GetEvents()
        {
            var events = _context.Events.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /*Startup on events add update and delete the ebent*/
        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (ApplicationDbContext  dc = new ApplicationDbContext())
            {
                if (e.Id > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.Id == e.Id).FirstOrDefault();
                    if (v != null)
                    {
                        v.Title = e.Title;
                        v.StartDate = e.StartDate;
                        v.EndDate = e.EndDate;
                        v.Description = e.Description;
                        v.IsFull = e.IsFull;
                        v.Color = e.Color;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.Events.Where(a => a.Id == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        /*end of events*/

        public ActionResult ViewGroupDetails(int? id)
        {
            var myGroup = _context.Groups.Include(s => s.Student)
                                         .Include(m => m.GroupMembers)
                                         .Where(g => g.Id == id).ToList();

            for(int i = 0; i < myGroup.Count; i++)
            {
                
                foreach(var member in myGroup[i].GroupMembers)
                {
                    var student = _context.Students.First(s => s.UserId == member.UserId);

                    member.Student = student;
                }
            }
            

            return View(myGroup);
        }


    }
}