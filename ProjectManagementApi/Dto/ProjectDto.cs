namespace ProjectManagementApi.Dto
{
    public class ProjectDto
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int CustomerCompanyID { get; set; }
        public int ContractorCompanyID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public int ProjectManagerID { get; set; }
    }
}
