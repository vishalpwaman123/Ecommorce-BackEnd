using Ecommerce.Common.Model;
using Ecommerce.Helper;
using Ecommerce.Processor;
using Ecommerce.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Services
{
    public class EcommerceServiceRL : IEcommerceServiceRL
    {
        public readonly ILogger<EcommerceServiceRL> _logger;
        public readonly IConfiguration _configuration;
        private SqlConnection sqlConnectionVariable;
        const int ConnectonTimeout = 180;
        int status = 0;

        public EcommerceServiceRL(ILogger<EcommerceServiceRL> logger)
        {
            _logger = logger;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            this.sqlConnectionVariable = new SqlConnection(_configuration["ConnectionStrings:DBConnection"]);
        }

        /*public IConfiguration GetConfiguration()
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            return builder;
        }
*/
        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            string Password = string.Empty;
            SignUpResponse response = new SignUpResponse() {
                IsSuccess = true
            };
            try
            {
                if (sqlConnectionVariable != null)
                {
                    string sqlQuery = SqlQueries.SignUp;
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, this.sqlConnectionVariable);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandTimeout = ConnectonTimeout;

                    Password = PasswordProcessing.Encrypt(request.Password.ToString(), _configuration["SecurityKey"]);
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@EmailID", request.EmailID);
                    sqlCommand.Parameters.AddWithValue("@SecurePassword", Password);
                    this.sqlConnectionVariable.Open();
                    status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Error At SignUp Query";
                        return response;
                    }
                    response.Message = "SuccessFully SignUp";
                }

            }catch(Exception ex)
            {
                _logger.LogError($"SignUp Repository Error => {ex}");
                response.Message = ex.ToString();
                response.IsSuccess = false;
            }
            finally
            {
                this.sqlConnectionVariable.Close();
            }
            return response;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            response.login = new Login();
            response.IsSuccess = false;
            try
            {
                string SqlQuery = SqlQueries.Login;
                SqlCommand sqlCommand = new SqlCommand(SqlQuery, sqlConnectionVariable);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = ConnectonTimeout;
                string EncryptPassword = PasswordProcessing.Encrypt(request.Password, _configuration["SecurityKey"]);
                sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                sqlCommand.Parameters.AddWithValue("@SecurePassword", EncryptPassword);
                sqlConnectionVariable.Open();
                using(DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                {
                    if (dbDataReader.HasRows)
                    {
                        response.IsSuccess = true;
                        response.Message = "SuccessFully Login";
                        await dbDataReader.ReadAsync();
                        response.login.UserID = dbDataReader["UserID"] != DBNull.Value ? Convert.ToInt32(dbDataReader["UserID"]) : 0;
                        response.login.EmailID = dbDataReader["EmailID"]!= DBNull.Value ? dbDataReader["EmailID"].ToString():string.Empty;
                        response.login.UserName = dbDataReader["UserName"] !=DBNull.Value ? dbDataReader["UserName"].ToString(): string.Empty;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Login Unsuccessfully.";
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error Occur at Login {ex}");
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            finally
            {
                if (sqlConnectionVariable != null)
                {
                    sqlConnectionVariable.Close();
                }
            }

            return response;
        }

        
    }
}
