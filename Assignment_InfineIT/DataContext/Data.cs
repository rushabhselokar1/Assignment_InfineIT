using Assignment_InfineIT.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;

namespace Assignment_InfineIT.DataContext
{
    public class Data
    {
        private readonly string connString = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public List<Department> GetDepartmentsFromDatabase()
        {
            List<Department> departments = new List<Department>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetDepartments", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Department department = new Department
                            {
                                Code = Convert.ToInt32(reader["Code"]),
                                Name = reader["Name"].ToString(),
                                Sort_No = Convert.ToByte(reader["Sort_No"])
                            };

                            departments.Add(department);
                        }
                    }
                }
            }

            return departments;
        }


        public List<Country> GetCountryFromDatabase()
        {
            List<Country> countries = new List<Country>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetCountries", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country
                            {
                                Code = Convert.ToInt32(reader["Code"]),
                                Name = reader["Name"].ToString()
                            };

                            countries.Add(country);
                        }
                    }
                }
            }

            return countries;
        }


        public List<State> GetStateFromDatabase()
        {
            List<State> states = new List<State>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetStates", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            State state = new State
                            {
                                Code = Convert.ToInt32(reader["Code"]),
                                Name = reader["Name"].ToString(),
                                Country_Code = Convert.ToInt32(reader["Country_Code"])
                            };

                            states.Add(state);
                        }
                    }
                }
            }

            return states;
        }


        public List<City> GetCityFromDatabase()
        {
            List<City> cities = new List<City>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("GetCities", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            City city = new City
                            {
                                Code = Convert.ToInt32(reader["Code"]),
                                Name = reader["Name"].ToString(),
                                State_Code = Convert.ToInt32(reader["State_Code"]),
                                Country_Code = Convert.ToInt32(reader["Country_Code"])
                            };

                            cities.Add(city);
                        }
                    }
                }
            }

            return cities;
        }


        public void CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            using (var connection = new MySqlConnection(connString))
            {
                try
                {
                    connection.Open();

                    string insertSql = "INSERT INTO EMP_MAS (Code, First_Name, Middle_Name, Last_Name, Full_Name, DOB, Salary, Dept_Code, Country_Code, State_Code, City_Code) VALUES (@Code, @First_Name, @Middle_Name, @Last_Name, @Full_Name, @DOB, @Salary, @Dept_Code, @Country_Code, @State_Code, @City_Code);";

                    MySqlCommand command = new MySqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@Code", employee.Code);
                    command.Parameters.AddWithValue("@First_Name", employee.First_Name);
                    command.Parameters.AddWithValue("@Middle_Name", employee.Middle_Name);
                    command.Parameters.AddWithValue("@Last_Name", employee.Last_Name);
                    command.Parameters.AddWithValue("@Full_Name", employee.Full_Name);
                    command.Parameters.AddWithValue("@DOB", employee.DOB);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Dept_Code", employee.Dept_Code);
                    command.Parameters.AddWithValue("@Country_Code", employee.Country_Code);
                    command.Parameters.AddWithValue("@State_Code", employee.State_Code);
                    command.Parameters.AddWithValue("@City_Code", employee.City_Code);

                    command.ExecuteNonQuery();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    
                }
                catch (Exception ex)
                {
                    // Handle other exceptions here (not specific to MySQL)
                }
            }
        }



        public List<Employee> GetEmployeesWithFullNameAndCode()
        {
            List<Employee> employees = new List<Employee>();

            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();

                string query = "SELECT Code, Full_Name FROM EMP_MAS";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                Code = reader["Code"].ToString(),
                                Full_Name = reader["Full_Name"].ToString()
                                
                            };

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

        public Employee GetEmployeeByCode(string code)
        {
            Employee employee = null;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                // Query to retrieve the employee by code
                string selectSql = "SELECT * FROM EMP_MAS WHERE Code = @Code";

                MySqlCommand command = new MySqlCommand(selectSql, connection);
                command.Parameters.AddWithValue("@Code", code);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Retrieve employee details from the database
                        employee = new Employee
                        {
                            Code = reader.GetString("Code"),
                            First_Name = reader.GetString("First_Name"),
                            Middle_Name = reader.GetString("Middle_Name"),
                            Last_Name = reader.GetString("Last_Name"),
                            Full_Name = reader.GetString("Full_Name"),
                            DOB = reader.GetDateTime("DOB"),
                            Salary = reader.GetInt32("Salary"),
                            Dept_Code = reader.GetByte("Dept_Code"),
                            Country_Code = reader.GetInt32("Country_Code"),
                            State_Code = reader.GetInt32("State_Code"),
                            City_Code = reader.GetInt32("City_Code")
                        };
                    }
                }
            }

            return employee;
        }

        public string GetDepartmentNameWithId(int id)
        {
            string departmentName = null;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                using (var command = new MySqlCommand("GetDepartmentNameWithId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@p_id", id));
                    command.Parameters.Add(new MySqlParameter("@p_departmentName", MySqlDbType.VarChar, 255));
                    command.Parameters["@p_departmentName"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    departmentName = command.Parameters["@p_departmentName"].Value.ToString();
                }
            }

            return departmentName;
        }


        public string GetCountryNameWithId(int id)
        {
            string countryName = null;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                using (var command = new MySqlCommand("GetCountryNameWithId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@p_id", id));
                    command.Parameters.Add(new MySqlParameter("@p_countryName", MySqlDbType.VarChar, 255));
                    command.Parameters["@p_countryName"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    countryName = command.Parameters["@p_countryName"].Value.ToString();
                }
            }

            return countryName;
        }


        public string GetCityNameWithId(int id)
        {
            string cityName = null;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                using (var command = new MySqlCommand("GetCityNameWithId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@p_id", id));
                    command.Parameters.Add(new MySqlParameter("@p_cityName", MySqlDbType.VarChar, 255));
                    command.Parameters["@p_cityName"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    cityName = command.Parameters["@p_cityName"].Value.ToString();
                }
            }

            return cityName;
        }


        public string GetStateNameWithId(int id)
        {
            string stateName = null;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                using (var command = new MySqlCommand("GetStateNameWithId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@p_id", id));
                    command.Parameters.Add(new MySqlParameter("@p_stateName", MySqlDbType.VarChar, 255));
                    command.Parameters["@p_stateName"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    stateName = command.Parameters["@p_stateName"].Value.ToString();
                }
            }

            return stateName;
        }


        public string GetEmployeeCodeByCode(string code)
        {
            string employeeCode = null;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                using (var command = new MySqlCommand("GetEmployeeCodeByCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@p_code", code));
                    command.Parameters.Add(new MySqlParameter("@p_employeeCode", MySqlDbType.VarChar, 255));
                    command.Parameters["@p_employeeCode"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    employeeCode = command.Parameters["@p_employeeCode"].Value.ToString();
                }
            }

            return employeeCode;
        }


        public bool EmployeeCodeExists(string code)
        {
            bool codeExists = false;

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                using (var command = new MySqlCommand("EmployeeCodeExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new MySqlParameter("@p_code", code));
                    command.Parameters.Add(new MySqlParameter("@p_codeExists", MySqlDbType.Bit));
                    command.Parameters["@p_codeExists"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    codeExists = Convert.ToBoolean(command.Parameters["@p_codeExists"].Value);
                }
            }

            return codeExists;
        }

        public List<CityStatisticsViewModel> GetCityStatistics()
        {
            string query = @"
                SELECT CITY_MAS.Name AS City,
                       SUM(EMP_MAS.Salary) AS TotalSalary,
                       COUNT(EMP_MAS.Code) AS EmployeeCount
                FROM EMP_MAS
                INNER JOIN CITY_MAS ON EMP_MAS.City_Code = CITY_MAS.Code
                GROUP BY CITY_MAS.Name;";

            return ExecuteQuery(query);
        }

        private List<CityStatisticsViewModel> ExecuteQuery(string query)
        {
            List<CityStatisticsViewModel> cityStatistics = new List<CityStatisticsViewModel>();

            using (var connection = new MySqlConnection(connString))
            {
                connection.Open();

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CityStatisticsViewModel cityStat = new CityStatisticsViewModel
                            {
                                Name = reader["City"].ToString(),
                                TotalSalary = Convert.ToInt32(reader["TotalSalary"]),
                                EmployeeCount = Convert.ToInt32(reader["EmployeeCount"])
                            };
                            cityStatistics.Add(cityStat);
                        }
                    }
                }
            }

            return cityStatistics;
        }

        public List<DepartmentStatisticsViewModel> GetDepartmentStatistics()
        {
            List<DepartmentStatisticsViewModel> departmentStatistics = new List<DepartmentStatisticsViewModel>();

            try
            {
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("GetDepartmentStatistics", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DepartmentStatisticsViewModel departmentStat = new DepartmentStatisticsViewModel
                            {
                                Name = reader["Department"].ToString(),
                                TotalSalary = Convert.ToInt32(reader["TotalSalary"]),
                                EmployeeCount = Convert.ToInt32(reader["EmployeeCount"])
                            };
                            departmentStatistics.Add(departmentStat);
                        }
                    }
                }

                return departmentStatistics;
            }
            catch (Exception ex)
            {
                
                return new List<DepartmentStatisticsViewModel>(); 
            }
        }


        public bool DeleteEmployeeByCode(string code)
        {
            try
            {
                using (var connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("DeleteEmployeeByCode", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameter for the stored procedure
                    command.Parameters.AddWithValue("@p_Code", code);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
               
                return false; 
            }
        }


    }
}