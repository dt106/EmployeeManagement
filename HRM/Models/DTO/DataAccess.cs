using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HRM.DTO
{
    public class Employee
    {
        public int EmployeeId           {get;set;}
        public string EmployeeName      {get;set;}
        public Byte Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CCCD { get; set; }
        public DateTime Birthday { get; set; }
        public Byte JobtitleId { get; set; }
        public Byte PositionId { get; set; }
        public Byte DepartmentId { get; set; }
        public string JobtitleName { get; set; }
        public string PositionName { get; set; }

        public string DepartmentName { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public Byte Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string ModifiedBy { get; set; }

    }
    public class EmployeeDepartment
    {
        public Byte DepartmentId { get; set; }
        public int EmployeeId { get; set; }
    }
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Byte Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CCCD { get; set; }
        public DateTime Birthday { get; set; }
        public string JobtitleName { get; set; }
        public string PositionName { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public int Manager { get; set; }
        public int Deputy { get; set; }  
    }
    public class SalaryFix
    {
      public int STT                      {get;set;}
      public int EmployeeId               {get;set;}
      public string EmployeeName          {get;set;}
      public decimal Money                {get;set;}
      public decimal Bonus                {get;set;}
      public DateTime StartDate           {get;set;}
      public DateTime EndDate             {get;set;}
      public Boolean Status { get; set; }
      public string Decription { get; set; }

    }

    public class Salary
    {
      public Int16 Year                                 {get;set;}
      public byte Month                                 {get;set;}
      public int EmployeeId                             {get;set;}
      public string EmployeeName                        {get;set;}
      public byte DepartmentId                          {get;set;}
      public decimal DaysWork                           {get;set;}
      public Byte Days { get; set; }
      public decimal Money                             {get;set;}
      public decimal Bonus                              {get;set;}
      public decimal RealSalary                         {get;set;}
      public DateTime CreateTime                        {get;set;}
      public string CreateBy                            {get;set;}
      public DateTime ModifiedTime                      {get;set;}
      public string ModifiedBy { get; set; }
    }
    public class SalaryFixInfo
    {
        public List<SalaryFix> salaryFixes { get; set; }
        public SalaryFix salaryfix { get; set; }
    }
    public class EmployeeHistory
    {
        public int STT { get; set; }
      public int EmployeeId               {get;set;}
        public string EmployeeName { get; set; }
      public DateTime StartDate           {get;set;}
      public DateTime EndDate             {get;set;}
      public byte DepartmentNew           {get;set;}
      public byte PositionNew              {get;set;}
      public byte JobtitleNew             {get;set;}
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string JobtitleName { get; set; }
        public string Decription { get; set; }
    }
    public class EmployeeInfo
    {
        public Employee employee { get; set; }
        public List<SalaryFix> salaryFixes { get; set; }
        public List<EmployeeHistory> employeeHistories { get; set; }
        public EmployeeHistory EmployeeHistory { get; set; }
    }
    public class ListEmployee
    {
       public   List<Employee> Employees { get; set; }
    }
    public class EmployeeHistoryQueryResult
    {
        public List<EmployeeHistory> Histories { get; set; }
        public int Response { get; set; }
    }
    public class Position
    {
        public int PositionId        {get; set; }
        public string PositionName { get; set; }
    }

    public class Department
    {
      public int DepartmentId              {get; set;}
      public string DepartmentName        {get; set;}
      public DateTime DateOfEstablishment {get; set;}
      public int Manager                  {get; set;}
      public int  Deputy                 {get; set;}
      public short Status                  {get; set;}
      public DateTime Createtime          { get; set; }
        public string ManagerName { get; set; }
        public string DeputyName { get; set; }

    }

    public class WorkStatus
    {

    }

    public class Timekeeping
    {
      public int LogID             {get; set ;}
      public int EmployeeID        {get; set ;}
      public DateTime LogDate      {get; set ;}
      public TimeSpan LogTime      {get; set ;}
      public string DayName        {get; set ;}
      public byte Status           {get; set ;}
      public DateTime CreateTime   {get; set ;}
      public string CreateBy       {get; set ;}
      public DateTime ModifiedTime {get; set ;}
      public string ModifiedBy { get; set; }
    }
    public class TimekeepingEmp
    {
        public int LogID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime LogDate { get; set; }
        public TimeSpan LogTime { get; set; }
        public string DayName { get; set; }
    }
    public class DailyLog
    {
          public int ID                      {get;set;}
          public int EmployeeID              {get;set;}
        public string EmployeeName { get; set; }
          public DateTime DayLog             {get;set;}
          public string DayDetail            {get;set;}
          public TimeSpan BeginLog           {get;set;}
          public TimeSpan EndLog             {get;set;}
          public decimal HourWork            {get;set;}
          public decimal ReduceTime          {get;set;}
          public byte LogStatus              {get;set;}
        public string StatusDetail { get; set; }
          public DateTime CreateTime         {get;set;}
          public string CreateBy             {get;set;}
          public decimal RealHour { get; set; }
    } 
    public class User
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public short UserType { get; set; }
        public short Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
    }
    public class UserType
    {
        public int UserTypeId { get; set; }
        public string userTypeName { get; set; }
        public string Decription { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
    }
    public class LogMonth
    {
      public Int16 Year { get; set; }
      public Int16 Month { get; set; }
      public byte DepartmentID                     {get;set;}
      public string DepartmentName { get; set; }
      public int EmployeeId                        {get;set;}
      public string EmployeeName                   {get;set;}
      public byte N1                                {get;set;}
      public byte N2                                {get;set;}
      public byte N3                                {get;set;}
      public byte N4                                {get;set;}
      public byte N5                                {get;set;}
      public byte N6                                {get;set;}
      public byte N7                                {get;set;}
      public byte N8                                {get;set;}
      public byte N9                                {get;set;}
      public byte N10                               {get;set;}
      public byte N11                               {get;set;}
      public byte N12                               {get;set;}
      public byte N13                               {get;set;}
      public byte N14                               {get;set;}
      public byte N15                               {get;set;}
      public byte N16                               {get;set;}
      public byte N17                               {get;set;}
      public byte N18                               {get;set;}
      public byte N19                               {get;set;}
      public byte N20                               {get;set;}
      public byte N21                               {get;set;}
      public byte N22                               {get;set;}
      public byte N23                               {get;set;}
      public byte N24                               {get;set;}
      public byte N25                               {get;set;}
      public byte N26                               {get;set;}
      public byte N27                               {get;set;}
      public byte N28                               {get;set;}
      public byte N29                               {get;set;}
      public byte N30                               {get;set;}
      public byte N31                               {get;set;}
      public decimal HourWork                      {get;set;}
      public decimal HourOff                       {get;set;}
      public byte Status                           {get;set;}
      public DateTime CreateTime                   {get;set;}
      public string CreateBy                       {get;set;}
      public DateTime ModifiedTime                 {get;set;}
      public string ModifiedBy                     {get;set;}
    }

    public class Jobtitle
    {
        public Byte JobtitleId { get; set; }
        public string JobtitleName { get; set; }
        public string Decription { get; set; }
    }
}   