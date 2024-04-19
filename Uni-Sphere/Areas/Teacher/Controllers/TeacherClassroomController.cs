using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories.IRepositories;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherClassroomController(ITeacherFunctionality teacherFunctionality, IClassroomRepository classroomRepository) : Controller
    {
        private readonly ITeacherFunctionality _teacherFunctionality = teacherFunctionality;
        private readonly IClassroomRepository _classroomRepository = classroomRepository;

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get the teacher's id who is currently logged-in
            var teacherId = 2; // Change this to get the actual teacher's id
            var courses = await _teacherFunctionality.GetCoursesByTeacherAsync(teacherId);
            var firstCourse = courses.FirstOrDefault();

            var model = new AddClassroomRequest
            {
                CourseId = firstCourse.Id,
                Courses = courses.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Batch = firstCourse.Batch,
                Sections = string.Join(", ", firstCourse.Sections)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClassroomRequest model)
        {
            if (ModelState.IsValid)
            {
                var classroom = new Classrooms
                {
                    Name = model.Name,
                    CourseId = model.CourseId,
                    Batch = model.Batch,
                    TeacherId = 2 // Change this to get the actual teacher's id
                };

                // Retrieve students enrolled in the specified course, batch, and sections
                var students = await _teacherFunctionality.GetStudentsByCourseBatchSectionsAsync(model.CourseId, model.Batch, model.Sections);
                var includedStudents = new List<Students>();

                foreach (var student in students)
                {
                    // Add the student to the classroom
                    includedStudents.Add(student);
                }

                classroom.Students = includedStudents;
                await _classroomRepository.AddClassroomAsync(classroom);
                TempData["Success"] = "Classroom created successfully";

                return RedirectToAction("List");
            }

            return View(model);
        }


        [HttpGet]
        // Display all the classroom of the teacher
        public async Task<IActionResult> List()
        {
            int teacherId = 2;
            var model = await _classroomRepository.GetAllAsync(teacherId);

            return View(model);
        }

        [HttpGet]
        // View Individual Classroom
        public async Task<IActionResult> Classroom(int classroomId)
        {
            var model = await _classroomRepository.GetClassroomByIdAsync(classroomId);

            return View(model);
        }

        [HttpGet]
        // View students in the classroom
        public async Task<IActionResult> DisplayStudents(int classroomId)
        {
            var model = await _classroomRepository.GetClassroomByIdAsync(classroomId);
            return View(model.Students);
        }

        [HttpGet]
        // Discussion Posts within a classroom
        public IActionResult CreatePost(int classroomId)
        {
            var model = new CreatePostRequest
            {
                ClassRoomsId = classroomId
            };

            return View(model);
        }

        // Create a post within a classroom
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostRequest createPostRequest)
        {
            if (ModelState.IsValid)
            {
                createPostRequest.Content ??= "";
                var post = new DiscussionPost
                {
                    Title = createPostRequest.Title,
                    Content = createPostRequest.Content,
                    CreatedAt = DateTime.Now,
                    ClassroomsId = createPostRequest.ClassRoomsId
                };  

                await _classroomRepository.AddPostAsync(post);
                TempData["Success"] = "Post created successfully";

                return RedirectToAction("Classroom", new { classroomId = createPostRequest.ClassRoomsId });
            }

            return View(createPostRequest);
        }

        // Delete a post within a classroom
        [HttpGet]
        public async Task<IActionResult> DeletePost(int postId, int classroomId)
        {
            var status = await _classroomRepository.DeletePostAsync(postId);
            if(status)
                TempData["Success"] = "Post deleted successfully";
            else
                TempData["Error"] = "Post deletion failed";

            return RedirectToAction("Classroom", new { classroomId = classroomId });
        }

        // Edit a post within a classroom
        [HttpGet]
        public async Task<IActionResult> EditPost(int postId, int classroomId)
        {
            var post = await _classroomRepository.GetPostByIdAsync(postId);

            var model = new EditPostRequest
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ClassRoomsId = classroomId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(EditPostRequest editPostRequest)
        {
            if (ModelState.IsValid)
            {
                var post = new DiscussionPost
                {
                    Id = editPostRequest.Id,
                    Title = editPostRequest.Title,
                    Content = editPostRequest.Content,
                    CreatedAt = DateTime.Now,
                    ClassroomsId = editPostRequest.ClassRoomsId
                };

                var status = await _classroomRepository.EditPostAsync(post);
                if(status)
                    TempData["Success"] = "Post edited successfully";
                else
                    TempData["Error"] = "Post edit failed";

                return RedirectToAction("Classroom", new { classroomId = editPostRequest.ClassRoomsId });
            }

            return View(editPostRequest);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetCourseDetails(int courseId)
        {
            var teacherId = 2;
            var courses = await _teacherFunctionality.GetCoursesByTeacherAsync(teacherId);
            // Select the course with the given courseId
            var course = courses.FirstOrDefault(c => c.Id == courseId);

            var batch = course.Batch;
            // Extract the sections
            var sections = course.Sections.ToList();
            // Get the course details

            return Json(new { batch = batch, sections = sections });
        }
        #endregion

    }
}