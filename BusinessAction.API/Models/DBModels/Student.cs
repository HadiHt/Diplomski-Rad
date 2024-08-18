﻿namespace BusinessAction.API.Models.DBModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int? MajorID { get; set; }
        public Major Major { get; set; }
    }
}
