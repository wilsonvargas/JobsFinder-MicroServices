﻿using Applicants.Api.Models;
using Applicants.Api.Services.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Applicants.Api.Services
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly string _connectionString;

        public ApplicantRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Applicant>> GetAll()
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<Applicant>("SELECT * FROM Applicants");
            }
        }

        public async Task<int> AddApplicantSubmission(ApplicantSubmission applicantSubmission)
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(
                    "insert ApplicantSubmissions values(@jobId,@applicantId,@title,@submissionDate)",
                    new
                    {
                        jobId = applicantSubmission.JobId,
                        applicantId = applicantSubmission.ApplicantId,
                        title = applicantSubmission.Title,
                        submissionDate = applicantSubmission.SubmissionDate
                    });
            }
        }
    }
}
