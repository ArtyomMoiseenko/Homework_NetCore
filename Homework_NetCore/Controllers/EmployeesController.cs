using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework_NetCore.Models;
using Homework_NetCore.Models.db;
using Homework_NetCore.Models.Repositories;

namespace Homework_NetCore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly RoleRepository _roleRepository;
        private readonly CompanyContext _context;

        public EmployeesController(IEmployeeRepository employeeRepository, CompanyContext context)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _projectRepository = new ProjectRepository(_context);
            _roleRepository = new RoleRepository(_context);
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var companyContext = await _employeeRepository.Get();
            return View(companyContext);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.FindById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProjectId"] = new SelectList(await _projectRepository.Get(), "Id", "Name");
            ViewData["RoleId"] = new SelectList(await _roleRepository.Get(), "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone,RoleId,ProjectId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Create(employee);
                await _employeeRepository.Save();
                return RedirectToAction("Index");
            }
            ViewData["ProjectId"] = new SelectList(await _projectRepository.Get(), "Id", "Id", employee.ProjectId);
            ViewData["RoleId"] = new SelectList(await _roleRepository.Get(), "Id", "Id", employee.RoleId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.FindById(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(await _employeeRepository.Get(), "Id", "Id", employee.ProjectId);
            ViewData["RoleId"] = new SelectList(await _employeeRepository.Get(), "Id", "Id", employee.RoleId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone,RoleId,ProjectId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(employee);
                    await _employeeRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _employeeRepository.Get(i => i.Id != employee.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProjectId"] = new SelectList(await _employeeRepository.Get(), "Id", "Id", employee.ProjectId);
            ViewData["RoleId"] = new SelectList(await _employeeRepository.Get(), "Id", "Id", employee.RoleId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.FindById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeRepository.FindById(id);
            _employeeRepository.Remove(employee);
            await _employeeRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
