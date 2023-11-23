﻿using CourseManagerAPI.Models;

namespace CourseManagerAPI.Entities
{
    public class Log : BaseEntity<int>
    {
        public string? UserName { get; set; }
        public string Description { get; set; }
    }
}
