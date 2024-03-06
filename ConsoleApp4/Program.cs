using System.Collections;

public class Program
{
	IList employeeList;
	IList salaryList;
	
	public Program(){
		employeeList = new List<Employee>() { 
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}			
		};
		
		salaryList = new List<Salary>() {
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
		};
	}
	
	public static void Main()
	{		
		Program program = new Program();
		
		program.Task1();
		
		program.Task2();
		
		program.Task3();
	}

	// Print Total Salary of all employees in ascending order.
    public void Task1()
    {
        var monthlySalaries = from employee in employeeList.Cast<Employee>()
                              join salary in salaryList.Cast<Salary>() on employee.EmployeeID equals salary.EmployeeID
                              where salary.Type == SalaryType.Monthly
                              orderby salary.Amount ascending
                              select new
                              {
                                  EmployeeID = employee.EmployeeID,
                                  FirstName = employee.EmployeeFirstName,
                                  LastName = employee.EmployeeLastName,
                                  Amount = salary.Amount
                              };
        Console.WriteLine("Task 1: Total Salary of all employees in ascending order:");
        foreach (var item in monthlySalaries)
        {
            Console.WriteLine($"{item.EmployeeID} {item.FirstName} {item.LastName}: {item.Amount}");
        }
        Console.WriteLine();
    }

    // Print Employee details of 2nd oldest employee including his/her total monthly salary.
    public void Task2()
    {
		var secondOldestEmployee = employeeList.Cast<Employee>().OrderByDescending(emp => emp.Age).Skip(1).FirstOrDefault();
        var totalMonthlySalary = salaryList.Cast<Salary>().Where(sal => sal.EmployeeID == secondOldestEmployee.EmployeeID && sal.Type == SalaryType.Monthly).Sum(sal => sal.Amount);
        Console.WriteLine("Task 2: Employee details of 2nd oldest employee including his/her total monthly salary:");
        Console.WriteLine($"ID: {secondOldestEmployee.EmployeeID}, FirstName: {secondOldestEmployee.EmployeeFirstName}, LastName: {secondOldestEmployee.EmployeeLastName}, Age: {secondOldestEmployee.Age}, TotalSalary: {totalMonthlySalary}");
        Console.WriteLine();
    }

    // Print means of Monthly, Performance, Bonus salary of employees whose age is greater than 30. with name and Id of the employee
    public void Task3()
    {
		
		// Get the employees whose age is greater than 30
		var employees = employeeList.Cast<Employee>().Where(emp => emp.Age > 30);

        // join the employees with their salaries on employee id select all data which is required for the output
        var resultList = from emp in employeeList.Cast<Employee>()
                         join sal in salaryList.Cast<Salary>() on emp.EmployeeID equals sal.EmployeeID
                         select new { emp.EmployeeID, emp.EmployeeFirstName, emp.EmployeeLastName, sal.Amount, sal.Type };

		foreach (var emp in employees)
		{
			// get the salaries of the employee
			double mean = 0;
			int count = 0;
			foreach (var item in resultList)
			{
                if (item.EmployeeID == emp.EmployeeID)
				{
                    mean += item.Amount;
                    count++;
                }
            }
			mean = Math.Round(mean / count, 2);
			Console.WriteLine($"Employee ID: {emp.EmployeeID} Name: {emp.EmployeeFirstName} {emp.EmployeeLastName} Mean Salary: {mean}");
		}

    }
}

public enum SalaryType{
	Monthly,
	Performance,
	Bonus
}

public class Employee{
	public int EmployeeID { get; set; }
	public string EmployeeFirstName { get; set; }
	public string EmployeeLastName { get; set; }
	public int Age { get; set; }	
}

public class Salary{
	public int EmployeeID { get; set; }
	public int Amount { get; set; }
	public SalaryType Type { get; set; }
}