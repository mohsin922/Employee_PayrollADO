using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollServiceADO
{
    public class EmployeeRepository
    {
        //Give path for Database Connection
        public static string connection = @"Server=LAPTOP-AT654SBH\MSSQLSERVER01;Database=payroll_service;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection sqlConnection = new SqlConnection(connection);
        public void GetSqlData()
        {
            try
            {
                EmployeePayroll employeePayroll = new EmployeePayroll();
                using (this.sqlConnection)
                {

                    //Open Connection
                    sqlConnection.Open();
                    string query = "select * from employee_payroll";
                    //Pass query to TSql, A SqlCommand object allows you to query and send commands to a database
                    SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
                    // SqlDataReader Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    //Check if sqlDataReader has Rows
                    if (sqlDataReader.HasRows)
                    {
                        //Read each row
                        while (sqlDataReader.Read())
                        {
                            //Read data SqlDataReader and store 
                            employeePayroll.ID = sqlDataReader.GetInt32(0);
                            employeePayroll.Name = sqlDataReader["Name"].ToString();
                            employeePayroll.BasicPay = Convert.ToDouble(sqlDataReader["BasicPay"]);
                            employeePayroll.Deduction = Convert.ToDouble(sqlDataReader["Deduction"]);
                            employeePayroll.IncomeTax = Convert.ToDouble(sqlDataReader["IncomeTax"]);
                            employeePayroll.TaxablePay = Convert.ToDouble(sqlDataReader["TaxablePay"]);
                            employeePayroll.NetPay = Convert.ToDouble(sqlDataReader["NetPay"]);
                            employeePayroll.Gender = Convert.ToChar(sqlDataReader["Gender"]);
                            employeePayroll.PhoneNumber = Convert.ToInt64(sqlDataReader["PhoneNumber"]);
                            employeePayroll.Department = sqlDataReader["Department"].ToString();
                            employeePayroll.Address = sqlDataReader["Address"].ToString();
                            employeePayroll.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"]);

                            //Display Data
                            Console.WriteLine("\nEmployee ID: {0} \t Employee Name: {1} \nBasic Pay: {2} \t Deduction: {3} \t Income Tax: {4} \t Taxable Pay: {5} \t NetPay: {6} \nGender: {7} \t PhoneNumber: {8} \t Department: {9} \t Address: {10}", employeePayroll.ID, employeePayroll.Name, employeePayroll.BasicPay, employeePayroll.Deduction, employeePayroll.IncomeTax, employeePayroll.TaxablePay, employeePayroll.NetPay, employeePayroll.Gender, employeePayroll.PhoneNumber, employeePayroll.Department, employeePayroll.Address);
                        }
                    }
                    else
                    {
                        Console.WriteLine("NO Data Found");
                    }
                    //Close sqlDataReader Connection
                    sqlDataReader.Close();

                    //Close Connection
                    this.sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public bool AddEmployeeDetails(EmployeePayroll employeePayroll)
        {
            try
            {
                using (this.sqlConnection)
                {
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", employeePayroll.Name);
                    command.Parameters.AddWithValue("@BasicPay", employeePayroll.BasicPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Gender", employeePayroll.Gender);
                    command.Parameters.AddWithValue("@PhoneNumber", employeePayroll.PhoneNumber);
                    command.Parameters.AddWithValue("@Department", employeePayroll.Department);
                    command.Parameters.AddWithValue("@Address", employeePayroll.Address);
                    command.Parameters.AddWithValue("@TaxablePay", employeePayroll.TaxablePay);
                    command.Parameters.AddWithValue("@Deduction", employeePayroll.Deduction);
                    command.Parameters.AddWithValue("@IncomeTax", employeePayroll.IncomeTax);
                    command.Parameters.AddWithValue("@NetPay", employeePayroll.NetPay);
                    
                    this.sqlConnection.Open();
                    var result = command.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if(result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        //UseCase 3: Update Salary to 3000000
        public void UpdateSalary()
        {
            //Open Connection
            sqlConnection.Open();
            string query = "update employee_payroll set BasicPay = 3000000.00 where Name = 'Mohsin'";
            //Pass query to TSql
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Console.WriteLine("Updated!");
            }
            else
            {
                Console.WriteLine("Not Updated!");
            }
            //Close Connection
            sqlConnection.Close();
            GetSqlData();
        }
    }
}