using ProjectManagementApi.Data;
using ProjectManagementApi.Models;

namespace ProjectManagementApi
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.Companies.Any())
            {
                var companies = new List<Companies>
                {
                    new Companies { CompanyName = "TechCorp" },
                    new Companies { CompanyName = "BuildIt Ltd" },
                    new Companies { CompanyName = "SoftSolutions" },
                    new Companies { CompanyName = "GlobalTech" }
                };

                _context.Companies.AddRange(companies);
                _context.SaveChanges();
            }

            if (!_context.Doljnosti.Any())
            {
                var positions = new List<Doljnosti>
                {
                    new Doljnosti {Post = "Руководитель", Password = "password1" },
                    new Doljnosti {Post = "Менеджер проекта", Password = "password2" },
                    new Doljnosti {Post = "Сотрудник", Password = "password3" }
                };

                _context.Doljnosti.AddRange(positions);
                _context.SaveChanges();
            }

            if (!_context.Employees.Any())
            {
                var employees = new List<Employees>
                {
                    new Employees { FirstName = "Alice", LastName = "Johnson", MiddleName = "M.", Email = "alice.johnson@example.com" },
                    new Employees { FirstName = "Bob", LastName = "Smith", MiddleName = "J.", Email = "bob.smith@example.com" },
                    new Employees { FirstName = "Charlie", LastName = "Brown", MiddleName = "L.", Email = "charlie.brown@example.com" },
                    new Employees { FirstName = "Diana", LastName = "White", MiddleName = "K.", Email = "diana.white@example.com" }
                };

                _context.Employees.AddRange(employees);
                _context.SaveChanges();
            }

            if (!_context.Projects.Any())
            {
                var projects = new List<Projects>
                {
                    new Projects
                    {
                        ProjectName = "AI Development",
                        CustomerCompanyID = 1,
                        ContractorCompanyID = 2,
                        StartDate = new DateTime(2024, 1, 1),
                        EndDate = new DateTime(2024, 12, 31),
                        Priority = 1,
                        ProjectManagerID = 2
                    },
                    new Projects
                    {
                        ProjectName = "Cloud Migration",
                        CustomerCompanyID = 3,
                        ContractorCompanyID = 4,
                        StartDate = new DateTime(2023, 5, 10),
                        EndDate = new DateTime(2024, 5, 9),
                        Priority = 2,
                        ProjectManagerID = 1
                    }
                };

                _context.Projects.AddRange(projects);
                _context.SaveChanges();
            }

            if (!_context.Tasks.Any())
            {
                var tasks = new List<Tasks>
                {
                    new Tasks { TaskName = "Set up cloud infrastructure", AuthorID = 1, PerformerID = 3, ProjectID = 2, Status = "ToDo", Comment = "Initial setup", Priority = 1 },
                    new Tasks { TaskName = "Develop AI model", AuthorID = 2, PerformerID = 4, ProjectID = 1, Status = "InProgress", Comment = "Training phase", Priority = 2 },
                    new Tasks { TaskName = "Testing AI accuracy", AuthorID = 3, PerformerID = 1, ProjectID = 1, Status = "Done", Comment = "Testing in progress", Priority = 3 }
                };

                _context.Tasks.AddRange(tasks);
                _context.SaveChanges();
            }

            if (!_context.ProjectEmployees.Any())
            {
                var projectEmployees = new List<ProjectEmployees>
                {
                    new ProjectEmployees { ProjectID = 1, EmployeeID = 1 },
                    new ProjectEmployees { ProjectID = 1, EmployeeID = 2 },
                    new ProjectEmployees { ProjectID = 2, EmployeeID = 3 },
                    new ProjectEmployees { ProjectID = 2, EmployeeID = 4 }
                };

                _context.ProjectEmployees.AddRange(projectEmployees);
                _context.SaveChanges();
            }
            if (!_context.DoljnostiEmployees.Any())
            {
                var doljnostiEmployees = new List<DoljnostiEmployee>
             {
                    new DoljnostiEmployee { EmployeeID = 1, PostID = 1 }, // Alice - Software Engineer
                    new DoljnostiEmployee { EmployeeID = 2, PostID = 2 }, // Bob - Project Manager
                    new DoljnostiEmployee { EmployeeID = 3, PostID = 3 }, // Charlie - QA Engineer
                    new DoljnostiEmployee { EmployeeID = 4, PostID = 4 }, // Diana - Business Analyst

                    // Дополнительные связи для сотрудников с несколькими должностями
                    new DoljnostiEmployee { EmployeeID = 1, PostID = 3 }, // Alice также QA Engineer
                    new DoljnostiEmployee { EmployeeID = 2, PostID = 4 }, // Bob также Business Analyst
                    new DoljnostiEmployee { EmployeeID = 3, PostID = 2 }, // Charlie также Project Manager
                    new DoljnostiEmployee { EmployeeID = 4, PostID = 1 }  // Diana также Software Engineer
    };

                _context.DoljnostiEmployees.AddRange(doljnostiEmployees);
                _context.SaveChanges();
            }
        }
    }
}
