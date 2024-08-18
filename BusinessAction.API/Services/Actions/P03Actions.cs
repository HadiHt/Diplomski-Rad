using BusinessAction.API.IRepositories;
using BusinessAction.API.Models;
using BusinessAction.API.Models.DBModels;
using Newtonsoft.Json;
using System.Text;

namespace BusinessAction.API.Services.Actions
{
    public class P03Actions
    {
        private readonly ILogger<P03Actions> _logger;
        private readonly HttpClient _httpClient;
        private readonly ICourseOfferingRepository _courseOfferingRepository;
        private readonly IMajorRepository _majorRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollementRepo _enrollementRepository;
        public string url = "http://localhost:8081/Camunda/api/addVariable";

        public P03Actions(ILogger<P03Actions> logger,
            HttpClient httpClient,
            ICourseOfferingRepository courseOfferingRepository,
            IMajorRepository majorRepository,
            IStudentRepository studentRepository,
            IEnrollementRepo enrollementRepository)
        {
            _logger = logger;
            _httpClient = httpClient;
            _courseOfferingRepository = courseOfferingRepository;
            _majorRepository = majorRepository;
            _studentRepository = studentRepository;
            _enrollementRepository = enrollementRepository;
        }

        public async Task<GenericResponseModel> A03_01_ValidateGPA(CamundaRequestModel request)
        {
            _logger.LogInformation($"Executing {nameof(A03_01_ValidateGPA)}");
            DataModel data = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(request.data);
            var student = await _studentRepository.GetStudentByEmailAsync(data.Email);
            if (student == null)
                _logger.LogError("Student doesnt exist in DB");
            var enrollement = await _enrollementRepository.GetEnrollmentAsync(student.StudentID);

            CamundaControllerModel camundaControllerModel = new CamundaControllerModel();
            camundaControllerModel.processInstanceId = request.processInstanceId;
            camundaControllerModel.variableName = "isPass";
            camundaControllerModel.variableType = "bool";

            double averageGPA = enrollement.Sum(e => e.GPA);
            averageGPA = averageGPA / enrollement.Count();
            if (averageGPA>= 2.0)
            {
                camundaControllerModel.variableValue = "true";
                string jsonData = JsonConvert.SerializeObject(camundaControllerModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                return new GenericResponseModel { Status = StatusCode.Success, Error = null };
            }
            camundaControllerModel.variableValue = "false";
            string jsonData1 = JsonConvert.SerializeObject(camundaControllerModel);
            StringContent content1 = new StringContent(jsonData1, Encoding.UTF8, "application/json");
            HttpResponseMessage response1 = await _httpClient.PostAsync(url, content1);
            return new GenericResponseModel { Status = StatusCode.Error, Error = "No Seats Available in Major" };
        }
    }
}
