namespace ProjectManagementApi.Dto
{
    public class TaskDto
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int AuthorID { get; set; }
        public int PerformerID { get; set; }
        public int ProjectID { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
    }
}
