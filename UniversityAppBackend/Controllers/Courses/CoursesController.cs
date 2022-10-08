﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Api.Helpers;
using University.BusinessLogic.Courses;
using University.DataAccess.Context;
using University.DataAccess.Models.DataModels;

namespace University.Api.Controllers.Courses
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ICoursesService _coursesService;

        public CoursesController(
            UniversityDBContext context,
            ICoursesService coursesService
        )
        {
            _context = context;
            _coursesService = coursesService;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ADMIN_ROLE)]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ADMIN_ROLE)]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'UniversityDBContext.Courses'  is null.");
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet, Route("GetCoursesByCategory")]
        public async Task<IActionResult> GetCoursesByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Envie una categoría para poder filtrar.");
            }
            var courses = await _coursesService.GetCoursesByCategory(category);
            return Ok(courses);
        }

        [HttpGet, Route("GetCoursesWithoutCurriculum")]
        public async Task<IActionResult> GetCoursesWithoutCurriculum()
        {
            var courses = await _coursesService.GetCoursesWithoutCurriculum();
            return Ok(courses);
        }

        [HttpGet, Route("GetCurriculumByCourse")]
        public async Task<IActionResult> GetCurriculumByCourse(int courseId)
        {
            var course = await _coursesService.GetCourseById(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course.Curriculum);
        }

        [HttpGet, Route("GetStudentsFromCourse")]
        public async Task<IActionResult> GetStudentsFromCourse(int courseId)
        {
            var students = await _coursesService.GetStudentsFromCourse(courseId);
            return Ok(students);
        }
    }
}
