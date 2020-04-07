using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TMS.Application.Common.Interfaces;
using TMS.Application.Common.Models;
using TMS.Application.Issues.Queries.GetIssueDetail;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.WebUI.Controllers
{
    [Route("[controller]")]
    public class LegacyController : Controller
    {
        private string _connectionString;
        private IDateTime _dateTime;

        public LegacyController(IConfiguration configuration, IDateTime datetime)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _dateTime = datetime;
        }

        public async Task<IActionResult> Index()
        {
            var issueList = new List<Issue>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Dependency: [AspNetUsers] -> [Employees] -> [Issues]
                var query = "SELECT " +
                    "[ea].[FullName] AS [AssigneeName], [au].[Email] AS [AssigneeEmail], " +
                    "[ee].[FullName] AS [ReporterName], [ru].[Email] AS [ReporterEmail], * " +
                    "FROM [Issues] AS [i] " +
                    "LEFT OUTER JOIN [Employees] AS [ea] ON [ea].[EmployeeId] = [i].[AssigneeId] " +
                    "LEFT OUTER JOIN [Employees] AS [ee] ON [ee].[EmployeeId] = [i].[ReporterId] " +
                    "LEFT OUTER JOIN [AspNetUsers] AS [au] ON [au].[Id] = [ea].[AppUserId] " +
                    "LEFT OUTER JOIN [AspNetUsers] AS [ru] ON [ru].[Id] = [ee].[AppUserId] ";
                var command = new SqlCommand(query, connection);

                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        Issue issue = new Issue
                        {
                            IssueId = Convert.ToInt64(dataReader[nameof(issue.IssueId)]),
                            Name = Convert.ToString(dataReader[nameof(issue.Name)]),
                            Description = Convert.ToString(dataReader[nameof(issue.Description)]),
                            Priority = (PriorityLevel)Convert.ToInt32(dataReader[nameof(issue.Priority)]),
                            Status = (IssueStatus)Convert.ToInt32(dataReader[nameof(issue.Status)]),
                            AssigneeId = Convert.ToInt64(dataReader[nameof(issue.AssigneeId)]),
                            ReporterId = Convert.ToInt64(dataReader[nameof(issue.ReporterId)]),
                            Assignee = new Employee
                            {
                                ShortName = Convert.ToString(dataReader["AssigneeName"]),
                                FullName = Convert.ToString(dataReader["AssigneeEmail"]),
                            },
                            Reporter = new Employee
                            {
                                ShortName = Convert.ToString(dataReader["ReporterName"]),
                                FullName = Convert.ToString(dataReader["ReporterEmail"]),
                            }
                        };

                        issueList.Add(issue);
                    }
                }

                connection.Close();
            }

            return View(issueList);
        }

        [Route("[action]")]        
        public async Task<IActionResult> Create()
        {
            var employeeList = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Employees";
                var command = new SqlCommand(query, connection);

                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = Convert.ToInt64(dataReader[nameof(employee.EmployeeId)]),
                            ShortName = Convert.ToString(dataReader[nameof(employee.ShortName)]),
                            FullName = Convert.ToString(dataReader[nameof(employee.FullName)]),
                        };
                        employeeList.Add(employee);
                    }
                }

                connection.Close();
            }

            var blankIssue = new IssueDetailForUpsertVm
            {
                Employees = employeeList
                    .Select(e => new FrameDto { Value = e.EmployeeId, Name = e.FullName })
                    .ToList(),
                Name = string.Empty,
                AssigneeName = string.Empty,
                ReporterName = string.Empty,
                Description = string.Empty,
            };
               
            return View(blankIssue);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IssueDetailVm issue)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = $"INSERT INTO Issues (Name, Description, Status, Priority, AssigneeId, ReporterId, Created) " +
                        $"VALUES (N'{issue.Name}', N'{issue.Description}', '{(int)IssueStatus.New}', '{(int)issue.Priority}', '{issue.AssigneeId}', '{issue.ReporterId}', '{_dateTime.Now}')";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                        connection.Close();
                    }
                    return RedirectToAction(nameof(this.Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(this.Create));
            }
        }

        [Route("[action]/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var issue = new IssueDetailForUpsertVm();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryIssue = $"SELECT * FROM Issues " +
                    $"WHERE IssueId='{id}'";
                var commandIssue = new SqlCommand(queryIssue, connection);

                var employeeList = new List<Employee>();
                var queryEmployee = "SELECT * FROM Employees";
                var commandEmployee = new SqlCommand(queryEmployee, connection);

                connection.Open();

                using (SqlDataReader dataReader = await commandIssue.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        issue.Id = Convert.ToInt64(dataReader["IssueId"]);
                        issue.Name = Convert.ToString(dataReader[nameof(issue.Name)]);
                        issue.Description = Convert.ToString(dataReader[nameof(issue.Description)]);
                        issue.Priority = (PriorityLevel)Convert.ToInt32(dataReader[nameof(issue.Priority)]);
                        issue.Status = (IssueStatus)Convert.ToInt32(dataReader[nameof(issue.Status)]);
                        issue.ReporterId = Convert.ToInt64(dataReader[nameof(issue.ReporterId)]);
                        issue.AssigneeId = Convert.ToInt64(dataReader[nameof(issue.AssigneeId)]);
                    }
                }

                using (SqlDataReader dataReader = await commandEmployee.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = Convert.ToInt64(dataReader[nameof(employee.EmployeeId)]),
                            ShortName = Convert.ToString(dataReader[nameof(employee.ShortName)]),
                            FullName = Convert.ToString(dataReader[nameof(employee.FullName)]),
                        };

                        employeeList.Add(employee);
                    }
                }

                issue.Employees = employeeList
                    .Select(e => new FrameDto { Value = e.EmployeeId, Name = e.FullName })
                    .ToList();

                connection.Close();
            }
            return View(issue);
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, IssueDetailVm issue)
        {
            if (ModelState.IsValid && issue.Id == id)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var query = $"Update Issues SET " +
                        $"Name=N'{issue.Name}', " +
                        $"Description=N'{issue.Description}', " +
                        $"Priority='{(int)issue.Priority}', " +
                        $"Status='{(int)issue.Status}', " +
                        $"ReporterId='{issue.ReporterId}', " +
                        $"AssigneeId='{issue.AssigneeId}' " +
                        $"WHERE IssueId='{issue.Id}'";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                        connection.Close();
                    }
                }
                return RedirectToAction(nameof(this.Index));
            }
            else
            {
                return RedirectToAction(nameof(this.Update), new { id = issue.Id });
            }
        }          

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"Delete From Issues Where IssueId='{id}'";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction(nameof(this.Index));
        }
    }
}
